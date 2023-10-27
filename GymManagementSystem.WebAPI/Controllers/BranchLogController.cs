using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/branchLog")]
    public class BranchLogController : Controller
    {
        IBranchLogService _branchLogService = InstanceFactory.GetInstance<IBranchLogService>();
        public BranchLogController() 
        {
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _branchLogService.GetAll();
            if (result.Success)
                return Ok(new SuccessDataResult<List<BranchLog>>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id) 
        {
            var result = _branchLogService.GetById(id);
            if (result.Success)
                return Ok(new SuccessDataResult<BranchLog>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("addBranchLog")]
        public IActionResult Add(BranchLog branchLog)
        {
            var result = _branchLogService.Add(branchLog);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla eklendi !"));
            return BadRequest(result);
        }

        [HttpPost("updateBranchLog")]
        public IActionResult Update(BranchLog branchLog)
        {
            var result = _branchLogService.Update(branchLog);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla güncellendi !"));
            return BadRequest(result);
        }

        [HttpDelete("deleteBranchLog")]
        public IActionResult Delete(BranchLog branchLog)
        {
            var result = _branchLogService.Delete(branchLog);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla silindi !"));
            return BadRequest(result);
        }
    }
}
