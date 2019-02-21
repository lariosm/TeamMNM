namespace DiscussionProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ItemName { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public DateTime TimeStamp { get; set; }

        public bool IsAvailable { get; set; }

        public decimal DailyPrice { get; set; }

        public int OwnerID { get; set; }

        public virtual SUPUser SUPUser { get; set; }
    }
}
