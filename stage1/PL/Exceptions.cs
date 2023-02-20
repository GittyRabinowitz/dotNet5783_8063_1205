using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class PlInvalideData : Exception
    {
        public PlInvalideData(string messege) : base(messege) { }
    }
}
