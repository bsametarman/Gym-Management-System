using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Entities.Concrete;
using GymManagementSystem.MVCWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GymManagementSystem.MVCWebUI.Controllers
{
    public class HomeController : Controller
    {
        private IBranchService _branchService;

        public HomeController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        public IActionResult Index()
        {
            var branches = _branchService.GetAll();
            return View(branches.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}