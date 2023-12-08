using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.ComplexTypes;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<AppUser>> GetAll();
        IDataResult<AppUser> GetById(string id);
        IDataResult<CheckUser> CheckUserStatus(string username, string password);
        IResult GetByEmailAndPassword(string email, string password);
        Task<IDataResult<IdentityResult>> Add(AppUser user, UserManager<AppUser> userManager);
        IResult Update(AppUser user);
        IResult Delete(AppUser user);
        IDataResult<List<AppUserDetailed>> GetAllUsersWithDetails();
        IDataResult<AppUserDetailed> GetUserWithDetails(string id);
    }
}
