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
    public class TrainerManager : ITrainerService
    {
        private ITrainerDal _trainerDal;

        public TrainerManager(ITrainerDal trainerDal)
        {
            _trainerDal = trainerDal;
        }

        public IResult Add(Trainer trainer)
        {
            _trainerDal.Add(trainer);
            return new SuccessResult("Başarıyla eklendi !");
        }

        public IResult Delete(Trainer trainer)
        {
            _trainerDal.Delete(trainer);
            return new SuccessResult("Başarıyla silindi !");
        }

        public IDataResult<List<Trainer>> GetAll()
        {
            var result = _trainerDal.GetAll();
            if (result.Count != 0)
                return new SuccessDataResult<List<Trainer>>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<List<Trainer>>(result, "Bir hata oluştu !");
        }

        public IDataResult<Trainer> GetById(int id)
        {
            var result = _trainerDal.Get(log => log.Id == id);
            if (result != null)
                return new SuccessDataResult<Trainer>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<Trainer>(result, "Bir hata oluştu !");
        }

        public IResult Update(Trainer trainer)
        {
            _trainerDal.Update(trainer);
            return new SuccessResult("Başarıyla güncellendi !");
        }
    }
}
