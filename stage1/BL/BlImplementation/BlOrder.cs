using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;



namespace BlImplementation
{
    internal class BlOrder: IOrder
    {
        private DalApi.IDal Dal = new DalList();

        public Order GetOrderDetails(int id)
        {
            Order balOrder=new Order();
            if (id > 0)
            {
                Dal.DO.Order dalOrder = Dal.Order.GetSingle(id);
                // IEnumerable<Dal.DO.OrderItem> orderItems=Dal.OrderItem.Get();




                double totalPrice=0;

              
                balOrder.ID = dalOrder.ID;
                balOrder.CustomerName = dalOrder.CustomerName;
                balOrder.CustomerAddress = dalOrder.CustomerAdress;
                balOrder.CustomerEmail = dalOrder.CustomerEmail;
                balOrder.OrderDate = dalOrder.OrderDate;
                balOrder.ShipDate = dalOrder.ShipDate;
                balOrder.DeliveryDate = dalOrder.DeliveryDate;
                IEnumerable<Dal.DO.OrderItem> orderItemsOfThisOrder = Dal.OrderItem.GetOrderItemByOrderId(id);
                foreach (var item in orderItemsOfThisOrder)
                {

                    if (item.OrderID == dalOrder.ID)
                    {
                        totalPrice += item.Price;
                    }




                    balOrder.TotalPrice = totalPrice;
                   for(int i=0;i< orderItemsOfThisOrder.Count();i++)
                    {
                        balOrder.Items.Add(orderItemsOfThisOrder.);
                    }
                    if (DateTime.Now > dalOrder.OrderDate && DateTime.Now < dalOrder.ShipDate)
                    {
                        balOrder.Status = eOrderStatus.ordered;
                    }
                    if (DateTime.Now > dalOrder.ShipDate && DateTime.Now < dalOrder.DeliveryDate)
                    {
                        balOrder.Status = eOrderStatus.shipped;
                    }
                    else
                    {
                        balOrder.Status = eOrderStatus.delivered;
                    }

                }
            }




            return balOrder;

        }
             
           // throw new NotImplementedException();

           
   

        public IEnumerable<OrderForList> GetOrderList()
        {

            IEnumerable<Dal.DO.Order> orders = Dal.Order.Get();
            IEnumerable<Dal.DO.OrderItem> orderItems = Dal.OrderItem.Get();
            List<OrderForList> Blorders=new List<OrderForList>();
            foreach (Dal.DO.Order Order in orders)
            {
                OrderForList o = new OrderForList();
                o.ID = Order.ID;
                o.CustomerName = Order.CustomerName;
                int counter = 0;
                double totalPrice=0;
                foreach (var item in orderItems)
                {

                    if (item.OrderID == Order.ID)
                    {
                        counter++;
                        totalPrice += item.Price;
                    }

                }
                o.TotalPrice = totalPrice;
                o.AmountOfItems = counter;
                //DateTime date = new DateTime();
                if (DateTime.Now > Order.OrderDate && DateTime.Now < Order.ShipDate)
                {
                    o.Status = eOrderStatus.ordered;
                }
                if (DateTime.Now > Order.ShipDate && DateTime.Now < Order.DeliveryDate)
                {
                    o.Status = eOrderStatus.shipped;
                }
                else
                {
                    o.Status = eOrderStatus.delivered;
                }
                Blorders.Add(o);
            }
            if (Blorders.Count() == 0)
                // throw new ();//לזרוק שגיאה
                return Blorders;

            throw new NotImplementedException();

        }

        public Order update(int id)
        {
            
           

            throw new NotImplementedException();
        }

        public Order updateDeliveryedOrder(int id,DateTime newDate)
        {
            Dal.DO.Order currentOrder = Dal.Order.GetSingle(id);
            if(currentOrder.)
            Order boCurrentOrder = new Order();
            boCurrentOrder.ID = currentOrder.ID;
            boCurrentOrder.CustomerName = currentOrder.CustomerName;
            boCurrentOrder.CustomerAddress = currentOrder.CustomerAdress;
            boCurrentOrder.CustomerEmail = currentOrder.CustomerEmail;
            boCurrentOrder.OrderDate = currentOrder.OrderDate;
            boCurrentOrder.ShipDate = newDate;
            boCurrentOrder.DeliveryDate = currentOrder.DeliveryDate;
            if (DateTime.Now > boCurrentOrder.OrderDate && DateTime.Now < boCurrentOrder.ShipDate)
            {
                boCurrentOrder.Status = eOrderStatus.ordered;
            }
            if (DateTime.Now > boCurrentOrder.ShipDate && DateTime.Now < boCurrentOrder.DeliveryDate)
            {
                boCurrentOrder.Status = eOrderStatus.shipped;
            }
            else
            {
                boCurrentOrder.Status = eOrderStatus.delivered;
            }
            if (boCurrentOrder.Status == eOrderStatus.ordered)
            {
                return boCurrentOrder;
            }

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