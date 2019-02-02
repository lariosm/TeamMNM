namespace DiscussionProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SUPContext : DbContext
    {
        public SUPContext()
            : base("name=ClassProjectDB")
        {
        }

        public virtual DbSet<SUPComment> SUPComments { get; set; }
        public virtual DbSet<SUPDiscussion> SUPDiscussions { get; set; }
        public virtual DbSet<SUPUser> SUPUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SUPDiscussion>()
                .HasMany(e => e.SUPComments)
                .WithRequired(e => e.SUPDiscussion)
                .HasForeignKey(e => e.DiscussionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPComments)
                .WithRequired(e => e.SUPUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUPUser>()
                .HasMany(e => e.SUPDiscussions)
                .WithRequired(e => e.SUPUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);
        }
    }
}
