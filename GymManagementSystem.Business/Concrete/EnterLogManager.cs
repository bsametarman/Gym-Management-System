using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Concrete
{
    public class EnterLogManager : IEnterLogService
    {
        private IEnterLogDal _enterLogDal;

        public EnterLogManager(IEnterLogDal enterLogDal)
        {
            _enterLogDal = enterLogDal;
        }

        public IResult Add(EnterLog enterLog)
        {
            _enterLogDal.Add(enterLog);
            return new SuccessResult("Başarıyla eklendi !");
        }

        public IResult Delete(EnterLog enterLog)
        {
            _enterLogDal.Delete(enterLog);
            return new SuccessResult("Başarıyla silindi !");
        }

        public IDataResult<List<EnterLog>> GetAll()
        {
            var result = _enterLogDal.GetAll();
            if (result.Count != 0)
                return new SuccessDataResult<List<EnterLog>>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<List<EnterLog>>(result, "Bir hata oluştu !");
        }

        public IDataResult<EnterLog> GetById(int id)
        {
            var result = _enterLogDal.Get(log => log.Id == id);
            if (result != null)
                return new SuccessDataResult<EnterLog>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<EnterLog>(result, "Bir hata oluştu !");
        }

        public IResult Update(EnterLog enterLog)
        {
            _enterLogDal.Update(enterLog);
            return new SuccessResult("Başarıyla güncellendi !");
        }
    }
}
