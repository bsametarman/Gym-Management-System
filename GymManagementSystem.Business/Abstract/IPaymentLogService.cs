using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Abstract
{
    public interface IPaymentLogService
    {
        IDataResult<List<PaymentLog>> GetAll();
        IDataResult<PaymentLog> GetById(int id);
        IResult Add(PaymentLog paymentLog);
        IResult Update(PaymentLog paymentLog);
        IResult Delete(PaymentLog paymentLog);
    }
}
