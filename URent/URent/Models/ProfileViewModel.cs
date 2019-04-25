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
        public virtual int? UserBeingReviewedId { get; set; }

        //[Required]
        //public virtual string UserDoingReviewId { get; set; }

        [Required]
        [Display(Name = "Review Details")]
        public virtual string Details { get; set; }

        //public SUPUser sUPUser { get; set; }

        //public SUPUserReview sUPUserReview { get; set; }

        //public IEnumerable<SUPUserReview> sUPUserReviews { get; set; }
    }
}