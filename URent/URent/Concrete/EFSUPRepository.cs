using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using URent.Abstract;
using URent.Models;

namespace URent.Concrete
{
    public class EFSUPRepository : ISUPRepository
    {
        private SUPContext context = new SUPContext();

        public IEnumerable<SUPItem> SUPItems
        {
            get { return context.SUPItems; }
        }

        public IEnumerable<SUPImage> SUPImages
        {
            get { return context.SUPImages; }
        }

        public IEnumerable<SUPTransaction> SUPTransactions
        {
            get { return context.SUPTransactions; }
        }

        public IEnumerable<SUPItemReview> SUPItemReviews
        {
            get { return context.SUPItemReviews; }
        }

        public IEnumerable<SUPRequest> SUPRequests
        {
            get { return context.SUPRequests; }
        }

        public IEnumerable<SUPUser> SUPUsers
        {
            get { return context.SUPUsers; }
        }

        public IEnumerable<SUPUserReview> SUPUserReviews
        {
            get { return context.SUPUserReviews; }
        }
        
    }
}