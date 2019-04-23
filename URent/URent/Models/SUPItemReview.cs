namespace URent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPItemReview
    {
        public int Id { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        public int UserDoingReviewID { get; set; }

        public int ItemBeingReviewedID { get; set; }

        public virtual SUPItem SUPItem { get; set; }

        public virtual SUPUser SUPUser { get; set; }
    }
}
