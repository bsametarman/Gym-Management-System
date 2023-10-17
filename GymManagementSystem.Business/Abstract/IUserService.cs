using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
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
        IResult GetByEmailAndPassword(string email, string password);
        IResult Add(AppUser user);
        IResult Update(AppUser user);
        IResult Delete(AppUser user);
    }
}
