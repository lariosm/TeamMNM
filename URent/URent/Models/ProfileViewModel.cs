using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace URent.Models
{
    public class ProfileViewModel
    {
        public SUPUser sUPUser { get; set; }

        public IEnumerable<SUPUserReview> sUPUserReviews { get; set; }
    }
}