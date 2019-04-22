namespace URent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPUserReview
    {
        public int Id { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime Timestamp { get; set; }

        public int UserDoingReviewID { get; set; }

        public int UserBeingReviewedID { get; set; }

        public virtual SUPUser SUPUser { get; set; }

        public virtual SUPUser SUPUser1 { get; set; }
    }
}
