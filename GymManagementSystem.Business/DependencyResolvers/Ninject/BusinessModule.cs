using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Business.Concrete;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {

            Bind<DbContext>().To<GymManagementSystemDbContext>().InSingletonScope();

            Bind<IUserService>().To<UserManager>().InSingletonScope();
            Bind<IUserDal>().To<EfUserDal>().InSingletonScope();

            Bind<IBranchLogService>().To<BranchLogManager>().InSingletonScope();
            Bind<IBranchLogDal>().To<EfBranchLogDal>().InSingletonScope();

            Bind<IEnterLogService>().To<EnterLogManager>().InSingletonScope();
            Bind<IEnterLogDal>().To<EfEnterLogDal>().InSingletonScope();

            Bind<IBranchService>().To<BranchManager>().InSingletonScope();
            Bind<IBranchDal>().To<EfBranchDal>().InSingletonScope();

            Bind<IPaymentStatusService>().To<PaymentStatusManager>().InSingletonScope();
            Bind<IPaymentStatusDal>().To<EfPaymentStatusDal>().InSingletonScope();
        }
    }
}
