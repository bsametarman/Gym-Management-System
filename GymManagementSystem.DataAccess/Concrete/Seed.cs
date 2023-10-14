using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DataAccess.Concrete
{
    public class Seed
    {
        public static async Task SeedAdminAndRoleAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Owner))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Owner));
                if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
                if (!await roleManager.RoleExistsAsync(UserRoles.Employee))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));
                if (!await roleManager.RoleExistsAsync(UserRoles.Member))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Member));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string ownerUsername = "admin";

                var ownerUser = await userManager.FindByNameAsync(ownerUsername);
                if (ownerUser == null)
                {
                    var newOwnerUser = new AppUser()
                    {
                        UserName = "admin",
                        Name = "admin",
                        Surname = "admin",
                        Address = "turkey",
                        Email = "admin@admin.com",
                        EmailConfirmed = true,
                        EmergencyPhoneNumber = "5554443322",
                        EnterCount = 55,
                        Gender = "Male",
                        IdentityNumber = "11122233344",
                        BloodTypeId = 1,
                        LastPaymentDate = DateTime.Now,
                        MembershipExpirationDate = DateTime.Now.AddDays(100),
                        MembershipTypeId = 1,
                        PaymentStatusId = 1,
                        PhoneNumber = "5554443322",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        UserRole = UserRoles.Owner,
                        Password = "admin*admin"
                    };
                    await userManager.CreateAsync(newOwnerUser, "admin*admin*");
                    await userManager.AddToRoleAsync(newOwnerUser, UserRoles.Owner);

                    var newManagerUser = new AppUser()
                    {
                        UserName = "manager",
                        Name = "manager",
                        Surname = "manager",
                        Address = "turkey",
                        Email = "manager@manager.com",
                        EmailConfirmed = true,
                        EmergencyPhoneNumber = "5554443322",
                        EnterCount = 55,
                        Gender = "Female",
                        IdentityNumber = "11122233345",
                        BloodTypeId = 1,
                        LastPaymentDate = DateTime.Now,
                        MembershipExpirationDate = DateTime.Now.AddDays(100),
                        MembershipTypeId = 1,
                        PaymentStatusId = 1,
                        PhoneNumber = "5554443322",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        UserRole = UserRoles.Manager,
                        Password = "manager*manager",
                    };
                    await userManager.CreateAsync(newManagerUser, "manager*manager");
                    await userManager.AddToRoleAsync(newManagerUser, UserRoles.Manager);

                    var newEmployeeUser = new AppUser()
                    {
                        UserName = "employee",
                        Name = "employee",
                        Surname = "employee",
                        Address = "turkey",
                        Email = "employee@employee.com",
                        EmailConfirmed = true,
                        EmergencyPhoneNumber = "5554443322",
                        EnterCount = 55,
                        Gender = "Male",
                        IdentityNumber = "11122233346",
                        BloodTypeId = 1,
                        LastPaymentDate = DateTime.Now,
                        MembershipExpirationDate = DateTime.Now.AddDays(100),
                        MembershipTypeId = 1,
                        PaymentStatusId = 1,
                        PhoneNumber = "5554443322",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        UserRole = UserRoles.Employee,
                        Password = "employee*employee"
                    };
                    await userManager.CreateAsync(newEmployeeUser, "employee*employee");
                    await userManager.AddToRoleAsync(newEmployeeUser, UserRoles.Employee);

                    var newMemberUser = new AppUser()
                    {
                        UserName = "member",
                        Name = "member",
                        Surname = "member",
                        Address = "turkey",
                        Email = "member@member.com",
                        EmailConfirmed = true,
                        EmergencyPhoneNumber = "5554443322",
                        EnterCount = 55,
                        Gender = "Male",
                        IdentityNumber = "11122233347",
                        BloodTypeId = 1,
                        LastPaymentDate = DateTime.Now,
                        MembershipExpirationDate = DateTime.Now.AddDays(100),
                        MembershipTypeId = 1,
                        PaymentStatusId = 1,
                        PhoneNumber = "5554443322",
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        UserRole = UserRoles.Member,
                        Password = "member*member"
                    };
                    await userManager.CreateAsync(newMemberUser, "member*member");
                    await userManager.AddToRoleAsync(newMemberUser, UserRoles.Member);
                }
            }
        }
    }
}
