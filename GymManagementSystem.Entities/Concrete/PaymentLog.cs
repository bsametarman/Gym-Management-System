using GymManagementSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Entities.Concrete
{
    public class PaymentLog : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MembershipId { get; set; }
        public int Price { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
