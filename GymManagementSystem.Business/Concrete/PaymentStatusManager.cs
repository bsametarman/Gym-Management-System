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
    public class PaymentStatusManager : IPaymentStatusService
    {
        private IPaymentStatusDal _paymentStatusDal;

        public PaymentStatusManager(IPaymentStatusDal paymentStatusDal)
        {
            _paymentStatusDal = paymentStatusDal;
        }

        public IResult Add(PaymentStatus paymentStatus)
        {
            _paymentStatusDal.Add(paymentStatus);
            return new SuccessResult("Başarıyla eklendi !");
        }

        public IResult Delete(PaymentStatus paymentStatus)
        {
            _paymentStatusDal.Delete(paymentStatus);
            return new SuccessResult("Başarıyla silindi !");
        }

        public IDataResult<List<PaymentStatus>> GetAll()
        {
            var result = _paymentStatusDal.GetAll();
            if (result.Count != 0)
                return new SuccessDataResult<List<PaymentStatus>>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<List<PaymentStatus>>(result, "Bir hata oluştu !");
        }

        public IDataResult<PaymentStatus> GetById(int id)
        {
            var result = _paymentStatusDal.Get(payment => payment.Id == id);
            if (result != null)
                return new SuccessDataResult<PaymentStatus>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<PaymentStatus>(result, "Bir hata oluştu !");
        }

        public IResult Update(PaymentStatus paymentStatus)
        {
            _paymentStatusDal.Update(paymentStatus);
            return new SuccessResult("Başarıyla güncellendi !");
        }
    }
}
