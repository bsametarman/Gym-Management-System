using GymManagementSystem.Entities.Abstract;
using GymManagementSystem.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Entities.ComplexTypes
{
    public class AppUserDetailed : AppUser, IEntity
    {
        public string MembershipName { get; set; }
        public string BloodTypeName { get; set; }
        public string? TrainerName { get; set; }
        public string? TrainerSurname { get; set; }
        public string PaymentStatusName { get; set; }
    }
}
