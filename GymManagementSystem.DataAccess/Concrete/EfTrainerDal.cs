using GymManagementSystem.Core.DataAccess.EntityFramework;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DataAccess.Concrete
{
    public class EfTrainerDal : EfEntityRepositoryBase<Trainer, GymManagementSystemDbContext>, ITrainerDal
    {
    }
}
