namespace DiscussionHub.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Discussion
    {
        public int ID { get; set; }

        [Required, DisplayName("Thread title")]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, DisplayName("Link to article")]
        [StringLength(256)]
        public string URL { get; set; }

        [DisplayName("Author")]
        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
