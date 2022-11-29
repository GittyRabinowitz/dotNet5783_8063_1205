using BlApi;
using Dal;



namespace BlImplementation
{
    internal class BlOrder : IOrder
    {
        private DalApi.IDal Dal = new DalList();


        public IEnumerable<BO.OrderForList> GetOrderList()
        {
            try
            {
                IEnumerable<Dal.DO.Order> DoOrders = Dal.Order.Get();
                List<BO.OrderForList> orderList = new List<BO.OrderForList>();

                foreach (Dal.DO.Order order in DoOrders)
                {
                    BO.OrderForList orderForList = new BO.OrderForList();
                    orderForList.ID = BO.BoConfig.OrderForListID;
                    orderForList.CustomerName = order.CustomerName;
                    orderForList.TotalPrice = 0;
                    orderForList.AmountOfItems = 0;
                    var orderItems = Dal.OrderItem.GetOrderItemByOrderId(order.ID);
                    foreach (var oi in orderItems)
                    {
                        orderForList.TotalPrice += oi.Price * oi.Amount;
                        orderForList.AmountOfItems++;
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
            catch (DalApi.DalEntityNotFoundException exc)
            {

                throw new BO.BlNoEntitiesFoundInDal(exc);
            }
        }

        public BO.Order GetOrderDetails(int id)
        {
            try
            {

                BO.Order BoOrder = new BO.Order();
                Dal.DO.Order DoOrder = new Dal.DO.Order();
                IEnumerable<Dal.DO.OrderItem> DoOrderItems;
                if (id <= 0)
                    throw new BO.BlInvalideData("id cant be negative");

                DoOrder = Dal.Order.GetSingle(id);
                DoOrderItems = Dal.OrderItem.GetOrderItemByOrderId(id);


                BoOrder.ID = BO.BoConfig.OrderID;
                BoOrder.CustomerName = DoOrder.CustomerName;
                BoOrder.CustomerAddress = DoOrder.CustomerAdress;
                BoOrder.CustomerEmail = DoOrder.CustomerEmail;
                BoOrder.OrderDate = DoOrder.OrderDate;
                BoOrder.ShipDate = DoOrder.ShipDate;
                BoOrder.DeliveryDate = DoOrder.DeliveryDate;

                if (DoOrder.DeliveryDate > DateTime.MinValue)
                    BoOrder.Status = BO.eOrderStatus.delivered;
                else if (DoOrder.ShipDate > DateTime.MinValue)
                    BoOrder.Status = BO.eOrderStatus.shipped;
                else
                    BoOrder.Status = BO.eOrderStatus.ordered;

                BoOrder.Items=new List<BO.OrderItem>();
                foreach (Dal.DO.OrderItem DoOrderItem in DoOrderItems)
                {
                    BO.OrderItem BoOrderItem = new BO.OrderItem();
                    BoOrderItem.ID = BO.BoConfig.OrderItemID;
                    BoOrderItem.ProductID = DoOrderItem.ProductID;
                    BoOrderItem.Name = Dal.Product.GetSingle(DoOrderItem.ProductID).Name;
                    BoOrderItem.Amount = DoOrderItem.Amount;
                    BoOrderItem.Price = DoOrderItem.Price;
                    BoOrderItem.TotalPrice =DoOrderItem.Price*DoOrderItem.Amount;
                    
                    BoOrder.Items.Add(BoOrderItem);

                    BoOrder.TotalPrice+=BoOrderItem.TotalPrice;
                }


                return BoOrder;
            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);

            }

        }

        public BO.Order updateShippedOrder(int orderId)
        {
            try
            {

                Dal.DO.Order DoOrder = Dal.Order.GetSingle(orderId);
                if (DoOrder.ShipDate != DateTime.MinValue)
                    throw new BO.BlNoNeedToUpdateException();

                DoOrder.ShipDate = DateTime.Now;
                Dal.Order.Update(DoOrder);

                BO.Order BoOrder = new BO.Order();

                BoOrder.ID = BO.BoConfig.OrderID;
                BoOrder.CustomerName = DoOrder.CustomerName;
                BoOrder.CustomerEmail = DoOrder.CustomerEmail;
                BoOrder.CustomerAddress = DoOrder.CustomerAdress;
                BoOrder.Status = BO.eOrderStatus.shipped;
                BoOrder.OrderDate = DoOrder.OrderDate;
                BoOrder.ShipDate = DateTime.Now;
                BoOrder.DeliveryDate = DateTime.MinValue;
                BoOrder.TotalPrice = 0;

                var DoOrderItems = Dal.OrderItem.GetOrderItemByOrderId(orderId);

                BoOrder.Items=new List<BO.OrderItem>();
                foreach (var oi in DoOrderItems)
                {
                    BO.OrderItem BoOrderItem = new BO.OrderItem();
                    BoOrderItem.ID = BO.BoConfig.OrderItemID;
                    BoOrderItem.ProductID = oi.ProductID;
                    BoOrderItem.Name = Dal.Product.GetSingle(oi.ProductID).Name;
                    BoOrderItem.Amount = oi.Amount;
                    BoOrderItem.Price = oi.Price;
                    BoOrderItem.TotalPrice = oi.Amount * oi.Price;
                    BoOrder.TotalPrice += BoOrderItem.TotalPrice;
                    BoOrder.Items.Add(BoOrderItem);
                }
                return BoOrder;
            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);

            }
        }


        public BO.Order updateDeliveryedOrder(int orderId)
        {
            try
            {
                Dal.DO.Order DoOrder = Dal.Order.GetSingle(orderId);


                if (DoOrder.ShipDate == DateTime.MinValue)
                    throw new BO.BlDeliveredBeforeShippedException();
                if (DoOrder.DeliveryDate != DateTime.MinValue)
                    throw new BO.BlNoNeedToUpdateException();

                DoOrder.DeliveryDate = DateTime.Now;
                Dal.Order.Update(DoOrder);

                BO.Order BoOrder = new BO.Order();

                BoOrder.ID = BO.BoConfig.OrderID;
                BoOrder.CustomerName = DoOrder.CustomerName;
                BoOrder.CustomerEmail = DoOrder.CustomerEmail;
                BoOrder.CustomerAddress = DoOrder.CustomerAdress;
                BoOrder.OrderDate = DoOrder.OrderDate;
                BoOrder.ShipDate = DoOrder.ShipDate;
                BoOrder.DeliveryDate = DateTime.Now;
                BoOrder.Status = BO.eOrderStatus.delivered;
                BoOrder.TotalPrice = 0;

                IEnumerable<Dal.DO.OrderItem> DoOrderItems = Dal.OrderItem.GetOrderItemByOrderId(orderId);
                BoOrder.Items=new List<BO.OrderItem>();
                foreach (var oi in DoOrderItems)
                {
                    BO.OrderItem BoOrderItem = new BO.OrderItem();
                    BoOrderItem.ID = BO.BoConfig.OrderItemID;
                    BoOrderItem.ProductID = oi.ProductID;
                    BoOrderItem.Name = Dal.Product.GetSingle(oi.ProductID).Name;
                    BoOrderItem.Amount = oi.Amount;
                    BoOrderItem.Price = oi.Price;
                    BoOrderItem.TotalPrice = oi.Amount * oi.Price;

                    BoOrder.TotalPrice += BoOrderItem.TotalPrice;

                    BoOrder.Items.Add(BoOrderItem);
                }
                return BoOrder;

            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);

            }
        }
        public BO.Order update(int id)
        {

            //בונוס

            throw new NotImplementedException();
        }
    }
}

