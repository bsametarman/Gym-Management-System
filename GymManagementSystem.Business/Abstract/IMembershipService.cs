using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Abstract
{
    public interface IMembershipService
    {
        IDataResult<List<Membership>> GetAll();
        IDataResult<Membership> GetById(int id);
        IResult Add(Membership membership);
        IResult Update(Membership membership);
        IResult Delete(Membership membership);
    }
}
