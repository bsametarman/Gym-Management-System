using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.DataAccess.Concrete;
using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Concrete
{
    public class BranchManager : IBranchService
    {
        private IBranchDal _branchDal;
        public BranchManager(IBranchDal branchDal)
        {
            _branchDal = branchDal;
            
        }
        public IResult Add(Branch branch)
        {
            _branchDal.Add(branch);
            return new SuccessResult("Başarıyla eklendi !!!");
        }

    

        public IResult Delete(Branch branch)
        {
            _branchDal.Delete(branch);
            return new SuccessResult("Başarıyla silindi !!!");
        }

        public IDataResult<List<Branch>> GetAll()
        {
            return new SuccessDataResult<List<Branch>>(_branchDal.GetAll(), "Başarıyla listelendi !!!");
        }

        public IResult GetByEmailAndPhone(string email, string phone)
        {
            var result = _branchDal.GetAll(u => u.Email == email && u.PhoneNumber == phone);

            if (result.Count() != 0)
                return new SuccessResult("Başarıyla getirildi");
            else
                return new ErrorResult("Branş bulunamadı");
        }

        public IDataResult<Branch> GetById(int id)
        {
            return new SuccessDataResult<Branch>(_branchDal.Get(u => u.Id == id), "Başarıyla listelendi !!!");
        }

       

        public IResult Update(Branch branch)
        {
            _branchDal.Update(branch);
            return new SuccessResult("Başarıyla güncellendi !!!");
        }
    }
}
