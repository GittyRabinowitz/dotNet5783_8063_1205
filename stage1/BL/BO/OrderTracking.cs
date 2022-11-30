using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public eOrderStatus Status { get; set; }

    public List<(DateTime, eOrderStatus)> DateAndTrack { get; set; }

    public override string ToString()
    {
        string toString =
            $@"order ID={ID},
Status - {Status},
items:";
        foreach (var i in DateAndTrack) { toString += "\n \t " + i.Item1 + "\t" + i.Item2; };
        return toString;
    }
}

