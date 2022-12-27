using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal.UseObjects;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        public static IDal Instance { get; } = new DalList();
        private DalList() { }
        public IProduct Product => new DalProduct() { };
        public IOrder Order => new DalOrder() { };
        public IOrderItem OrderItem => new DalOrderItem() { };
    }
}
