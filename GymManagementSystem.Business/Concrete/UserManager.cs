﻿using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.DataAccess.Concrete;
using GymManagementSystem.Entities.ComplexTypes;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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

        public async Task<IDataResult<IdentityResult>> Add(AppUser user, UserManager<AppUser> _userManager)
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
                var result = await _userManager.CreateAsync(addUser, addUser.Password);
                if(result.Succeeded)
                    return new SuccessDataResult<IdentityResult>(result, "Başarıyla eklendi !!!");
                else
                    return new ErrorDataResult<IdentityResult>(result, "Bir sorun oluştu !");
            }
            return new ErrorDataResult<IdentityResult>("Bir sorun oluştu !");
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
            var user = _userDal.Get(u => u.Id == id);
            if (user != null)
                return new SuccessDataResult<AppUser>(user, "Başarıyla listelendi !!!");
            else
                return new ErrorDataResult<AppUser>("Kişi bulunamadı !");
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

        public IDataResult<CheckUser> CheckUserStatus(string username, string password)
        {
            var user = _userDal.Get(u => u.UserName == username && u.PasswordHash == password);
            
            if(user != null)
            {
                if ((user.MembershipExpirationDate - DateTime.Now).Days < 0)
                {
                    user.IsPassActive= false;
                    user.PaymentStatusId = 2;
                    _userDal.Update(user);

                    return new ErrorDataResult<CheckUser>("Your membership is expired. Please renew your membership.");
                }
                else
                {
                    if (user.IsActive)
                    {
                        if (user.IsPassActive)
                        {
                            var checkUser = new CheckUser
                            {
                                Name = user.Name,
                                Surname = user.Surname,
                                UserName = user.UserName,
                                EnterCount = user.EnterCount,
                                LastPaymentDate = user.LastPaymentDate,
                                MembershipExpirationDate = user.MembershipExpirationDate,
                                LeftDays = (user.MembershipExpirationDate - DateTime.Now).Days > 0 ? (user.MembershipExpirationDate - DateTime.Now).Days : 0
                            };
                            return new SuccessDataResult<CheckUser>(checkUser, "User authenticated!");
                        }
                        else
                            return new ErrorDataResult<CheckUser>("You must buy membership to enter our gyms.");
                    }
                    else
                        return new ErrorDataResult<CheckUser>("Your membership has been restricted. Please contact with us.");
                    
                }   
            }
            return new ErrorDataResult<CheckUser>("Username or password is not correct.");
        }

        public IDataResult<List<AppUserDetailed>> GetAllUsersWithDetails()
        {
            return new SuccessDataResult<List<AppUserDetailed>>(_userDal.GetAllUsersWithDetails(), "Successfully listed!");
        }

        public IDataResult<AppUserDetailed> GetUserWithDetails(string id)
        {
            return new SuccessDataResult<AppUserDetailed>(_userDal.GetUserWithDetails(id), "Successfully listed!");
        }
    }
}
