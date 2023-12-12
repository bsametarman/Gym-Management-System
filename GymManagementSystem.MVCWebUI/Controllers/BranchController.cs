using GymManagementSystem.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.MVCWebUI.Controllers
{
    public class BranchController : Controller
    {
        private IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        public IActionResult Index()
        {
            var branches = _branchService.GetAll();
            return View(branches.Data);
        }

        public IActionResult Detail(int id) 
        {
            var branch = _branchService.GetById(id);
            return View(branch.Data);
        }
    }
}
