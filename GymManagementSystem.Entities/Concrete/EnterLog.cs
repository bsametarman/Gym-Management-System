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
    public class EnterLog : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BranchId { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime? ExitDate { get; set; }
    }
}
