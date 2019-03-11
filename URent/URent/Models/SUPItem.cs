namespace URent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUPItem()
        {
            SUPImages = new HashSet<SUPImage>();
            SUPRequests = new HashSet<SUPRequest>();
            SUPTransactions = new HashSet<SUPTransaction>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Item name")]
        public string ItemName { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [Display(Name = "Item availability")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Daily rate")]
        public decimal DailyPrice { get; set; }

        public int OwnerID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPImage> SUPImages { get; set; }

        public virtual SUPUser SUPUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPRequest> SUPRequests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPTransaction> SUPTransactions { get; set; }
    }
}
