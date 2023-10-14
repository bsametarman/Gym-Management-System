using GymManagementSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Entities.Concrete
{
    public class BloodType : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
