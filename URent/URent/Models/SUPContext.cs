using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace URent.Models
{
    

    public partial class SUPContext : DbContext
    {
        public SUPContext()
            : base("name=URentDB")
        {
            Database.SetInitializer<SUPContext>(null);
        }

        public virtual DbSet<SUPImage> SUPImages { get; set; }
        public virtual DbSet<SUPItemReview> SUPItemReviews { get; set; }
        public virtual DbSet<SUPItem> SUPItems { get; set; }
        public virtual DbSet<SUPRequest> SUPRequests { get; set; }
        public virtual DbSet<SUPTransaction> SUPTransactions { get; set; }
        public virtual DbSet<SUPUserReview> SUPUserReviews { get; set; }
        public virtual DbSet<SUPUser> SUPUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SUPItem>()
                .Property(e => e.DailyPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SUPItem>()
                .HasMany(e => e.SUPImages)
                .WithOptional(e => e.SUPItem)
                .HasForeignKey(e => e.ItemID);

            modelBuilder.Entity<SUPItem>()
                .HasMany(e => e.SUPItemReviews)
                .WithRequired(e => e.SUPItem)
                .HasForeignKey(e => e.ItemBeingReviewedID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPItem>()
                .HasMany(e => e.SUPRequests)
                .WithRequired(e => e.SUPItem)
                .HasForeignKey(e => e.ItemID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPItem>()
                .HasMany(e => e.SUPTransactions)
                .WithRequired(e => e.SUPItem)
                .HasForeignKey(e => e.ItemID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPTransaction>()
                .Property(e => e.TotalPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPItemReviews)
                .WithRequired(e => e.SUPUser)
                .HasForeignKey(e => e.UserDoingReviewID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPItems)
                .WithRequired(e => e.SUPUser)
                .HasForeignKey(e => e.OwnerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPRequests)
                .WithRequired(e => e.SUPUser)
                .HasForeignKey(e => e.RenterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPTransactions)
                .WithRequired(e => e.SUPUser)
                .HasForeignKey(e => e.OwnerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPTransactions1)
                .WithRequired(e => e.SUPUser1)
                .HasForeignKey(e => e.RenterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPUserReviews)
                .WithRequired(e => e.SUPUser)
                .HasForeignKey(e => e.UserBeingReviewedID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPUserReviews1)
                .WithRequired(e => e.SUPUser1)
                .HasForeignKey(e => e.UserDoingReviewID)
                .WillCascadeOnDelete(false);
        }
    }
}
