using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Abstract
{
    public interface ITrainerService
    {
        IDataResult<List<Trainer>> GetAll();
        IDataResult<Trainer> GetById(int id);
        IResult Add(Trainer trainer);
        IResult Update(Trainer trainer);
        IResult Delete(Trainer trainer);
    }
}
