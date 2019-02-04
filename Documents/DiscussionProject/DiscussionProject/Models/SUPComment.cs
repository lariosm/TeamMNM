namespace DiscussionProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPComment
    {
        public int ID { get; set; }

        [Required, DisplayName("Comment title")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        [DisplayName("Created on")]
        public DateTime TIMESTAMP { get; set; } = DateTime.Now;

        [DisplayName("User")]
        public int UserID { get; set; }

        [DisplayName("Discussion thread")]
        public int DiscussionID { get; set; }

        public virtual SUPDiscussion SUPDiscussion { get; set; }

        public virtual SUPUser SUPUser { get; set; }
    }
}
