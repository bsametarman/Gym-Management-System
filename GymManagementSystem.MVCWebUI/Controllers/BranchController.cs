using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Errors;
using GymManagementSystem.Entities.Concrete;
using GymManagementSystem.MVCWebUI.Tools.Validation;
using Microsoft.AspNetCore.Http;
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

        public IActionResult AddBranch()
        {
            if (User.IsInRole("member"))
                return RedirectToAction("Account", "Dashboard");
            return View();
        }

        [HttpPost]
        public IActionResult AddBranch(Branch branch)
        {
            List<string> errors = new List<string>();
            List<List<ValidationError>> validationErrors = new List<List<ValidationError>>();

            if(!ModelState.IsValid)
                return View(branch);

            branch.IsActive = true;
            branch.NumberOfMembers = 0;
            branch.Price = 0;

            BranchValidator branchValidator = new BranchValidator(_branchService);

			validationErrors.Add(branchValidator.CheckName(branch.Name));
			validationErrors.Add(branchValidator.CheckDescription(branch.Description));
			validationErrors.Add(branchValidator.CheckEmail(branch.Email));
			validationErrors.Add(branchValidator.CheckPhoneNumber(branch.PhoneNumber));
			validationErrors.Add(branchValidator.CheckCity(branch.City));
			validationErrors.Add(branchValidator.CheckAddress(branch.Address));
			validationErrors.Add(branchValidator.CheckCapacity(branch.Capacity));
			validationErrors.Add(branchValidator.CheckCountry(branch.Country));
			validationErrors.Add(branchValidator.CheckNumberOfEmployee(branch.NumberOfEmployee));
			validationErrors.Add(branchValidator.CheckNumberOfTools(branch.NumberOfTools));
			validationErrors.Add(branchValidator.CheckSquareMeters(branch.SquareMeters));
			validationErrors.Add(branchValidator.CheckBranchWorkingTime(branch.WeekdaysOpeningTime, branch.WeekdaysClosingTime, branch.WeekendsOpeningTime, branch.WeekendsClosingTime));

            foreach (var results in validationErrors)
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
				var result = _branchService.Add(branch);

				if (result.Success)
					return RedirectToAction("Index", "Home");
                else
					errors.Add(result.Message);
			    ViewBag.Errors = errors;
			}

            return View(branch);
        }
    }
}
