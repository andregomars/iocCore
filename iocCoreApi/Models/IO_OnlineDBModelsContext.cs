namespace iocCoreApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class IO_OnlineDBModelsContext : DbContext
    {
        public IO_OnlineDBModelsContext()
            : base("name=IO_OnlineDBConn")
        {
        }

        public virtual DbSet<IO_Company> IO_Company { get; set; }
        public virtual DbSet<IO_Users> IO_Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IO_Company>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Company>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Company>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Company>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Company>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Company>()
                .Property(e => e.Tel)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Company>()
                .Property(e => e.Relation)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Users>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Users>()
                .Property(e => e.LogName)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Users>()
                .Property(e => e.Pwd)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Users>()
                .Property(e => e.Tel)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Users>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<IO_Users>()
                .Property(e => e.HeadImage)
                .IsUnicode(false);
        }
    }
}
