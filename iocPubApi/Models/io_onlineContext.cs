using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iocPubApi.Models
{
    public partial class io_onlineContext : DbContext
    {
        public virtual DbSet<HamsAlertData> HamsAlertData { get; set; }
        public virtual DbSet<HamsAlertItem> HamsAlertItem { get; set; }
        public virtual DbSet<HamsNetData> HamsNetData { get; set; }
        public virtual DbSet<HamsNetDataItem> HamsNetDataItem { get; set; }
        public virtual DbSet<IoFleet> IoFleet { get; set; }
        public virtual DbSet<IoVehicle> IoVehicle { get; set; }

        public io_onlineContext(DbContextOptions<io_onlineContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HamsAlertData>(entity =>
            {
                entity.HasKey(e => e.DataId)
                    .HasName("HAMS_AlertData_PK");

                entity.Property(e => e.DataId).ValueGeneratedNever();
            });

            modelBuilder.Entity<HamsNetData>(entity =>
            {
                entity.HasKey(e => e.DataId)
                    .HasName("HAMS_NetData_PK");

                entity.Property(e => e.DataId).ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasDefaultValueSql("[dbo].[IO_LocalNow_To_UTC]()");

                entity.Property(e => e.DataType).HasDefaultValueSql("0");

                entity.Property(e => e.Gps).HasDefaultValueSql("0");

                entity.Property(e => e.IsView).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<HamsNetDataItem>(entity =>
            {
                entity.Property(e => e.DataType).HasDefaultValueSql("0");

                entity.Property(e => e.Source).HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasDefaultValueSql("0");
            });

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