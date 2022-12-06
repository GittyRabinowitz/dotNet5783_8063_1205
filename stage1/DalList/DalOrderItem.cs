
using Dal.DO;
using DalApi;
namespace Dal.UseObjects;

/// <summary>
/// class for crud actions for an order item
/// </summary>
/// 
internal class DalOrderItem:IOrderItem
{

    /// <summary>
    /// create function gets an object and insert it into the order items array
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>the object's id</returns>
    /// <exception cref="NotImplementedException"></exception>
    public int Add(OrderItem obj)
    {
        obj.ID = DataSource.Config.OrderItemID;
        DataSource.OrderItemList.Add(obj);
        return obj.ID;
    }


    /// <summary>
    /// delete function gets an id of the object requsted to be deleted and deletes it from the order items array
    /// </summary>
    /// <param name="Id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Delete(int Id)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.OrderItemList.Count(); i++)
        {
            if (DataSource.OrderItemList[i].ID == Id)
            {
                DataSource.OrderItemList.Remove(DataSource.OrderItemList[i]);
                flag = false;
            }
        }
        if (flag)
            throw new DalIdNotFoundException("this order item does not exist");

    }


    /// <summary>
    /// read function copies all order items exists in the order items array and returns it
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<OrderItem> Get(Func<OrderItem, bool> func = null)
    {
        //List<OrderItem> OrderItemList = new List<OrderItem>();
        //for (int i = 0; i < DataSource.OrderItemList.Count(); i++)
        //{
        //    OrderItemList.Add(DataSource.OrderItemList[i]);
        //}
        //return OrderItemList;

        return (func == null ? DataSource.OrderItemList : DataSource.OrderItemList.Where(func).ToList());
    }


    /// <summary>
    /// read single function gets an id of the order item requested and returns it (if it's exist) 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public OrderItem GetSingle(int Id)
    {
        bool flag = true;
        int i;
        for (i = 0; i < DataSource.OrderItemList.Count(); i++)
        {
            if (DataSource.OrderItemList[i].ID == Id)
            {
                flag = false;
                break;
            }
        }
        if (flag)
            throw new DalIdNotFoundException("this order item does not exist");
        return DataSource.OrderItemList[i];
    }


    /// <summary>
    /// update function gets an order item with updated details and put it in the array instead of the order item exist with this id
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(OrderItem obj)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.OrderItemList.Count(); i++)
        {
            if (DataSource.OrderItemList[i].ID == obj.ID)
            {
                DataSource.OrderItemList[i] = obj;
                flag = false;
            }
        }
        if (flag)
            throw new DalIdNotFoundException("this order item does not exist");

    }


    /// <summary>
    /// GetOrderItemByOrderIdAndProductId function gets an order id and product id of the order item requested and returns it (if it's exist) 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public OrderItem GetOrderItemByOrderIdAndProductId(int orderId, int productId)
    {
        bool flag = true;
        int i;
        for (i = 0; i < DataSource.OrderItemList.Count(); i++)
        {
            if (DataSource.OrderItemList[i].OrderID == orderId &&
                DataSource.OrderItemList[i].ProductID == productId)
            {
                flag = false;
                break;
            }
        }
        if (flag)
            throw new DalIdNotFoundException("there is no any order item with this order id and Product id");
        return DataSource.OrderItemList[i];
    }


    /// <summary>
    /// GetOrderItemByOrderId function gets an order id of the order items requested and returns it (it can be more than one) 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<OrderItem> GetOrderItemByOrderId(int orderId)
    {
        //bool flag = true;
        List<OrderItem> OrderItemList = new List<OrderItem>();
        for (int i = 0; i < DataSource.OrderItemList.Count(); i++)
        {
            if (DataSource.OrderItemList[i].OrderID == orderId)
            {
                OrderItemList.Add(DataSource.OrderItemList[i]);
                //flag = false;
            }
        }
       // if (flag)
         //   throw new DalEntityNotFoundException("there are no order items with this order id");
        return OrderItemList;
    }
}


