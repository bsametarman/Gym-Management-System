using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Abstract
{
    public interface IBranchService
    {
        IDataResult<List<Branch>> GetAll();
        IDataResult<Branch> GetById(int id);
        IResult GetByEmailAndPhone(string email, string phone);
        IResult Add(Branch branch);
        IResult Update(Branch branch);
        IResult Delete(Branch branch);
    }
}
