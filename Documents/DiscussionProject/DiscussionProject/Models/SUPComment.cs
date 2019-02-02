namespace DiscussionProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPComment
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime TIMESTAMP { get; set; }

        public int UserID { get; set; }

        public int DiscussionID { get; set; }

        public virtual SUPDiscussion SUPDiscussion { get; set; }

        public virtual SUPUser SUPUser { get; set; }
    }
}
