using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Entities.ComplexTypes;
using GymManagementSystem.Entities.Concrete;
using GymManagementSystem.MVCWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymManagementSystem.MVCWebUI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private IUserService _userService;
        private UserManager<AppUser> _userManager;
        private IMembershipService _membershipService;

        public DashboardController(IUserService userService, UserManager<AppUser> userManager, IMembershipService membershipService)
        {
            _userService = userService;
            _userManager = userManager;
            _membershipService = membershipService;
        }

        public async Task<IActionResult> Index()
        {
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

        public IActionResult Account()
        {
            if (User.IsInRole("member"))
            {
                var userId = _userManager.GetUserId(HttpContext.User);

                var user = _userService.GetUserWithDetails(userId);
                if (user != null)
                    return View(user.Data);
            }
            else
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        public IActionResult Payment()
        {
            var memberships = _membershipService.GetAll();
            ViewBag.Memberships = memberships.Data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Payment(UserPaymentViewModel userPaymentViewModel)
        {
            if (userPaymentViewModel != null)
            {
                var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

                user.MembershipTypeId = userPaymentViewModel.MembershipTypeId;
                if(user.MembershipTypeId == 1 || user.MembershipTypeId == 2)
                {
                    user.LastPaymentDate = DateTime.Now;
                    if ((DateTime.Now - user.MembershipExpirationDate).Days >= 0)
                        user.MembershipExpirationDate = DateTime.Now.AddDays(30);
                    else
                        user.MembershipExpirationDate = user.MembershipExpirationDate.AddDays(30);
                    user.IsPassActive = true;
                    await _userManager.UpdateAsync(user);
                }
                else if(user.MembershipTypeId == 3)
                {
                    user.LastPaymentDate = DateTime.Now;
                    if ((DateTime.Now - user.MembershipExpirationDate).Days >= 0)
                        user.MembershipExpirationDate = DateTime.Now.AddYears(5);
                    else
                        user.MembershipExpirationDate = user.MembershipExpirationDate.AddYears(5);
                    user.IsPassActive = true;
                    await _userManager.UpdateAsync(user);
                }
            }
            return RedirectToAction("Account", "Dashboard");
        }

        public IActionResult Detail(string id)
        {
            var user = _userService.GetUserWithDetails(id);
            var tempDataValues = TempData["Errors"] as IEnumerable<string>;
            ViewBag.Errors = tempDataValues;
            if(user == null)
            {
                return NotFound();
            }
            return View("~/Views/Dashboard/Detail.cshtml", user.Data);
        }
    }
}
