namespace URent.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUPItemRate
    {
        public int Id { get; set; }

        public int? HourlyRate { get; set; }

        public int? DailyRate { get; set; }

        public int? WeeklyRate { get; set; }

        public int? MonthlyRate { get; set; }

        public int ItemID { get; set; }

        public virtual SUPItem SUPItem { get; set; }
    }
}
