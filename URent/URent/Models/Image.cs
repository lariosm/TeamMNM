namespace URent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Filename { get; set; }

        [Required]
        public byte[] Input { get; set; }

        //public int ItemID { get; set; }

        public virtual SUPItem SUPItem { get; set; }
    }
}
