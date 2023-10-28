using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Abstract
{
    public interface IPaymentStatus
    {
        IDataResult<List<PaymentStatus>> GetAll();
        IDataResult<PaymentStatus> GetById(int id);
        IResult Add(PaymentStatus paymentStatus);
        IResult Update(PaymentStatus paymentStatus);
        IResult Delete(PaymentStatus paymentStatus);
    }
}
