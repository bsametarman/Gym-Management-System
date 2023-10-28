using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.Concrete;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/trainer")]
    public class TrainerController : Controller
    {
        ITrainerService _trainerManager = InstanceFactory.GetInstance<ITrainerService>();

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _trainerManager.GetAll();
            if (result.Success)
                return Ok(new SuccessDataResult<List<Trainer>>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _trainerManager.GetById(id);
            if (result.Success)
                return Ok(new SuccessDataResult<Trainer>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("addTrainer")]
        public IActionResult Add(Trainer trainer)
        {
            var result = _trainerManager.Add(trainer);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla eklendi !"));
            return BadRequest(result);
        }

        [HttpPost("updateTrainer")]
        public IActionResult Update(Trainer trainer)
        {
            var result = _trainerManager.Update(trainer);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla güncellendi !"));
            return BadRequest(result);
        }

        [HttpDelete("deleteTrainer")]
        public IActionResult Delete(Trainer trainer)
        {
            var result = _trainerManager.Delete(trainer);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla silindi !"));
            return BadRequest(result);
        }
    }
}
