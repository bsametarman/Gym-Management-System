using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Abstract
{
    public interface IBranchLogService
    {
        IDataResult<List<BranchLog>> GetAll();
        IDataResult<BranchLog> GetById(int id);
        IResult Add(BranchLog branchLog);
        IResult Update(BranchLog branchLog);
        IResult Delete(BranchLog branchLog);
    }
}
