using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DiscussionHub.Models;

namespace DiscussionHub.DAL
{
    public class DiscussionContext : DbContext
    {

        public DiscussionContext()
            : base("name=ClassProjectDB")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Discussion> Discussions { get; set; }
    }
}