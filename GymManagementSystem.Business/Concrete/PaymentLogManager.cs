using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.DataAccess.Concrete;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Concrete
{
    public class PaymentLogManager : IPaymentLogService
    {
        private IPaymentLogDal _paymentLogDal;

        public PaymentLogManager(IPaymentLogDal paymentLogDal)
        {
            _paymentLogDal = paymentLogDal;
        }

        public IResult Add(PaymentLog paymentLog)
        {
            _paymentLogDal.Add(paymentLog);
            return new SuccessResult("Başarıyla eklendi !");
        }

        public IResult Delete(PaymentLog paymentLog)
        {
            _paymentLogDal.Delete(paymentLog);
            return new SuccessResult("Başarıyla silindi !");
        }

        public IDataResult<List<PaymentLog>> GetAll()
        {
            var result = _paymentLogDal.GetAll();
            if (result.Count != 0)
                return new SuccessDataResult<List<PaymentLog>>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<List<PaymentLog>>(result, "Bir hata oluştu !");
        }

        public IDataResult<PaymentLog> GetById(int id)
        {
            var result = _paymentLogDal.Get(paymentLog => paymentLog.Id == id);
            if (result != null)
                return new SuccessDataResult<PaymentLog>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<PaymentLog>(result, "Bir hata oluştu !");
        }

        public IResult Update(PaymentLog paymentLog)
        {
            _paymentLogDal.Update(paymentLog);
            return new SuccessResult("Başarıyla güncellendi !");
        }
    }
}
