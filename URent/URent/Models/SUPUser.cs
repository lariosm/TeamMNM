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
            SUPItemReviews = new HashSet<SUPItemReview>();
            SUPItems = new HashSet<SUPItem>();
            SUPRequests = new HashSet<SUPRequest>();
            SUPTransactions = new HashSet<SUPTransaction>();
            SUPTransactions1 = new HashSet<SUPTransaction>();
            SUPUserReviews = new HashSet<SUPUserReview>();
            SUPUserReviews1 = new HashSet<SUPUserReview>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(128)]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(128)]
        public string CityAddress { get; set; }

        [Required]
        [StringLength(128)]
        public string StateAddress { get; set; }

        [Required]
        [StringLength(128)]
        public string ZipCode { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public DateTime TimeStamp { get; set; }

        [StringLength(128)]
        public string NetUserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPItemReview> SUPItemReviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPItem> SUPItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPRequest> SUPRequests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPTransaction> SUPTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPTransaction> SUPTransactions1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPUserReview> SUPUserReviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPUserReview> SUPUserReviews1 { get; set; }
    }
}
