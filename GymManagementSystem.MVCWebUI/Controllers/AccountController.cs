﻿using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Entities.Concrete;
using GymManagementSystem.MVCWebUI.Models;
using GymManagementSystem.MVCWebUI.Tools.Validation;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json.Linq;
using System.Drawing;
using ZXing.QrCode;
using ZXing;
using ZXing.Windows.Compatibility;
using GymManagementSystem.Core.Utilities.Errors;

namespace GymManagementSystem.MVCWebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private IUserService _userService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserSignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (passwordCheck)
                {
                    if (!user.IsActive)
                    {
                        TempData["Error"] = "Your account has been blocked! Please contact with us.";
                        return View(model);
                    }
                    else
                    {
                        if((user.MembershipExpirationDate - DateTime.Now).Days <= 0)
                        {
                            user.IsPassActive = false;
                            user.PaymentStatusId = 2;
                            await _userManager.UpdateAsync(user);
                        }

                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["error"] = "Something went wrong. Try again a little bit!";
                            return View(model);
                        }
                    }
                }
                else
                {
                    TempData["Error"] = "Username or password is wrong!";
                    return View(model);
                }
            }
            else
            {
                TempData["Error"] = "Username or password is wrong!";
                return View(model);
            }
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(UserRegisterViewModel userCredentials)
        {
			//if (!ModelState.IsValid)
			//    return View(userCredentials);
			List<string> errors = new List<string>();

			var findUser = await _userManager.FindByEmailAsync(userCredentials.Email);

            if (findUser != null)
            {
                errors.Add("There is a user with that informations.");
                ViewBag.Errors = errors;
                return View(userCredentials);
            }

            AppUser user = new AppUser
            {
                Name = userCredentials.Name,
                Surname = userCredentials.Surname,
                NormalizedUserName = userCredentials.Username,
                IdentityNumber = userCredentials.IdentityNumber,
                Email = userCredentials.Email,
                Password = userCredentials.Password,
                YearOfBirth = userCredentials.YearOfBirth,
                PhoneNumber = userCredentials.PhoneNumber,
                Gender = userCredentials.Gender,
                UserName = userCredentials.Name,
                IsActive = true,
                IsPassActive = false,
                CreatedDate = DateTime.Now,
                LastPaymentDate = DateTime.Now,
                BloodTypeId = userCredentials.BloodTypeId,
                MembershipTypeId = 4,
                MembershipExpirationDate = DateTime.Now.AddDays(-1),
                PaymentStatusId = 4,
                TrainerId = 2,
                EnterCount = 0,
                Address = userCredentials.Address,
                EmergencyPhoneNumber = userCredentials.EmergencyPhoneNumber,
                EmailConfirmed = true,
                UserRole = "member",
            };
            user.UserName = userCredentials.Username;

            // Validation
            UserValidator validator = new UserValidator(_userService);

            List<List<ValidationError>> validationResults = new List<List<ValidationError>>();

            validationResults.Add(validator.CheckName(user.Name));
            validationResults.Add(validator.CheckSurname(user.Surname));
            validationResults.Add(validator.CheckUsername(user.UserName));
            validationResults.Add(validator.CheckPassword(user.Password));
            validationResults.Add(validator.CheckEmail(user.Email));
            validationResults.Add(validator.CheckYearOfBirth(user.YearOfBirth));
            validationResults.Add(validator.CheckIdentityNumber(user.IdentityNumber));
            validationResults.Add(validator.CheckAddress(user.Address));
            validationResults.Add(validator.CheckPhoneNumber(user.PhoneNumber));
            validationResults.Add(validator.CheckEmergencyPhoneNumber(user.EmergencyPhoneNumber));

            foreach (var results in validationResults)
            {
                //ModelState.AddModelError("", item.Description);
                if(results != null)
                    foreach (var error in results)
                    {
                        errors.Add(error.Description);
                    }
            }
            ViewBag.Errors = errors;

            if(errors.Count == 0)
            {
                var result = await _userManager.CreateAsync(user, userCredentials.Password);
                
				if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Member);
                    SendUserToEmail(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        //ModelState.AddModelError("", item.Description);
                        errors.Add(error.Description);
                    }
                    ViewBag.Errors = errors;
                }
            }
            return View(userCredentials);
        }

        public IActionResult EmailChange(string id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailChange(UserEmailChangeViewModel model)
        {
            ViewBag.id = model.Id;
            var user = await _userManager.FindByIdAsync(model.Id);
            List<string> errors = new List<string>();

            if (user == null)
                return View(model);

            UserValidator validator = new UserValidator(_userService);

            List<List<ValidationError>> validationResults = new List<List<ValidationError>>();

            validationResults.Add(validator.CheckEmail(model.NewEmail));

            foreach (var results in validationResults)
            {
                if (results != null)
                    foreach (var error in results)
                    {
                        errors.Add(error.Description);
                    }
            }
            ViewBag.Errors = errors;

            if(errors.Count == 0)
            {
                user.Email = model.NewEmail;
                await _userManager.UpdateAsync(user);

                SendUserToEmail(user);

                return RedirectToAction("Account", "Dashboard");
            }

            return View(model);
        }

        public IActionResult PasswordChange(string id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeViewModel passwordChangeViewModel)
        {
            ViewBag.id = passwordChangeViewModel.Id;
            List<string> errors = new List<string>();

            var user = await _userManager.FindByIdAsync(passwordChangeViewModel.Id);

            if (user == null)
                return View(passwordChangeViewModel.Id);

            UserValidator validator = new UserValidator(_userService);

            List<List<ValidationError>> validationResults = new List<List<ValidationError>>();

            validationResults.Add(validator.CheckPassword(passwordChangeViewModel.Password));

            foreach (var results in validationResults)
            {
                if (results != null)
                    foreach (var error in results)
                    {
                        errors.Add(error.Description);
                    }
            }
            ViewBag.Errors = errors;

            if(errors.Count == 0)
            {
                user.Password = passwordChangeViewModel.Password;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, passwordChangeViewModel.Password);
                await _userManager.UpdateAsync(user);

                SendUserToEmail(user);

                return RedirectToAction("Account", "Dashboard");
            }

            return View(passwordChangeViewModel);
        }

        public IActionResult UsernameChange(string id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UsernameChange(UserUsernameChangeViewModel model)
        {
            ViewBag.id = model.Id;
            var user = await _userManager.FindByIdAsync(model.Id);
            List<string> errors = new List<string>();

            if (user == null)
                return View(model);

            UserValidator validator = new UserValidator(_userService);

            List<List<ValidationError>> validationResults = new List<List<ValidationError>>();

            validationResults.Add(validator.CheckUsername(model.Username));

            foreach (var results in validationResults)
            {
                if (results != null)
                    foreach (var error in results)
                    {
                        errors.Add(error.Description);
                    }
            }
            ViewBag.Errors = errors;

            if(errors.Count == 0)
            {
                user.UserName = model.Username;
                await _userManager.UpdateAsync(user);

                SendUserToEmail(user);

                return RedirectToAction("Account", "Dashboard");
            }

            return View(model);
        }

        public async Task<IActionResult> EditActiveState(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return View("Not Found");
            }
            if (user.IsActive == true)
            {
                user.IsActive = false;
                user.IsPassActive = false;
            }
            else
            {
                user.IsActive = true;
                if((user.MembershipExpirationDate - DateTime.Now).Days > 0)
                    user.IsPassActive = true;
            }

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Detail", "Dashboard", new { id = user.Id });
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            if (User.IsInRole("owner") || User.IsInRole("employee"))
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return View("Not Found");
                }

                await _userManager.DeleteAsync(user);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult RoleChange(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public async Task<IActionResult> UserRoleChange(string id, string role)
        {
            List<string> roles = new List<string>() { "member", "employee", "manager", "owner" };
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return View(id);

            foreach (var roleitem in roles)
            {
                await _userManager.RemoveFromRoleAsync(user, roleitem);
            }

            user.UserRole = role;
            await _userManager.AddToRoleAsync(user, role);
            user.IsPassActive = true;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult ExtendTime(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public async Task<IActionResult> UserExtendTime(string id, int dayToExtend)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return View(id);

            user.LastPaymentDate = user.LastPaymentDate.AddDays(dayToExtend);
            if((DateTime.Now - user.MembershipExpirationDate).Days >= 0)
            {
                user.MembershipExpirationDate = DateTime.Now.AddDays(dayToExtend);
                user.PaymentStatusId = 1;
                user.IsPassActive = true;
            }
            else
            {
                user.MembershipExpirationDate = user.MembershipExpirationDate.AddDays(dayToExtend);
                user.PaymentStatusId = 1;
                user.IsPassActive = true;
            }

            if ((DateTime.Now - user.MembershipExpirationDate).Days >= 0)
            {
                user.PaymentStatusId = 2;
                user.IsPassActive = false;
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Dashboard");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordViewModel forgotPasswordViewModel)
        {
            List<string> errors = new List<string>();
            var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);

            if(user != null)
            {
                List<List<ValidationError>> validationResults = new List<List<ValidationError>>();

                UserValidator validator = new UserValidator(_userService);
                var passwordValidation = validator.CheckPassword(forgotPasswordViewModel.NewPassword);

                validationResults.Add(passwordValidation);

                foreach (var results in validationResults)
                {
                    if (results != null)
                        foreach (var error in results)
                        {
                            errors.Add(error.Description);
                        }
                }
                if(errors.Count == 0)
                {
                    user.Password = forgotPasswordViewModel.NewPassword;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, forgotPasswordViewModel.NewPassword);

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        SendUserToEmail(user);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                            errors.Add(error.Description);
                        ViewBag.Errors = errors;
                    }
                }
                ViewBag.Errors = errors;
                return View(forgotPasswordViewModel);
            }
            else
            {
                errors.Add("User could not found with this email.");
                ViewBag.Errors = errors;
                return View(forgotPasswordViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserInfo(AppUser model)
        {
            List<string> errors = new List<string>();
            bool isCridentialsChanged = false;

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return View(model.Id);

            UserValidator validator = new UserValidator(_userService);

            List<List<ValidationError>> validationResults = new List<List<ValidationError>>();

            if(user.Name != model.Name)
                validationResults.Add(validator.CheckName(model.Name));
            if (user.Surname != model.Surname)
                validationResults.Add(validator.CheckSurname(model.Surname));
            if (user.UserName != model.UserName)
            {
                validationResults.Add(validator.CheckUsername(model.UserName));
                isCridentialsChanged = true;
            }
            if (user.Password != model.Password)
            {
                validationResults.Add(validator.CheckPassword(model.Password));
                isCridentialsChanged = true;
            }
            if (user.Email != model.Email)
            {
                validationResults.Add(validator.CheckEmail(model.Email));
                isCridentialsChanged = true;
            }
            if (user.Address != model.Address)
                validationResults.Add(validator.CheckAddress(model.Address));
            if (user.PhoneNumber != model.PhoneNumber)
                validationResults.Add(validator.CheckPhoneNumber(model.PhoneNumber));
            if (user.EmergencyPhoneNumber != model.EmergencyPhoneNumber)
                validationResults.Add(validator.CheckEmergencyPhoneNumber(model.EmergencyPhoneNumber));

            foreach (var results in validationResults)
            {
                if (results != null)
                    foreach (var error in results)
                    {
                        errors.Add(error.Description);
                    }
            }
            TempData["Errors"] = errors;

            if (errors.Count == 0)
            {
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.UserName = model.UserName;
                if(user.Password != model.Password)
                {
                    user.Password = model.Password;
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                }
                user.Email = model.Email;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.EmergencyPhoneNumber = model.EmergencyPhoneNumber;
                await _userManager.UpdateAsync(user);

                if(isCridentialsChanged)
                    SendUserToEmail(user);

                return RedirectToAction("Detail", "Dashboard", new { id = model.Id });
            }

            return RedirectToAction("Detail", "Dashboard", new {id = model.Id});
        }

        public async Task<IActionResult> ChangeUserGymPass(string id)
        {
            if (User.IsInRole("owner") || User.IsInRole("manager"))
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return View("Not Found");
                }

                //if((user.MembershipExpirationDate - DateTime.Now).Days <= 0)
                //{
                //    user.MembershipExpirationDate = DateTime.Now.AddDays(3);
                //    user.PaymentStatusId = 1;
                //}
                    
                user.IsPassActive = user.IsPassActive ? false : true;

                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public void SendUserToEmail(AppUser user)
        {
            var passwordHash = _userManager.FindByIdAsync(user.Id).Result.PasswordHash;

            QrCodeEncodingOptions options = new()
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 500,
                Height = 500
            };

            BarcodeWriter writer = new()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };

            Bitmap qrCodeBitmap = writer.Write($"{user.UserName}*onyedieylulgym*{passwordHash}");
            qrCodeBitmap.Save($"./Tools/QrCodes/{user.UserName}.png");

            // Reading credentials from json for email sending
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "secrets.json");
            string json = System.IO.File.ReadAllText(jsonFilePath);
            JObject jsonObj = JObject.Parse(json);

            //Sending mail
            MimeMessage mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress("Gym Management System Support", "gym.man.system.sup@gmail.com"));
            mimeMessage.To.Add(new MailboxAddress("User", user.Email));

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Hoşgeldiniz!" +
                $"\n\nÜyelik bilgileriniz aşağıdaki gibidir." +
                $"\n\nKullanıcı Adı: {user.UserName}" +
                $"\nŞifre: {user.Password}" +
                $"\n\nDilerseniz ekte bulunan QR kod ile salonlarımıza hızlıca giriş yapabilirsiniz.";
            bodyBuilder.Attachments.Add(@$"./Tools/QrCodes/{user.UserName}.png");

            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "GYM Üyelik Bilgileri";

            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate((string)jsonObj["Secrets"]["Mail"], (string)jsonObj["Secrets"]["Key"]);
            smtp.Send(mimeMessage);
            smtp.Disconnect(true);
        }
    }
}
