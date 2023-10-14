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
        public bool Gender { get; set; }
        public string UserRole { get; set; }
        [ForeignKey("Membership")]
        public int MembershipTypeId { get; set; }
        public Membership Membership { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        [ForeignKey("BloodType")]
        public string BloodTypeId { get; set; }
        public BloodType BloodType { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        [ForeignKey("PaymentStatus")]
        public int PaymentStatusId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int EnterCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime MembershipExpirationDate { get; set; }
        public bool IsActive { get; set; }
        

    }
}
