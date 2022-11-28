using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public static class BoConfig
    {
        private static int orderID = 99999;
        public static int OrderID { get { orderID++; return orderID; } }


        private static int orderForListID = 99999;
        public static int OrderForListID { get { orderForListID++; return orderForListID; } }




        private static int orderItemID = 99999;
        public static int OrderItemID { get { orderItemID++; return orderItemID; } }


        private static int orderTrackingID = 99999;
        public static int OrderTrackingID { get { orderTrackingID++; return orderTrackingID; } }


        private static int productID = 99999;
        public static int ProductID { get { productID++; return productID; } }



        private static int productForListID = 99999;
        public static int ProductForListID { get { productForListID++; return productForListID; } }



        private static int productItemID = 99999;
        public static int ProductItemID { get { productItemID++; return productItemID; } }
    }
}
