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
    public class BranchLog : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Income { get; set; }
        public string Expense { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public string Description { get; set; }
    }
}
