using GymManagementSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Entities.Concrete
{
    public class Trainer : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public int BloodTypeId { get; set; }
        public int BranchId { get; set; }
        public string EmergencyPhoneNumber { get; set; }
        public int EnterCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public int Salary { get; set; }
        public string YearOfExperience { get; set; }
    }
}
