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
    internal class BlOrder : IOrder
    {
        private DalApi.IDal Dal = new DalList();


        public IEnumerable<OrderForList> GetOrderList()
        {
            IEnumerable<Dal.DO.Order> orders = Dal.Order.Get();
            List<BO.OrderForList> orderList = new List<BO.OrderForList>();
            foreach (var order in orders)
            {
                BO.OrderForList orderForList = new BO.OrderForList();
                orderForList.ID = order.ID;
                orderForList.CustomerName = order.CustomerName;
                orderForList.TotalPrice = 0;
                orderForList.AmountOfItems = 0;
                var orderItems = Dal.OrderItem.GetOrderItemByOrderId(order.ID);
                foreach (var oi in orderItems)
                {
                    orderForList.TotalPrice += oi.Price * oi.Amount;
                    orderForList.AmountOfItems++;

                   //או oi.amount לא לעשות רק פלוס אחד???
                }
                if (order.DeliveryDate > DateTime.MinValue)
                    orderForList.Status = BO.eOrderStatus.delivered;
                else if (order.ShipDate > DateTime.MinValue)
                    orderForList.Status = BO.eOrderStatus.shipped;
                else
                    orderForList.Status = BO.eOrderStatus.ordered;
                orderList.Add(orderForList);
            }
            return orderList;
        }

        public Order GetOrderDetails(int id)
        {
            Order BOOrder = new Order();
            Dal.DO.Order DOOrder;
            IEnumerable<Dal.DO.OrderItem> DOOrderItems;
            if (id <= 0)
                throw new BlInvalideData("id cant be negative");
            try
            {
                DOOrder = Dal.Order.GetSingle(id);
                DOOrderItems = Dal.OrderItem.GetOrderItemByOrderId(id);

            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);

            }


            BOOrder.ID = DOOrder.ID;
            BOOrder.CustomerName = DOOrder.CustomerName;
            BOOrder.CustomerAddress = DOOrder.CustomerAdress;
            BOOrder.CustomerEmail = DOOrder.CustomerEmail;
            BOOrder.OrderDate = DOOrder.OrderDate;
            BOOrder.ShipDate = DOOrder.ShipDate;
            BOOrder.DeliveryDate = DOOrder.DeliveryDate;


            if (DateTime.Now > DOOrder.OrderDate && DateTime.Now < DOOrder.ShipDate)
            {
                BOOrder.Status = eOrderStatus.ordered;
            }
            if (DateTime.Now > DOOrder.ShipDate && DateTime.Now < DOOrder.DeliveryDate)
            {
                BOOrder.Status = eOrderStatus.shipped;
            }
            else
            {
                BOOrder.Status = eOrderStatus.delivered;
            }


            foreach (var oi in DOOrderItems)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = oi.ID;
                orderItem.ProductID = oi.ProductID;
                orderItem.Name = Dal.Product.GetSingle(oi.ProductID).Name;
                orderItem.Amount = oi.Amount;
                orderItem.Price = oi.Price;
                orderItem.TotalPrice =oi.Price*oi.Amount;
                BOOrder.Items.Add(orderItem);

                BOOrder.TotalPrice+=orderItem.TotalPrice;
            }


            return BOOrder;

        }

        public Order updateShippedOrder(int orderId)
        {
            Dal.DO.Order DoOrder;
            try
            {
                DoOrder = Dal.Order.GetSingle(orderId);

            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);

            }
            if (DoOrder.ShipDate != DateTime.MinValue)
                throw new BlNoNeedToUpdateException();



            DoOrder.ShipDate = DateTime.Now;
            Dal.Order.Update(DoOrder);



            BO.Order BoOrder = new BO.Order();

            BoOrder.ID = DoOrder.ID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAddress = DoOrder.CustomerAdress;
            BoOrder.Status = BO.eOrderStatus.shipped;
            BoOrder.OrderDate = DoOrder.OrderDate;
            BoOrder.ShipDate = DateTime.Now;
            BoOrder.DeliveryDate = DateTime.MinValue;
            BoOrder.TotalPrice = 0;

            var DoOrderItems = Dal.OrderItem.GetOrderItemByOrderId(orderId);
            foreach (var oi in DoOrderItems)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = oi.ID;
                orderItem.ProductID = oi.ProductID;
                orderItem.Name = Dal.Product.GetSingle(oi.ProductID).Name;
                orderItem.Amount = oi.Amount;
                orderItem.Price = oi.Price;
                orderItem.TotalPrice = oi.Amount * oi.Price;
                BoOrder.TotalPrice += orderItem.TotalPrice;
                BoOrder.Items.Add(orderItem);
            }
            return BoOrder;
        }


        public Order updateDeliveryedOrder(int orderId)
        {
            Dal.DO.Order DoOrder;
            try
            {
                DoOrder = Dal.Order.GetSingle(orderId);

            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);

            }

            if (DoOrder.ShipDate == DateTime.MinValue)
                throw new BlDeliveredBeforeShippedException();
            if (DoOrder.DeliveryDate != DateTime.MinValue)
                throw new BlNoNeedToUpdateException();

            DoOrder.DeliveryDate = DateTime.Now;
            Dal.Order.Update(DoOrder);

            BO.Order BoOrder = new BO.Order();

            BoOrder.ID = DoOrder.ID;
            BoOrder.CustomerName = DoOrder.CustomerName;
            BoOrder.CustomerEmail = DoOrder.CustomerEmail;
            BoOrder.CustomerAddress = DoOrder.CustomerAdress;
            BoOrder.OrderDate = DoOrder.OrderDate;
            BoOrder.ShipDate = DoOrder.ShipDate;
            BoOrder.DeliveryDate = DateTime.Now;
            BoOrder.Status = BO.eOrderStatus.delivered;
            BoOrder.TotalPrice = 0;

            IEnumerable<Dal.DO.OrderItem> DoOrderItems = Dal.OrderItem.GetOrderItemByOrderId(orderId);
            foreach (var oi in DoOrderItems)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = oi.ID;
                orderItem.ProductID = oi.ProductID;
                orderItem.Name = Dal.Product.GetSingle(oi.ProductID).Name;
                orderItem.Amount = oi.Amount;
                orderItem.Price = oi.Price;
                orderItem.TotalPrice = oi.Amount * oi.Price;

                BoOrder.TotalPrice += orderItem.TotalPrice;

                BoOrder.Items.Add(orderItem);
            }
            return BoOrder;
        }
        public Order update(int id)
        {

            //בונוס

            throw new NotImplementedException();
        }
    }
}

