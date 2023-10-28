using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.Concrete;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/bloodType")]
    public class BloodTypeController : Controller
    {
        IBloodTypeService _bloodTypeManager = InstanceFactory.GetInstance<IBloodTypeService>();

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _bloodTypeManager.GetAll();
            if (result.Success)
                return Ok(new SuccessDataResult<List<BloodType>>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _bloodTypeManager.GetById(id);
            if (result.Success)
                return Ok(new SuccessDataResult<BloodType>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("addBloodType")]
        public IActionResult Add(BloodType bloodType)
        {
            var result = _bloodTypeManager.Add(bloodType);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla eklendi !"));
            return BadRequest(result);
        }

        [HttpPost("updateBloodType")]
        public IActionResult Update(BloodType bloodType)
        {
            var result = _bloodTypeManager.Update(bloodType);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla güncellendi !"));
            return BadRequest(result);
        }

        [HttpDelete("deleteBloodType")]
        public IActionResult Delete(BloodType bloodType)
        {
            var result = _bloodTypeManager.Delete(bloodType);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla silindi !"));
            return BadRequest(result);
        }
    }
}
