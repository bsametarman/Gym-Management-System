using GymManagementSystem.Core.DataAccess.EntityFramework;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.Entities.ComplexTypes;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<AppUser, GymManagementSystemDbContext>, IUserDal
    {
        public List<AppUserDetailed> GetAllUsersWithDetails()
        {
            using (GymManagementSystemDbContext context = new GymManagementSystemDbContext())
            {
                var UsersWithMemberships = (from user in context.AspNetUsers
                                            join membership in context.Memberships on user.MembershipTypeId equals membership.Id
                                            select new AppUserDetailed
                                            {
                                                Id = user.Id,
                                                Name = user.Name,
                                                Surname= user.Surname,
                                                UserName = user.UserName,
                                                Password = user.Password,
                                                Email = user.Email,
                                                Gender = user.Gender,
                                                YearOfBirth = user.YearOfBirth,
                                                UserRole = user.UserRole,
                                                IdentityNumber = user.IdentityNumber,
                                                Address = user.Address,
                                                MembershipTypeId = membership.Id,
                                                MembershipName = membership.Name,
                                                BloodTypeId = user.BloodTypeId,
                                                BloodTypeName = null,
                                                TrainerId = user.TrainerId,
                                                TrainerName = null,
                                                TrainerSurname = null,
                                                PaymentStatusId = user.PaymentStatusId,
                                                PaymentStatusName = null,
                                                PhoneNumber = user.PhoneNumber,
                                                EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                                                EnterCount = user.EnterCount,
                                                CreatedDate = user.CreatedDate,
                                                LastPaymentDate = user.LastPaymentDate,
                                                MembershipExpirationDate = user.MembershipExpirationDate,
                                                IsActive = user.IsActive,
                                                IsPassActive = user.IsPassActive
                                            }).ToList();

                var UsersWithBloodTypes = (from user in UsersWithMemberships
                                           join bloodType in context.BloodTypes on user.BloodTypeId equals bloodType.Id
                                           select new AppUserDetailed
                                           {
                                               Id = user.Id,
                                               Name = user.Name,
                                               Surname = user.Surname,
                                               UserName = user.UserName,
                                               Password = user.Password,
                                               Email = user.Email,
                                               Gender = user.Gender,
                                               YearOfBirth = user.YearOfBirth,
                                               UserRole = user.UserRole,
                                               IdentityNumber = user.IdentityNumber,
                                               Address = user.Address,
                                               MembershipTypeId = user.MembershipTypeId,
                                               MembershipName = user.MembershipName,
                                               BloodTypeId = bloodType.Id,
                                               BloodTypeName = bloodType.Name,
                                               TrainerId = user.TrainerId,
                                               TrainerName = null,
                                               TrainerSurname = null,
                                               PaymentStatusId = user.PaymentStatusId,
                                               PaymentStatusName = null,
                                               PhoneNumber = user.PhoneNumber,
                                               EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                                               EnterCount = user.EnterCount,
                                               CreatedDate = user.CreatedDate,
                                               LastPaymentDate = user.LastPaymentDate,
                                               MembershipExpirationDate = user.MembershipExpirationDate,
                                               IsActive = user.IsActive,
                                               IsPassActive = user.IsPassActive
                                           }).ToList();
                var UsersWithTrainers = (from user in UsersWithBloodTypes
                                           join trainer in context.Trainers on user.TrainerId equals trainer.Id
                                           select new AppUserDetailed
                                           {
                                               Id = user.Id,
                                               Name = user.Name,
                                               Surname = user.Surname,
                                               UserName = user.UserName,
                                               Password = user.Password,
                                               Email = user.Email,
                                               Gender = user.Gender,
                                               YearOfBirth = user.YearOfBirth,
                                               UserRole = user.UserRole,
                                               IdentityNumber = user.IdentityNumber,
                                               Address = user.Address,
                                               MembershipTypeId = user.MembershipTypeId,
                                               MembershipName = user.MembershipName,
                                               BloodTypeId = user.BloodTypeId,
                                               BloodTypeName = user.BloodTypeName,
                                               TrainerId = trainer.Id,
                                               TrainerName = trainer.Name,
                                               TrainerSurname = trainer.Surname,
                                               PaymentStatusId = user.PaymentStatusId,
                                               PaymentStatusName = null,
                                               PhoneNumber = user.PhoneNumber,
                                               EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                                               EnterCount = user.EnterCount,
                                               CreatedDate = user.CreatedDate,
                                               LastPaymentDate = user.LastPaymentDate,
                                               MembershipExpirationDate = user.MembershipExpirationDate,
                                               IsActive = user.IsActive,
                                               IsPassActive = user.IsPassActive
                                           }).ToList();
                var UsersWithPaymentsAndAllDetails = (from user in UsersWithTrainers
                                         join payment in context.PaymentStatuses on user.PaymentStatusId equals payment.Id
                                         select new AppUserDetailed
                                         {
                                             Id = user.Id,
                                             Name = user.Name,
                                             Surname = user.Surname,
                                             UserName = user.UserName,
                                             Password = user.Password,
                                             Email = user.Email,
                                             Gender = user.Gender,
                                             YearOfBirth = user.YearOfBirth,
                                             UserRole = user.UserRole,
                                             IdentityNumber = user.IdentityNumber,
                                             Address = user.Address,
                                             MembershipTypeId = user.MembershipTypeId,
                                             MembershipName = user.MembershipName,
                                             BloodTypeId = user.BloodTypeId,
                                             BloodTypeName = user.BloodTypeName,
                                             TrainerId = user.TrainerId,
                                             TrainerName = user.TrainerName,
                                             TrainerSurname = user.TrainerSurname,
                                             PaymentStatusId = payment.Id,
                                             PaymentStatusName = payment.Name,
                                             PhoneNumber = user.PhoneNumber,
                                             EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                                             EnterCount = user.EnterCount,
                                             CreatedDate = user.CreatedDate,
                                             LastPaymentDate = user.LastPaymentDate,
                                             MembershipExpirationDate = user.MembershipExpirationDate,
                                             IsActive = user.IsActive,
                                             IsPassActive = user.IsPassActive
                                         }).ToList();

                return UsersWithPaymentsAndAllDetails.ToList();
            }
        }

        public AppUserDetailed GetUserWithDetails(string id)
        {
            using (GymManagementSystemDbContext context = new GymManagementSystemDbContext())
            {
                var UserWithMembership = (from user in context.AspNetUsers where user.Id == id
                                            join membership in context.Memberships on user.MembershipTypeId equals membership.Id
                                            select new AppUserDetailed
                                            {
                                                Id = user.Id,
                                                Name = user.Name,
                                                Surname = user.Surname,
                                                UserName = user.UserName,
                                                Password = user.Password,
                                                Email = user.Email,
                                                Gender = user.Gender,
                                                YearOfBirth = user.YearOfBirth,
                                                UserRole = user.UserRole,
                                                IdentityNumber = user.IdentityNumber,
                                                Address = user.Address,
                                                MembershipTypeId = membership.Id,
                                                MembershipName = membership.Name,
                                                BloodTypeId = user.BloodTypeId,
                                                BloodTypeName = null,
                                                TrainerId = user.TrainerId,
                                                TrainerName = null,
                                                TrainerSurname = null,
                                                PaymentStatusId = user.PaymentStatusId,
                                                PaymentStatusName = null,
                                                PhoneNumber = user.PhoneNumber,
                                                EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                                                EnterCount = user.EnterCount,
                                                CreatedDate = user.CreatedDate,
                                                LastPaymentDate = user.LastPaymentDate,
                                                MembershipExpirationDate = user.MembershipExpirationDate,
                                                IsActive = user.IsActive,
                                                IsPassActive = user.IsPassActive
                                            });

                var UserWithBloodType = (from user in UserWithMembership
                                           join bloodType in context.BloodTypes on user.BloodTypeId equals bloodType.Id
                                           select new AppUserDetailed
                                           {
                                               Id = user.Id,
                                               Name = user.Name,
                                               Surname = user.Surname,
                                               UserName = user.UserName,
                                               Password = user.Password,
                                               Email = user.Email,
                                               Gender = user.Gender,
                                               YearOfBirth = user.YearOfBirth,
                                               UserRole = user.UserRole,
                                               IdentityNumber = user.IdentityNumber,
                                               Address = user.Address,
                                               MembershipTypeId = user.MembershipTypeId,
                                               MembershipName = user.MembershipName,
                                               BloodTypeId = bloodType.Id,
                                               BloodTypeName = bloodType.Name,
                                               TrainerId = user.TrainerId,
                                               TrainerName = null,
                                               TrainerSurname = null,
                                               PaymentStatusId = user.PaymentStatusId,
                                               PaymentStatusName = null,
                                               PhoneNumber = user.PhoneNumber,
                                               EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                                               EnterCount = user.EnterCount,
                                               CreatedDate = user.CreatedDate,
                                               LastPaymentDate = user.LastPaymentDate,
                                               MembershipExpirationDate = user.MembershipExpirationDate,
                                               IsActive = user.IsActive,
                                               IsPassActive = user.IsPassActive
                                           });
                var UserWithTrainer = (from user in UserWithBloodType
                                         join trainer in context.Trainers on user.TrainerId equals trainer.Id
                                         select new AppUserDetailed
                                         {
                                             Id = user.Id,
                                             Name = user.Name,
                                             Surname = user.Surname,
                                             UserName = user.UserName,
                                             Password = user.Password,
                                             Email = user.Email,
                                             Gender = user.Gender,
                                             YearOfBirth = user.YearOfBirth,
                                             UserRole = user.UserRole,
                                             IdentityNumber = user.IdentityNumber,
                                             Address = user.Address,
                                             MembershipTypeId = user.MembershipTypeId,
                                             MembershipName = user.MembershipName,
                                             BloodTypeId = user.BloodTypeId,
                                             BloodTypeName = user.BloodTypeName,
                                             TrainerId = trainer.Id,
                                             TrainerName = trainer.Name,
                                             TrainerSurname = trainer.Surname,
                                             PaymentStatusId = user.PaymentStatusId,
                                             PaymentStatusName = null,
                                             PhoneNumber = user.PhoneNumber,
                                             EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                                             EnterCount = user.EnterCount,
                                             CreatedDate = user.CreatedDate,
                                             LastPaymentDate = user.LastPaymentDate,
                                             MembershipExpirationDate = user.MembershipExpirationDate,
                                             IsActive = user.IsActive,
                                             IsPassActive = user.IsPassActive
                                         });
                var UserWithPaymentAndAllDetails = (from user in UserWithTrainer
                                                      join payment in context.PaymentStatuses on user.PaymentStatusId equals payment.Id
                                                      select new AppUserDetailed
                                                      {
                                                          Id = user.Id,
                                                          Name = user.Name,
                                                          Surname = user.Surname,
                                                          UserName = user.UserName,
                                                          Password = user.Password,
                                                          Email = user.Email,
                                                          Gender = user.Gender,
                                                          YearOfBirth = user.YearOfBirth,
                                                          UserRole = user.UserRole,
                                                          IdentityNumber = user.IdentityNumber,
                                                          Address = user.Address,
                                                          MembershipTypeId = user.MembershipTypeId,
                                                          MembershipName = user.MembershipName,
                                                          BloodTypeId = user.BloodTypeId,
                                                          BloodTypeName = user.BloodTypeName,
                                                          TrainerId = user.TrainerId,
                                                          TrainerName = user.TrainerName,
                                                          TrainerSurname = user.TrainerSurname,
                                                          PaymentStatusId = payment.Id,
                                                          PaymentStatusName = payment.Name,
                                                          PhoneNumber = user.PhoneNumber,
                                                          EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                                                          EnterCount = user.EnterCount,
                                                          CreatedDate = user.CreatedDate,
                                                          LastPaymentDate = user.LastPaymentDate,
                                                          MembershipExpirationDate = user.MembershipExpirationDate,
                                                          IsActive = user.IsActive,
                                                          IsPassActive = user.IsPassActive
                                                      });

                return UserWithPaymentAndAllDetails.FirstOrDefault();
            }
        }
    }
}
