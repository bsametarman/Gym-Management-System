using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Abstract
{
    public interface IEnterLogService
    {
        IDataResult<List<EnterLog>> GetAll();
        IDataResult<EnterLog> GetById(int id);
        IResult Add(EnterLog enterLog);
        IResult Update(EnterLog enterLog);
        IResult Delete(EnterLog enterLog);
    }
}
