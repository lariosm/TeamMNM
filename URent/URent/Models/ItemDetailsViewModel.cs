using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace URent.Models
{
    public class ItemDetailsViewModel
    {
        public SUPItem sUPItem { get; set; }

        [Required]
        public virtual int? ItemBeingReviewedID { get; set; }

        [Required]
        public virtual int? UserDoingReviewID { get; set; }

        [Required]
        [Display(Name = "Reviews")]
        public virtual string Details { get; set; }

        public virtual int? Ratings { get; set; }

        public virtual int? RatingCount { get; set; }

        public virtual double? RatingAverage { get; set; }


        public IEnumerable<SUPItemReview> sUPItemReviews { get; set; }

    }
}