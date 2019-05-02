using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace URent.Models
{
    public class NotificationViewModel
    {
        public List<SUPTransaction> Notifications { get; set; }

        public Dictionary<int, string> RenterNames { get; set; }
    }
}