namespace URent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUPUser()
        {
            SUPItems = new HashSet<SUPItem>();
            SUPRequests = new HashSet<SUPRequest>();
            SUPTransactions = new HashSet<SUPTransaction>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Street address")]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "City")]
        public string CityAddress { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "State")]
        public string StateAddress { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [StringLength(128)]
        public string NetUserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPItem> SUPItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPRequest> SUPRequests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPTransaction> SUPTransactions { get; set; }
    }
}
