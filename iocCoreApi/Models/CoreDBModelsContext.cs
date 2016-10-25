namespace iocCoreApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CoreDBModelsContext : DbContext
    {
        public CoreDBModelsContext()
            : base("name=IO_OnlineDBConn")
        {
        }

        public virtual DbSet<Core_Function> Core_Function { get; set; }
        public virtual DbSet<Core_Permission> Core_Permission { get; set; }
        public virtual DbSet<Core_Role> Core_Role { get; set; }
        public virtual DbSet<Core_SMS> Core_SMS { get; set; }
        public virtual DbSet<Core_UserRole> Core_UserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Core_SMS>()
                .Property(e => e.MessageID)
                .IsUnicode(false);

            modelBuilder.Entity<Core_SMS>()
                .Property(e => e.SubMessageID)
                .IsUnicode(false);

            modelBuilder.Entity<Core_SMS>()
                .Property(e => e.SMSType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Core_SMS>()
                .Property(e => e.SenderCode)
                .IsUnicode(false);

            modelBuilder.Entity<Core_SMS>()
                .Property(e => e.ReceiverCode)
                .IsUnicode(false);

            modelBuilder.Entity<Core_SMS>()
                .Property(e => e.Status)
                .IsFixedLength()
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
