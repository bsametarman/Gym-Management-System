using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using GymManagementSystem.MVCWebUI.Models;
using GymManagementSystem.MVCWebUI.Tools.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
                if (!((DateTime.Now - user.MembershipExpirationDate).Days > 3))
                {
                    if (user.IsActive != false)
                    {
                        var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                        if (passwordCheck)
                        {
                            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        TempData["Error"] = "Kullanıcı adı veya şifre yanlış!";
                        return View(model);
                    }
                    TempData["Error"] = "Hesabınız süreniz doldu veya hesabınız bloke edildi! Lütfen iletişime geçiniz.";
                    return View(model);
                }
                else
                {
                    TempData["Error"] = "Hesap süreniz doldu! Lütfen üyeliğinizi yenileyin.";
                    user.IsActive = false;
                    await _userManager.UpdateAsync(user);
                    return View(model);
                }
            }
            return RedirectToAction("SignIn");
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

            var findUser = await _userManager.FindByEmailAsync(userCredentials.Email);

            if (findUser != null)
                return View(userCredentials);

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
                CreatedDate = DateTime.Now,
                LastPaymentDate = DateTime.Now,
                MembershipExpirationDate = DateTime.Now.AddDays(30),
                EnterCount = 0,
                Address = userCredentials.Address,
                EmergencyPhoneNumber = userCredentials.EmergencyPhoneNumber,
                EmailConfirmed = true,
                UserRole = "member",
            };
            user.UserName = userCredentials.Username;

            // Validation
            UserValidator validator = new UserValidator(_userService);

            List<List<IdentityError>> validationResults = new List<List<IdentityError>>();

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

            List<string> errors = new List<string>();

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

        public async Task<IActionResult> UserEmailChange(string id, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return View(id);

            user.Email = newEmail;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult PasswordChange(string id)
        {
            ViewBag.id = id;
            return View();
        }

        public async Task<IActionResult> UserPasswordChange(string id, string password)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return View(id);

            user.Password = password;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult UsernameChange(string id)
        {
            ViewBag.id = id;
            return View();
        }

        public async Task<IActionResult> UserUsernameChange(string id, string username)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return View(id);

            user.UserName = username;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Dashboard");
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
            }
            else
            {
                user.IsActive = true;
            }

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index", "Dashboard");
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
            user.MembershipExpirationDate = user.MembershipExpirationDate.AddDays(dayToExtend);
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
