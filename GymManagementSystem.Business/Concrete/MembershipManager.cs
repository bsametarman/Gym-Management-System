using GymManagementSystem.Business.Abstract;
using GymManagementSystem.Core.Utilities.Results;
using GymManagementSystem.DataAccess.Abstract;
using GymManagementSystem.DataAccess.Concrete;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Business.Concrete
{
    public class MembershipManager : IMembershipService
    {
        private IMembershipDal _membershipDal;

        public MembershipManager(IMembershipDal membershipDal)
        {
            _membershipDal = membershipDal;
        }

        public IResult Add(Membership membership)
        {
            _membershipDal.Add(membership);
            return new SuccessResult("Başarıyla eklendi !");
        }

        public IResult Delete(Membership membership)
        {
            _membershipDal.Delete(membership);
            return new SuccessResult("Başarıyla silindi !");
        }

        public IDataResult<List<Membership>> GetAll()
        {
            var result = _membershipDal.GetAll();
            if (result.Count != 0)
                return new SuccessDataResult<List<Membership>>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<List<Membership>>(result, "Bir hata oluştu !");
        }

        public IDataResult<Membership> GetById(int id)
        {
            var result = _membershipDal.Get(membership => membership.Id == id);
            if (result != null)
                return new SuccessDataResult<Membership>(result, "Başarıyla listelendi !");
            return new ErrorDataResult<Membership>(result, "Bir hata oluştu !");
        }

        public IResult Update(Membership membership)
        {
            _membershipDal.Update(membership);
            return new SuccessResult("Başarıyla güncellendi !");
        }
    }
}
