namespace iocCoreApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CoreDBModelsContext : DbContext
    {
        public CoreDBModelsContext()
            : base("name=CoreDBConn")
        {
        }

        public virtual DbSet<core_role> core_role { get; set; }
        public virtual DbSet<core_user> core_user { get; set; }
        public virtual DbSet<core_UserRole> core_UserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<core_role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<core_role>()
                .Property(e => e.RoleType)
                .IsUnicode(false);

            modelBuilder.Entity<core_role>()
                .Property(e => e.RoleDescription)
                .IsUnicode(false);

            modelBuilder.Entity<core_role>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<core_role>()
                .Property(e => e.EditUser)
                .IsUnicode(false);

            modelBuilder.Entity<core_role>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.LoginName)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.UserType)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.CellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.WorkPhone)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.HeadImage)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.EditUser)
                .IsUnicode(false);

            modelBuilder.Entity<core_user>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<core_UserRole>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<core_UserRole>()
                .Property(e => e.EditUser)
                .IsUnicode(false);
        }
    }
}
