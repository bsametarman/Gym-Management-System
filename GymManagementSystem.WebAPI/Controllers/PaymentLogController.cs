using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.Concrete;
using GymManagementSystem.Business.DependencyResolvers.Ninject;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/paymentLog")]
    public class PaymentLogController : Controller
    {
        IPaymentLogService _paymentLogManager = InstanceFactory.GetInstance<IPaymentLogService>();

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _paymentLogManager.GetAll();
            if (result.Success)
                return Ok(new SuccessDataResult<List<PaymentLog>>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _paymentLogManager.GetById(id);
            if (result.Success)
                return Ok(new SuccessDataResult<PaymentLog>(result.Data, result.Message));
            return BadRequest(result.Message);
        }

        [HttpPost("addPaymentLog")]
        public IActionResult Add(PaymentLog paymentLog)
        {
            var result = _paymentLogManager.Add(paymentLog);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla eklendi !"));
            return BadRequest(result);
        }

        [HttpPost("updatePaymentLog")]
        public IActionResult Update(PaymentLog paymentLog)
        {
            var result = _paymentLogManager.Update(paymentLog);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla güncellendi !"));
            return BadRequest(result);
        }

        [HttpDelete("deletePaymentLog")]
        public IActionResult Delete(PaymentLog paymentLog)
        {
            var result = _paymentLogManager.Delete(paymentLog);
            if (result != null)
                return Ok(new SuccessResult("Başarıyla silindi !"));
            return BadRequest(result);
        }
    }
}
