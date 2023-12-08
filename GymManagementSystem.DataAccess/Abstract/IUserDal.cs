using GymManagementSystem.Core;
using GymManagementSystem.Entities.ComplexTypes;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<AppUser>
    {
        List<AppUserDetailed> GetAllUsersWithDetails();
        AppUserDetailed GetUserWithDetails(string id);
    }
}
