using GymManagementSystem.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DataAccess.Concrete
{
    public class GymManagementSystemDbContext : IdentityDbContext<AppUser>
    {
        public GymManagementSystemDbContext()
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
        }

        public GymManagementSystemDbContext(DbContextOptions<GymManagementSystemDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = LAPTOP-MBSERMR3\SQLEXPRESS01; Initial Catalog = GymManagementSystemDb; Integrated Security = true; Trust Server Certificate=true; Connection Timeout=3600;");
        }

        public DbSet<AppUser> AspNetUsers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchLog> BranchLogs { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<EnterLog> EnterLogs { get; set; }
        public DbSet<PaymentLog> PaymentLogs { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
    }
}
