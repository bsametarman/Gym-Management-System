using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/enterLog")]
    public class EnterLogController : Controller
    {
        IEnterLogService _enterLogService = InstanceFactory.GetInstance<IEnterLogService>();
        public EnterLogController()
        {
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _enterLogService.GetAll();
            if (result.Success)
                return Ok(new SuccessDataResult<List<EnterLog>>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _enterLogService.GetById(id);
            if (result.Success)
                return Ok(new SuccessDataResult<EnterLog>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("addEnterLog")]
        public IActionResult Add(EnterLog enterLog)
        {
            var result = _enterLogService.Add(enterLog);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla eklendi !"));
            return BadRequest(result);
        }

        [HttpPost("updateEnterLog")]
        public IActionResult Update(EnterLog enterLog)
        {
            var result = _enterLogService.Update(enterLog);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla güncellendi !"));
            return BadRequest(result);
        }

        [HttpDelete("deleteEnterLog")]
        public IActionResult Delete(EnterLog enterLog)
        {
            var result = _enterLogService.Delete(enterLog);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla silindi !"));
            return BadRequest(result);
        }
    }
}
