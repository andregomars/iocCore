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

        public virtual DbSet<Core_Role> core_role { get; set; }
        public virtual DbSet<Core_User> core_user { get; set; }
        public virtual DbSet<Core_UserRole> core_UserRole { get; set; }
        public virtual DbSet<Core_Permission> core_Permission { get; set; }
        public virtual DbSet<Core_Function> core_Function { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Core_Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Role>()
                .Property(e => e.RoleType)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Role>()
                .Property(e => e.RoleDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Role>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Role>()
                .Property(e => e.EditUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Role>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.LoginName)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.UserType)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.CellPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.WorkPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.HeadImage)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.EditUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Core_UserRole>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_UserRole>()
                .Property(e => e.EditUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Permission>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Permission>()
                .Property(e => e.EditUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Function>()
                .Property(e => e.FunctionName)
                .IsUnicode(true);

            modelBuilder.Entity<Core_Function>()
                .Property(e => e.FunctionDescription)
                .IsUnicode(true);

            modelBuilder.Entity<Core_Function>()
                .Property(e => e.FunctionType)
                .IsUnicode(true);

            modelBuilder.Entity<Core_Function>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Function>()
                .Property(e => e.EditUser)
                .IsUnicode(false);
        }
    }
}
