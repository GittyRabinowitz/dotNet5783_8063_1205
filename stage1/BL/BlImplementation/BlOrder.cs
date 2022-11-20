using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;


namespace BlImplementation
{
    internal class BlOrder: BlApi.IOrder
    {
        private IDal Dal = new DalList();

        public Order GetOrderDetails(int id)
        {
             Order balOrder;
            if (id > 0)
            {
               Dal.DO.Order dalOrder= Dal.Order.GetSingle(id);
                // IEnumerable<Dal.DO.OrderItem> orderItems=Dal.OrderItem.Get();
 



                double totalPrice;

                                 balOrder=new Order();
                balOrder.ID=dalOrder.ID;
                balOrder.CustomerName=dalOrder.CustomerName;
                balOrder.CustomerAddress=dalOrder.CustomerAddress;
                balOrder.CustomerEmail=dalOrder.CustomerEmail;
                balOrder.OrderDate=dalOrder.OrderDate;
                balOrder.ShipDate=dalOrder.ShipDate;
                balOrder.DeliveryDate=dalOrder.DeliveryDate;
              IEnumerable<Dal.DO.OrderItem> orderItemsOfThisOrder= Dal.Order.GetOrderItemByOrderId(id);
                                foreach (var item in orderItemsOfThisOrder)
	{

                    if (item.OrderId == dalOrder.OrderId)
                    {
                     totalPrice+= item.Price;
                    }
                        
	


                                balOrder.TotalPrice=totalPrice;
                balOrder.Items=orderItemsOfThisOrder;
                                    if(date.now>dalOrder.OrderDate && date.now < dalOrder.ShipDate)
                {
                    balOrder.Status=eOrderStatus.ordered;
                }
                  if(date.now>dalOrder.ShipDate && date.now < dalOrder.DeliveryDate)
                {
                    balOrder.Status=eOrderStatus.shipped;
                }
                else
                {
                    balOrder.Status=eOrderStatus.delivered;
                }

            }
}

           

               

              
            }
           
            return balOrder;
        }

        public IEnumerable<OrderForList> GetOrderList()
        {
            IEnumerable<Dal.DO.Order> orders = Dal.Order.Get();
            IEnumerable<Dal.DO.OrderItem> orderItems=Dal.OrderItem.Get();
            List<OrderForList> Blorders;
            foreach (Dal.DO.Order Order in orders)
            {
                OrderForList o=new OrderForList();
                o.ID=Order.ID;
                o.CustomerName=Order.CustomerName;
                int counter=0;
                double totalPrice;
                foreach (var item in orderItems)
	{

                    if (item.OrderId == Order.OrderId)
                    {
                       counter++;
                     totalPrice+= item.Price;
                    }
                        
	}
                o.TotalPrice=totalPrice;
                o.AmountOfItems=counter;
                DateTime date=new DateTime();
                if(date.now>Order.OrderDate && date.now < Order.ShipDate)
                {
                    o.Status=eOrderStatus.ordered;
                }
                  if(date.now>Order.ShipDate && date.now < Order.DeliveryDate)
                {
                    o.Status=eOrderStatus.shipped;
                }
                else
                {
                    o.Status=eOrderStatus.delivered;
                }
              Blorders.Add(o);
            }
            if(Blorders.Count()==0)
               // throw new ();//לזרוק שגיאה
            return Blorders;
            
        }

        public Order update(int id)
        {
            throw new NotImplementedException();
        }

        public Order updateDeliveryedOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Order updateShippedOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}



//public IEnumerable<OrderForList> GetOrderList();//manager
//public Order GetOrderDetails(int id);//manager and customer
//public Order updateShippedOrder(int id);//manager
//public Order updateDeliveryedOrder(int id);//manager
//public Order update(int id);