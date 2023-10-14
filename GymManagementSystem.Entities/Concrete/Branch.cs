using GymManagementSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Entities.Concrete
{
    public class Branch : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string SquareMeters { get; set; }
        public string WeekdaysOpeningTime { get; set; }
        public string WeekdaysClosingTime { get; set; }
        public string WeekendsOpeningTime { get; set; }
        public string WeekendsClosingTime { get; set; }
        public int Capacity { get; set; }
        public int NumberOfTools { get; set; }
        public int NumberOfMembers { get; set; }
        public int NumberOfEmployee { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
