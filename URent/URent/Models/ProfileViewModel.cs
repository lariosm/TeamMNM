using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace URent.Models
{
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        [Required]
        public virtual int? UserBeingReviewedID { get; set; }

        [Required]
        public virtual int? UserDoingReviewID { get; set; }

        [Required]
        [Display(Name = "Review Details")]
        public virtual string Details { get; set; }

        public virtual int? Ratings { get; set; }

        public virtual double? RatingAverage { get; set; }

        public virtual int? RatingCount { get; set; }

        //public SUPUser sUPUser { get; set; }

        //public SUPUserReview sUPUserReview { get; set; }

        public IEnumerable<SUPUserReview> sUPUserReviews { get; set; }
    }
}