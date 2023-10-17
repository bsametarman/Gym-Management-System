using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        IUserService userService = InstanceFactory.GetInstance<IUserService>();

        [HttpGet("getAllUsers")]
        public IActionResult GetAll()
        {
            var result = userService.GetAll();
            if (result.Success)
                return Ok(new SuccessDataResult<List<AppUser>>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(string id)
        {
            var result = userService.GetById(id);
            if (result.Success)
                return Ok(new SuccessDataResult<AppUser>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("addUser")]
        public IActionResult Add(AppUser user)
        {
            var result = userService.Add(user);
            if (result.Success)
                return Ok(new SuccessResult(result.Message));
            return BadRequest(result.Message);
        }

        [HttpDelete("deleteUser")]
        public IActionResult Delete(AppUser user)
        {
            var result = userService.Delete(user);
            if (result.Success)
                return Ok(new SuccessResult(result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("updateUser")]
        public IActionResult Update(AppUser user)
        {
            var result = userService.Update(user);
            if (result.Success)
                return Ok(new SuccessResult(result.Message));
            return BadRequest(result.Message);
        }
    }
}
