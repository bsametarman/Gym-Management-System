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
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(AppUser user)
        {
            _userDal.Add(user);
            return new SuccessResult("Başarıyla eklendi !!!");
        }

        public IResult Delete(AppUser user)
        {
            _userDal.Delete(user);
            return new SuccessResult("Başarıyla silindi !!!");
        }

        public IDataResult<AppUser> GetById(string id)
        {
            return new SuccessDataResult<AppUser>(_userDal.Get(u => u.Id == id), "Başarıyla listelendi !!!");
        }

        public IResult GetByEmailAndPassword(string email, string password)
        {
            var result = _userDal.GetAll(u => u.Email == email && u.Password == password);

            if (result.Count() != 0)
                return new SuccessResult("Başarıyla getirildi !!!");
            else
                return new ErrorResult("Kişi bulunumadı!!!");
        }

        public IDataResult<List<AppUser>> GetAll()
        {
            return new SuccessDataResult<List<AppUser>>(_userDal.GetAll(), "Başarıyla listelendi !!!");
        }

        public IResult Update(AppUser user)
        {
            _userDal.Update(user);
            return new SuccessResult("Başarıyla güncellendi !!!");
        }
    }
}
