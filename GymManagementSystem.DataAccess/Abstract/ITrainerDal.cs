using GymManagementSystem.Core;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DataAccess.Abstract
{
    public interface ITrainerDal : IEntityRepository<Trainer>
    {
    }
}
