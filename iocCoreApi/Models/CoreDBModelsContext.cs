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
            Database.SetInitializer<CoreDBModelsContext>(null);
        }

        public virtual DbSet<Core_Company> Core_Company { get; set; }
        public virtual DbSet<Core_Function> Core_Function { get; set; }
        public virtual DbSet<Core_Permission> Core_Permission { get; set; }
        public virtual DbSet<Core_Role> Core_Role { get; set; }
        public virtual DbSet<Core_User> Core_User { get; set; }
        public virtual DbSet<Core_UserRole> Core_UserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Core_Company>()
                .Property(e => e.CompanyType)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Company>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Company>()
                .Property(e => e.Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Company>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Company>()
                .Property(e => e.Relation)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Company>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Company>()
                .Property(e => e.EditUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Function>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Function>()
                .Property(e => e.EditUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Permission>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_Permission>()
                .Property(e => e.EditUser)
                .IsUnicode(false);

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

            modelBuilder.Entity<Core_User>()
                .Property(e => e.LoginName)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Core_User>()
                .Property(e => e.Mobile)
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

            modelBuilder.Entity<Core_UserRole>()
                .Property(e => e.InUser)
                .IsUnicode(false);

            modelBuilder.Entity<Core_UserRole>()
                .Property(e => e.EditUser)
                .IsUnicode(false);
        }
    }
}
