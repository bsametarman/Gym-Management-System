using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.Concrete;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/branch")]
    public class BranchController : Controller
    {
        IBranchService branchService = InstanceFactory.GetInstance<IBranchService>();



        [HttpGet("getAllUsers")]
        public IActionResult GetAll()
        {
            var result = branchService.GetAll();
            if (result.Success)
                return Ok(new SuccessDataResult<List<Branch>>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = branchService.GetById(id);
            if (result.Success)
                return Ok(new SuccessDataResult<Branch>(result.Data, result.Message));
            return NotFound(new ErrorDataResult<Branch> (result.Data, result.Message));
        }

        [HttpPost("addUser")]
        public IActionResult Add(Branch branch)
        {
            var result = branchService.Add(branch);
            if (result.Message != null)
                return Ok(new SuccessResult("Başarıyla eklendi !"));
            return BadRequest(result.Message);
        }

        [HttpDelete("deleteUser")]
        public IActionResult Delete(Branch branch)
        {
            var result = branchService.Delete(branch);
            if (result.Success)
                return Ok(new SuccessResult(result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("updateUser")]
        public IActionResult Update(Branch branch)
        {
            var result = branchService.Update(branch);
            if (result.Success)
                return Ok(new SuccessResult(result.Message));
            return BadRequest(result.Message);

        }
    }
}
