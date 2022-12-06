using BlApi;
using Dal;


namespace BlImplementation;

internal class BlOrder : IOrder
{
    private DalApi.IDal Dal = new DalList();




    /// <summary>
    /// this function reads the list of orders
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlNoEntitiesFoundInDal"></exception>
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

                //var orderItems = Dal.OrderItem.GetOrderItemByOrderId(order.ID);
                List<Dal.DO.OrderItem> orderItems = Dal.OrderItem.Get(oi => oi.OrderID == order.ID).ToList();


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
        catch (DalApi.DalIdNotFoundException exc)
        {

            throw new BO.BlNoEntitiesFoundInDal(exc);
        }
    }



    /// <summary>
    /// this function reads the properties of a specific order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlInvalideData"></exception>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public BO.Order GetOrderDetails(int id)
    {
        try
        {
            BO.Order BoOrder = new BO.Order();

            Dal.DO.Order DoOrder = new Dal.DO.Order();

            if (id <= 0)
                throw new BO.BlInvalideData("id cant be negative");

            //DoOrder = Dal.Order.GetSingle(id);
            DoOrder = Dal.Order.Get(o => o.ID == id).First();
            //IEnumerable<Dal.DO.OrderItem> DoOrderItems = Dal.OrderItem.GetOrderItemByOrderId(id);
            IEnumerable<Dal.DO.OrderItem> DoOrderItems = Dal.OrderItem.Get(oi=>oi.OrderID== id).ToList();


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


            BoOrder.Items = new List<BO.OrderItem>();
            foreach (Dal.DO.OrderItem DoOrderItem in DoOrderItems)
            {
                BO.OrderItem BoOrderItem = new BO.OrderItem();

                BoOrderItem.ID = BO.BoConfig.OrderItemID;
                BoOrderItem.ProductID = DoOrderItem.ProductID;
                BoOrderItem.Name = Dal.Product.GetSingle(DoOrderItem.ProductID).Name;
                BoOrderItem.Amount = DoOrderItem.Amount;
                BoOrderItem.Price = DoOrderItem.Price;
                BoOrderItem.TotalPrice = DoOrderItem.Price * DoOrderItem.Amount;

                BoOrder.Items.Add(BoOrderItem);

                BoOrder.TotalPrice += BoOrderItem.TotalPrice;
            }


            return BoOrder;
        }
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);

        }

    }



    /// <summary>
    /// updates order as sent 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlUpdateException"></exception>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public BO.Order updateShippedOrder(int orderId)
    {
        try
        {

            // Dal.DO.Order DoOrder = Dal.Order.GetSingle(orderId);

            Dal.DO.Order DoOrder = Dal.Order.Get(o => o.ID == orderId).First();

            if (DoOrder.ShipDate != DateTime.MinValue)
                throw new BO.BlUpdateException("The order has already been updated");

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

            //IEnumerable<Dal.DO.OrderItem> DoOrderItems = Dal.OrderItem.GetOrderItemByOrderId(orderId);
            IEnumerable<Dal.DO.OrderItem> DoOrderItems = Dal.OrderItem.Get(oi=>oi.OrderID==orderId).ToList();



            BoOrder.Items = new List<BO.OrderItem>();

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
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);

        }
    }



    /// <summary>
    /// updates order as delivered
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlUpdateException"></exception>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public BO.Order updateDeliveryedOrder(int orderId)
    {
        try
        {
            // Dal.DO.Order DoOrder = Dal.Order.GetSingle(orderId);

            Dal.DO.Order DoOrder = Dal.Order.Get(o => o.ID == orderId).First();

            if (DoOrder.ShipDate == DateTime.MinValue)
                throw new BO.BlUpdateException("The order delivered before shipping");
            if (DoOrder.DeliveryDate != DateTime.MinValue)
                throw new BO.BlUpdateException("The order has already been updated");

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

            //IEnumerable<Dal.DO.OrderItem> DoOrderItems = Dal.OrderItem.GetOrderItemByOrderId(orderId);

            IEnumerable<Dal.DO.OrderItem> DoOrderItems = Dal.OrderItem.Get(oi=>oi.OrderID==orderId).ToList();


            BoOrder.Items = new List<BO.OrderItem>();

            foreach (var oi in DoOrderItems)
            {
                BO.OrderItem BoOrderItem = new BO.OrderItem();

                BoOrderItem.ID = BO.BoConfig.OrderItemID;
                BoOrderItem.ProductID = oi.ProductID;
                // BoOrderItem.Name = Dal.Product.GetSingle(oi.ProductID).Name;
                BoOrderItem.Name = Dal.Product.Get(p=>p.ID==oi.ProductID).First().Name;

                BoOrderItem.Amount = oi.Amount;
                BoOrderItem.Price = oi.Price;
                BoOrderItem.TotalPrice = oi.Amount * oi.Price;

                BoOrder.TotalPrice += BoOrderItem.TotalPrice;

                BoOrder.Items.Add(BoOrderItem);
            }

            return BoOrder;

        }
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);

        }
    }


    /// <summary>
    /// the function gets an order id ,gets the order from the data layer and creates with its details an orderTracking object and returns it
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public BO.OrderTracking orderTracking(int orderId)
    {
        try
        {


            //Dal.DO.Order order = Dal.Order.GetSingle(orderId);

            Dal.DO.Order order = Dal.Order.Get(o => o.ID == orderId).First();

            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.DateAndTrack = new List<(DateTime, BO.eOrderStatus)>();

            orderTracking.ID = BO.BoConfig.OrderTrackingID;
            orderTracking.DateAndTrack.Add(((DateTime)order.OrderDate, BO.eOrderStatus.ordered));


            if (order.ShipDate != DateTime.MinValue)
            {
                orderTracking.Status = BO.eOrderStatus.shipped;

                orderTracking.DateAndTrack.Add(((DateTime)order.ShipDate, BO.eOrderStatus.shipped));
            }


            if (order.DeliveryDate != DateTime.MinValue)
            {
                orderTracking.Status = BO.eOrderStatus.delivered;
                orderTracking.DateAndTrack.Add(((DateTime)order.DeliveryDate, BO.eOrderStatus.delivered));

            }

            return orderTracking;
        }
        catch (DalApi.DalIdNotFoundException exc)
        {

            throw new BO.BlIdNotExist(exc);
        }
    }
}


