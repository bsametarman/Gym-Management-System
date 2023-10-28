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
    public class BloodTypeManager : IBloodTypeService
    {
        private IBloodTypeDal _bloodTypeDal;

        public BloodTypeManager(IBloodTypeDal bloodTypeDal)
        {
            _bloodTypeDal = bloodTypeDal;
        }

        public IResult Add(BloodType bloodType)
        {
            _bloodTypeDal.Add(bloodType);
            return new SuccessResult("Başarıyla eklendi !");
        }

        public IResult Delete(BloodType bloodType)
        {
            _bloodTypeDal.Delete(bloodType);
            return new SuccessResult("Başarıyla silindi !");
        }

        public IDataResult<List<BloodType>> GetAll()
        {
            var result = _bloodTypeDal.GetAll();
            if (result.Count != 0)
                return new SuccessDataResult<List<BloodType>>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<List<BloodType>>(result, "Bir hata oluştu !");
        }

        public IDataResult<BloodType> GetById(int id)
        {
            var result = _bloodTypeDal.Get(bloodType => bloodType.Id == id);
            if (result != null)
                return new SuccessDataResult<BloodType>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<BloodType>(result, "Bir hata oluştu !");
        }

        public IResult Update(BloodType bloodType)
        {
            _bloodTypeDal.Update(bloodType);
            return new SuccessResult("Başarıyla güncellendi !");
        }
    }
}
