using Rideally.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Data.EFRepository
{
    class RideallyEFContext : DbContext
    {
        public RideallyEFContext()
            : base("name=DefaultConnection")
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Schedule>().HasRequired(p => p.FromAddress).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Schedule>().HasRequired(p => p.ToAddress).WithMany().WillCascadeOnDelete(false);
            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<Rideally.Entities.Employee> Employees { set; get; }
        public DbSet<Rideally.Entities.Authentication> Authentications { get; set; }
        public DbSet<Rideally.Entities.Address> Address { get; set; }
        public DbSet<Rideally.Entities.Brand> Brands { get; set; }
        public DbSet<Rideally.Entities.RiderMaster> RiderMasters { get; set; }
        public DbSet<Rideally.Entities.Schedule> Schedules { get; set; }
        public DbSet<Rideally.Entities.EmployeeVehicle> EmployeeVehicles { get; set; }
        public DbSet<Rideally.Entities.VehicleType> VehicleTypes { get; set; }
        public DbSet<Rideally.Entities.VehicleTypeMaster> VehicleTypeMasters { get; set; }
        public DbSet<Rideally.Entities.Vehicle> Vehicle { get; set; }
        public DbSet<Rideally.Entities.Notification> Notification { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>;
        //}
    }
}
