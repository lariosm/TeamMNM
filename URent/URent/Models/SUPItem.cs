namespace URent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUPItem()
        {
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100), DisplayName("Title")]
        public string ItemName { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [DisplayName("Item availability")]
        public bool IsAvailable { get; set; }

        public int OwnerID { get; set; }

        public virtual SUPUser SUPUser { get; set; }

        [Required]
        public decimal DailyPrice { get; set; }
    }
}
