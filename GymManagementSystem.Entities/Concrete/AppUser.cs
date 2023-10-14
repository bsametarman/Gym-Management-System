using GymManagementSystem.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Entities.Concrete
{
    public class AppUser : IdentityUser, IEntity
    {
        [Key]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string UserRole { get; set; }
        public int MembershipTypeId { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public int BloodTypeId { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        public int PaymentStatusId { get; set; }
        public int EnterCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime MembershipExpirationDate { get; set; }
        public bool IsActive { get; set; }
        

    }
}
