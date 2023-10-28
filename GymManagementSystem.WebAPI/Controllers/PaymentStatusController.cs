using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/paymentStatus")]
    public class PaymentStatusController : Controller
    {
        IPaymentStatusService _paymentStatusManager = InstanceFactory.GetInstance<IPaymentStatusService>();

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _paymentStatusManager.GetAll();
            if (result.Success)
                return Ok(new SuccessDataResult<List<PaymentStatus>>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _paymentStatusManager.GetById(id);
            if (result.Success)
                return Ok(new SuccessDataResult<PaymentStatus>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("addPaymentStatus")]
        public IActionResult Add(PaymentStatus paymentStatus)
        {
            var result = _paymentStatusManager.Add(paymentStatus);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla eklendi !"));
            return BadRequest(result);
        }

        [HttpPost("updatePaymentStatus")]
        public IActionResult Update(PaymentStatus paymentStatus)
        {
            var result = _paymentStatusManager.Update(paymentStatus);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla güncellendi !"));
            return BadRequest(result);
        }

        [HttpDelete("deletePaymentStatus")]
        public IActionResult Delete(PaymentStatus paymentStatus)
        {
            var result = _paymentStatusManager.Delete(paymentStatus);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla silindi !"));
            return BadRequest(result);
        }
    }
}
