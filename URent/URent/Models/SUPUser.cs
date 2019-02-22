namespace URent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUPUser()
        {
            SUPItems = new HashSet<SUPItem>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100), DisplayName("First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100), DisplayName("Last name")]
        public string LastName { get; set; }

        [Column(TypeName = "date"), DisplayName("Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(128), DisplayName("Street address")]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(128), DisplayName("City")]
        public string CityAddress { get; set; }

        [Required]
        [StringLength(128), DisplayName("State")]
        public string StateAddress { get; set; }

        [Required]
        [StringLength(128), DisplayName("Zip code")]
        public string ZipCode { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        [StringLength(128)]
        public string NetUserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUPItem> SUPItems { get; set; }
    }
}
