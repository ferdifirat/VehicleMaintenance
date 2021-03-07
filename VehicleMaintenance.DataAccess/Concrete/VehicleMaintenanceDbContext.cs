using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleMaintenance.Entity.Concrete;

namespace VehicleMaintenance.DataAccess.Concrete
{
    public class VehicleMaintenanceDbContext : DbContext
    {
        public VehicleMaintenanceDbContext(DbContextOptions<VehicleMaintenanceDbContext> options) : base(options)
        {

        }

        public virtual DbSet<ActionType> ActionType { get; set; }
        public virtual DbSet<Maintenance> Maintenance { get; set; }
        public virtual DbSet<MaintenanceHistory> MaintenanceHistory { get; set; }
        public virtual DbSet<PictureGroup> PictureGroup { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleMaintenanceDbContext).Assembly);
        }
    }
}
