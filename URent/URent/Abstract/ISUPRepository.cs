using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using URent.Models;

namespace URent.Abstract
{
    public interface ISUPRepository
    {
        IEnumerable<SUPItem> SUPItems { get; }

        IEnumerable<SUPImage> SUPImages { get; }

        IEnumerable<SUPTransaction> SUPTransactions { get; }

        IEnumerable<SUPItemReview> SUPItemReviews { get; }

        IEnumerable<SUPRequest> SUPRequests { get; }

        IEnumerable<SUPUser> SUPUsers { get; }

        IEnumerable<SUPUserReview> SUPUserReviews { get; }
    }

}