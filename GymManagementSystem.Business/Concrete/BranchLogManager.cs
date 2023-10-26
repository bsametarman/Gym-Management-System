using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Concrete
{
    internal class BranchLogManager : IBranchLogService
    {
        private IBranchLogDal _branchLogDal;

        public BranchLogManager(IBranchLogDal branchLogDal)
        {
            _branchLogDal = branchLogDal;
        }
        public IResult Add(BranchLog branchLog)
        {
            _branchLogDal.Add(branchLog);
            return new SuccessResult("Başarıyla eklendi !!!");
        }

        public IResult Delete(BranchLog branchLog)
        {
            _branchLogDal.Delete(branchLog);
            return new SuccessResult("Başarıyla silindi !!!");
        }

        public IDataResult<List<BranchLog>> GetAll()
        {
            return new SuccessDataResult<List<BranchLog>>(_branchLogDal.GetAll(), "Başarıyla listelendi!!!");
        }

        public IDataResult<BranchLog> GetById(int id)
        {
            return new SuccessDataResult<BranchLog>(_branchLogDal.Get(bran => bran.Id == id), "Başarıyla listelendi !!!");
        }

        public IResult Update(BranchLog branchLog)
        {
            _branchLogDal.Update(branchLog);
            return new SuccessResult("Başarıyla güncellendi !!!");
        }
    }
}
