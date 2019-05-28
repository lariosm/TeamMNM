using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace URent.Models
{
    public class AllReviewsModel
    {
        public virtual int iId { get; set; }
        public virtual string iName { get; set; }
        public virtual IEnumerable<SUPItemReview> iReviews { get; set; }
        public virtual int uId { get; set; }
        public virtual string uName { get; set; }
        public virtual IEnumerable<SUPUserReview> uReviews { get; set; }

    }
}