namespace URent.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SUPContext : DbContext
    {
        public SUPContext()
            : base("name=SUPUSerTables")
        {
        }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<SUPItem> SUPItems { get; set; }
        public virtual DbSet<SUPUser> SUPUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SUPItem>()
                .Property(e => e.DailyPrice)
                .HasPrecision(18, 0);

            //modelBuilder.Entity<SUPItem>()
            //    .HasMany(e => e.Images)
            //    .WithRequired(e => e.SUPItem)
            //    .HasForeignKey(e => e.ItemID)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPItems)
                .WithRequired(e => e.SUPUser)
                .HasForeignKey(e => e.OwnerID)
                .WillCascadeOnDelete(false);
        }
    }
}
