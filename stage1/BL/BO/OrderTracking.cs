using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int ID { get; set; }
        public eOrderStatus Status { get; set; }

        public List<(DateTime, eOrderStatus)> DateAndTrack { get; set; }//איך מדפיסים את זה??????
        public override string ToString() => $@"Order ID={ID}: 
Status - {Status}, 
";
    }
}
