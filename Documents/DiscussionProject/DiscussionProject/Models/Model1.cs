namespace DiscussionProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<SUPItem> SUPItems { get; set; }
        public virtual DbSet<SUPUser> SUPUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SUPItem>()
                .Property(e => e.DailyPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPItems)
                .WithRequired(e => e.SUPUser)
                .HasForeignKey(e => e.OwnerID)
                .WillCascadeOnDelete(false);
        }
    }
}
