using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Abstract
{
    public interface IBloodTypeService
    {
        IDataResult<List<BloodType>> GetAll();
        IDataResult<BloodType> GetById(int id);
        IResult Add(BloodType membership);
        IResult Update(BloodType membership);
        IResult Delete(BloodType membership);
    }
}
