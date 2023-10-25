using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.Concrete;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.DataAccess.Concrete;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        IUserService userService = InstanceFactory.GetInstance<IUserService>();
        private UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

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
            return NotFound(new ErrorDataResult<AppUser>(result.Data, result.Message));
        }

        [HttpPost("addUser")]
        public IActionResult Add(AppUser user)
        {
            var result = userService.Add(user, _userManager);
            if (result.Result != null)
                return Ok(new SuccessResult("Başarıyla eklendi !"));
            return BadRequest(result.Result.Message);
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
