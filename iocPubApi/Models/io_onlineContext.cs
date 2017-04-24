using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iocPubApi.Models
{
    public partial class io_onlineContext : DbContext
    {
        public virtual DbSet<IoFleet> IoFleet { get; set; }
        public virtual DbSet<IoVehicle> IoVehicle { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=io_online;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IoFleet>(entity =>
            {
                entity.HasKey(e => e.FleetId)
                    .HasName("IO_Fleet_PK");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("[dbo].[IO_LocalNow_To_UTC]()");

                entity.Property(e => e.IntervalNet).HasDefaultValueSql("5");

                entity.Property(e => e.IntervalSms).HasDefaultValueSql("5");

                entity.Property(e => e.LogFormat).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("0");

                entity.Property(e => e.TimeOffset).HasDefaultValueSql("0");

                entity.Property(e => e.Timezone).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<IoVehicle>(entity =>
            {
                entity.HasKey(e => e.VehicleId)
                    .HasName("IO_Vehicle_PK");
            });
        }
    }
}