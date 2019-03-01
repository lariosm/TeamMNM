namespace URent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPTransaction
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public DateTime TimeStamp { get; set; }

        public decimal TotalPrice { get; set; }

        public int RenterID { get; set; }

        public int ItemID { get; set; }

        public virtual SUPItem SUPItem { get; set; }

        public virtual SUPUser SUPUser { get; set; }
    }
}
