using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.MVCWebUI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private IUserService _userService;
        private UserManager<AppUser> _userManager;

        public DashboardController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("member"))
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var viewBagUser = _userManager.FindByIdAsync(userId);
                ViewBag.Id = userId;
                ViewBag.Name = viewBagUser.Result.Name;
                ViewBag.Surname = viewBagUser.Result.Surname;
                ViewBag.Username = viewBagUser.Result.UserName;
                ViewBag.IdentityNumber = viewBagUser.Result.IdentityNumber;
                ViewBag.Address = viewBagUser.Result.Address;
                ViewBag.Email = viewBagUser.Result.Email;
                ViewBag.PhoneNumber = viewBagUser.Result.PhoneNumber;
                ViewBag.EmergencyPhoneNumber = viewBagUser.Result.EmergencyPhoneNumber;
                ViewBag.BloodTypeId = viewBagUser.Result.BloodTypeId;
                ViewBag.YearOfBirth = viewBagUser.Result.YearOfBirth;
                ViewBag.EnterCount = viewBagUser.Result.EnterCount;
                ViewBag.TrainerId = viewBagUser.Result.TrainerId;
                ViewBag.CreatedDate = viewBagUser.Result.CreatedDate;
                ViewBag.MembershipExpirationDate = viewBagUser.Result.MembershipExpirationDate;
            }
            if (User.IsInRole("owner"))
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var viewBagUser = _userManager.FindByIdAsync(userId);
                ViewBag.Name = viewBagUser.Result.Name;
                ViewBag.Surname = viewBagUser.Result.Surname;

                List<AppUser> usersWithUserRole = new List<AppUser>();
                var users = _userService.GetAll();
                foreach (var user in users.Data)
                {
                    usersWithUserRole.Add(user);
                }
                return View(usersWithUserRole);
            }
            else if (User.IsInRole("manager"))
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var viewBagUser = _userManager.FindByIdAsync(userId);
                ViewBag.Name = viewBagUser.Result.Name;
                ViewBag.Surname = viewBagUser.Result.Surname;

                List<AppUser> usersWithUserRole = new List<AppUser>();
                var users = _userService.GetAll();
                foreach (var user in users.Data)
                {
                    bool isNormalUser = true;
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        if (role == "manager" || role == "owner")
                            isNormalUser = false;
                    }
                    if (isNormalUser)
                        usersWithUserRole.Add(user);
                }
                return View(usersWithUserRole);
            }
            else if (User.IsInRole("employee"))
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var viewBagUser = _userManager.FindByIdAsync(userId);
                ViewBag.Name = viewBagUser.Result.Name;
                ViewBag.Surname = viewBagUser.Result.Surname;

                List<AppUser> usersWithUserRole = new List<AppUser>();
                var users = _userService.GetAll();
                foreach (var user in users.Data)
                {
                    bool isNormalUser = true;
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        if (role == "manager" || role == "owner" || role == "employee")
                            isNormalUser = false;
                    }
                    if (isNormalUser)
                        usersWithUserRole.Add(user);
                }
                return View(usersWithUserRole);
            }
            return View();
        }
    }
}
