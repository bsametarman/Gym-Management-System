using GymManagementSystem.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Entities.Concrete
{
	public class CheckUser : IEntity
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string UserName { get; set; }
		public int EnterCount { get; set; }
        public int LeftDays { get; set; }
        public DateTime LastPaymentDate { get; set; }
		public DateTime MembershipExpirationDate { get; set; }
	}
}
