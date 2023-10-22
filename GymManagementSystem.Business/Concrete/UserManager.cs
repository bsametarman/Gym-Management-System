using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.DataAccess.Concrete;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
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
        //private UserManager<AppUser> _userManager;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
            //_userManager = userManager;
        }

        public async Task<IResult> Add(AppUser user, UserManager<AppUser> _userManager)
        {
            var findUser = _userManager.FindByNameAsync(user.UserName);

            if (findUser.Result == null)
            {
                string userRoleToAssign = user.UserRole;

                AppUser addUser = new AppUser
                {
                    Name = user.UserName,
                    Surname = user.Surname,
                    UserName = user.UserName,
                    Password = user.Password,
                    Address = user.Address,
                    Email = user.Email,
                    Gender = user.Gender,
                    IdentityNumber = user.IdentityNumber,
                    EnterCount = 0,
                    BloodTypeId = user.BloodTypeId,
                    EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                    LastPaymentDate = user.LastPaymentDate,
                    MembershipExpirationDate = user.MembershipExpirationDate,
                    MembershipTypeId = user.MembershipTypeId,
                    PhoneNumber = user.PhoneNumber,
                    PaymentStatusId = user.PaymentStatusId,
                    TrainerId = user.TrainerId,
                    YearOfBirth = user.YearOfBirth,
                    CreatedDate = DateTime.Now,
                    UserRole = userRoleToAssign,
                    IsActive = true
                };
                await _userManager.CreateAsync(user, user.Password);
            }
            return new SuccessResult("Başarıyla eklendi !!!");
            //_userDal.Add(user);
            //return new SuccessResult("Başarıyla eklendi !!!");
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
