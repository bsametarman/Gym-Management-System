using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.Concrete;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/membership")]
    public class MembershipController : Controller
    {
        IMembershipService _membershipManager = InstanceFactory.GetInstance<IMembershipService>();

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _membershipManager.GetAll();
            if (result.Success)
                return Ok(new SuccessDataResult<List<Membership>>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _membershipManager.GetById(id);
            if (result.Success)
                return Ok(new SuccessDataResult<Membership>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("addMembership")]
        public IActionResult Add(Membership membership)
        {
            var result = _membershipManager.Add(membership);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla eklendi !"));
            return BadRequest(result);
        }

        [HttpPost("updateMembership")]
        public IActionResult Update(Membership membership)
        {
            var result = _membershipManager.Update(membership);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla güncellendi !"));
            return BadRequest(result);
        }

        [HttpDelete("deleteMembership")]
        public IActionResult Delete(Membership membership)
        {
            var result = _membershipManager.Delete(membership);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla silindi !"));
            return BadRequest(result);
        }
    }
}
