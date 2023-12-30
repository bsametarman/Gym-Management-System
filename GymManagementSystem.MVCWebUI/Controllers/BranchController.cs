using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Errors;
using GymManagementSystem.Entities.Concrete;
using GymManagementSystem.MVCWebUI.Tools.Validation;
using Microsoft.AspNetCore.Authorization;
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
            var tempDataValues = TempData["Errors"] as IEnumerable<string>;
            ViewBag.Errors = tempDataValues;

            var branch = _branchService.GetById(id);
            return View(branch.Data);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangeBranchInfo(Branch branch)
        {
            List<string> errors = new List<string>();
            List<List<ValidationError>> validationErrors = new List<List<ValidationError>>();
            
            var br = _branchService.GetById(branch.Id);

            BranchValidator branchValidator = new BranchValidator(_branchService);

            if(branch.Name != br.Data.Name)
                validationErrors.Add(branchValidator.CheckName(branch.Name));
            if (branch.Description != br.Data.Description)
                validationErrors.Add(branchValidator.CheckDescription(branch.Description));
            if (branch.Email != br.Data.Email)
                validationErrors.Add(branchValidator.CheckEmail(branch.Email));
            if (branch.PhoneNumber != br.Data.PhoneNumber)
                validationErrors.Add(branchValidator.CheckPhoneNumber(branch.PhoneNumber));
            if (branch.City != br.Data.City)
                validationErrors.Add(branchValidator.CheckCity(branch.City));
            if (branch.Address != br.Data.Address)
                validationErrors.Add(branchValidator.CheckAddress(branch.Address));
            if (branch.Capacity != br.Data.Capacity)
                validationErrors.Add(branchValidator.CheckCapacity(branch.Capacity));
            if (branch.Country != br.Data.Country)
                validationErrors.Add(branchValidator.CheckCountry(branch.Country));
            if (branch.NumberOfEmployee != br.Data.NumberOfEmployee)
                validationErrors.Add(branchValidator.CheckNumberOfEmployee(branch.NumberOfEmployee));
            if (branch.NumberOfTools!= br.Data.NumberOfTools)
                validationErrors.Add(branchValidator.CheckNumberOfTools(branch.NumberOfTools));
            if (branch.SquareMeters != br.Data.Name)
                validationErrors.Add(branchValidator.CheckSquareMeters(branch.SquareMeters));
            if (branch.WeekdaysOpeningTime != br.Data.WeekdaysOpeningTime || branch.WeekdaysClosingTime != br.Data.WeekdaysClosingTime || branch.WeekendsOpeningTime != br.Data.WeekendsOpeningTime || branch.WeekendsClosingTime != br.Data.WeekendsClosingTime)
                validationErrors.Add(branchValidator.CheckBranchWorkingTime(branch.WeekdaysOpeningTime, branch.WeekdaysClosingTime, branch.WeekendsOpeningTime, branch.WeekendsClosingTime));

            foreach (var results in validationErrors)
            {
                if (results != null)
                    foreach (var error in results)
                    {
                        errors.Add(error.Description);
                    }
            }

            TempData["Errors"] = errors;

            if(errors.Count == 0)
            {
                _branchService.Update(branch);
                return RedirectToAction("Detail", "Branch", new { id = branch.Id });
            }

            return RedirectToAction("Detail", "Branch", new { id = branch.Id });
        }

        [Authorize]
        public IActionResult AddBranch()
        {
            if (User.IsInRole("member"))
                return RedirectToAction("Account", "Dashboard");
            return View();
        }

        [HttpPost]
        [Authorize]
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

        [Authorize]
        public IActionResult Delete(int id)
        {
            var branch = _branchService.GetById(id);

            if(branch == null)
            {
                return View("Error");
            }
            var result = _branchService.Delete(branch.Data);

            if (result.Success)
            {
                return RedirectToAction("Index", "Branch");
            }
            return View(branch);
        }
    }
}
