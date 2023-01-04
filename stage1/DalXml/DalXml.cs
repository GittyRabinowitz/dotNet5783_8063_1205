using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        private static Lazy<IDal>? instance;
        public static IDal Instance { get { return GetInstence(); } }

        public static IDal GetInstence()
        {
            lock (instance ??= new Lazy<IDal>(() => new DalXml())) // thread safe
            {
                return instance.Value;
            }
        }


        private DalXml() { }
        public IProduct Product { get; } = new Dal.DalProduct();
        public IOrder Order { get; } = new Dal.DalOrder();
        public IOrderItem OrderItem { get; } = new Dal.DalOrderItem();
    }
}
