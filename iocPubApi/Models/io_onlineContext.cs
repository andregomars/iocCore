using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iocPubApi.Models
{
    public partial class io_onlineContext : DbContext
    {
        public virtual DbSet<HamsAlertData> HamsAlertData { get; set; }
        public virtual DbSet<HamsAlertItem> HamsAlertItem { get; set; }
        public virtual DbSet<HamsCsv> HamsCsv { get; set; }
        public virtual DbSet<HamsDayTotal> HamsDayTotal { get; set; }
        public virtual DbSet<HamsNetData> HamsNetData { get; set; }
        public virtual DbSet<HamsNetDataItem> HamsNetDataItem { get; set; }
        public virtual DbSet<HamsSmsalertData> HamsSmsalertData { get; set; }
        public virtual DbSet<HamsSmsalertItem> HamsSmsalertItem { get; set; }
        public virtual DbSet<HamsSmsdata> HamsSmsdata { get; set; }
        public virtual DbSet<HamsSmsitem> HamsSmsitem { get; set; }
        public virtual DbSet<IoFleet> IoFleet { get; set; }
        public virtual DbSet<IoUsers> IoUsers { get; set; }
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

            modelBuilder.Entity<HamsCsv>(entity =>
            {
                entity.HasKey(e => e.Csvid)
                    .HasName("HAMS_CSV_PK");
            });

            modelBuilder.Entity<HamsDayTotal>(entity =>
            {
                entity.HasKey(e => new { e.Yymmdd, e.VehicleId })
                    .HasName("HAMS_DayTotal_PK");

                entity.Property(e => e.DataType).HasDefaultValueSql("0");

                entity.Property(e => e.DayHours).HasDefaultValueSql("0");

                entity.Property(e => e.HightCurrent).HasDefaultValueSql("0");

                entity.Property(e => e.HightTmp).HasDefaultValueSql("0");

                entity.Property(e => e.HightVoltage).HasDefaultValueSql("0");

                entity.Property(e => e.KWhCharged).HasDefaultValueSql("0");

                entity.Property(e => e.KWhUsed).HasDefaultValueSql("0");

                entity.Property(e => e.LowCurrent).HasDefaultValueSql("0");

                entity.Property(e => e.LowTmp).HasDefaultValueSql("0");

                entity.Property(e => e.LowVoltage).HasDefaultValueSql("0");

                entity.Property(e => e.Mileage).HasDefaultValueSql("0");

                entity.Property(e => e.SocCharged).HasDefaultValueSql("0");

                entity.Property(e => e.SocUsed).HasDefaultValueSql("0");
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

            modelBuilder.Entity<HamsSmsalertData>(entity =>
            {
                entity.HasKey(e => e.DataId)
                    .HasName("HAMS_SMSAlertData_PK");

                entity.Property(e => e.DataId).ValueGeneratedNever();
            });

            modelBuilder.Entity<HamsSmsalertItem>(entity =>
            {
                entity.HasKey(e => new { e.DataId, e.ItemCode })
                    .HasName("HAMS_SMSAlertItem_PK");
            });

            modelBuilder.Entity<HamsSmsdata>(entity =>
            {
                entity.HasKey(e => e.DataId)
                    .HasName("HAMS_HAMS_SMSData_PK");

                entity.Property(e => e.DataId).ValueGeneratedNever();

                entity.Property(e => e.Gps).HasDefaultValueSql("0");

                entity.Property(e => e.IsDone).HasDefaultValueSql("0");

                entity.Property(e => e.IsMoved).HasDefaultValueSql("0");

                entity.Property(e => e.IsUpdate).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<HamsSmsitem>(entity =>
            {
                entity.HasKey(e => new { e.DataId, e.ItemCode })
                    .HasName("HAMS_SMSItem_PK");

                entity.Property(e => e.Data).HasDefaultValueSql("0");

                entity.Property(e => e.IsCondition).HasDefaultValueSql("0");

                entity.Property(e => e.IsMoved).HasDefaultValueSql("0");

                entity.Property(e => e.IsUpdate).HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<IoFleet>(entity =>
            {
                entity.HasKey(e => e.FleetId)
                    .HasName("IO_Fleet_PK");

                entity.Property(e => e.DayTotal).HasDefaultValueSql("'N'");

                entity.Property(e => e.Icon).HasDefaultValueSql("'Default'");

                entity.Property(e => e.IntervalNet).HasDefaultValueSql("5");

                entity.Property(e => e.IntervalSms).HasDefaultValueSql("5");

                entity.Property(e => e.IsUtc).HasDefaultValueSql("0");

                entity.Property(e => e.LogFormat).HasDefaultValueSql("0");

                entity.Property(e => e.TimeOffset).HasDefaultValueSql("0");

                entity.Property(e => e.Timezone).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<IoUsers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("IO_Users_PK");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("[dbo].[IO_LocalNow_To_UTC]()");
            });

            modelBuilder.Entity<IoVehicle>(entity =>
            {
                entity.HasKey(e => e.VehicleId)
                    .HasName("IO_Vehicle_PK");

                entity.Property(e => e.Online).HasDefaultValueSql("0");

                entity.Property(e => e.Renewable).HasDefaultValueSql("0");

                entity.Property(e => e.Status).HasDefaultValueSql("0");
            });
        }
    }
}