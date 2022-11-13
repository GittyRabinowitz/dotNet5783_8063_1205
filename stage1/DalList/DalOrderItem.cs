
using Dal.DO;
namespace Dal.UseObjects;

/// <summary>
/// class for crud actions for an order item
/// </summary>
/// 
public class DalOrderItem
{

    /// <summary>
    /// create function gets an object and insert it into the order items array
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>the object's id</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static int Create(OrderItem obj)
    {
        if (DataSource.Config.orderItemIdx >= DataSource.maxNumOfOrderItems)
        {
            throw new NotImplementedException("There is no space available for your order item");
        }
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].ID == obj.ID)
            {
                throw new Exception("this order item already exist");
            }
        }
        DataSource.OrderItemList[DataSource.Config.orderItemIdx++] = obj;
        return obj.ID;
        throw new Exception();
    }


    /// <summary>
    /// delete function gets an id of the object requsted to be deleted and deletes it from the order items array
    /// </summary>
    /// <param name="Id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Delete(int Id)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].ID == Id)
            {
                for (int j = i; j < DataSource.Config.orderItemIdx; j++)
                {
                    DataSource.OrderItemList[j] = DataSource.OrderItemList[j + 1];

                }
                DataSource.Config.orderItemIdx--;
                flag = false;
            }
        }
        if (flag)
            throw new Exception("this order item does not exist");

    }


    /// <summary>
    /// read function copies all order items exists in the order items array and returns it
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static OrderItem[] Read()
    {
        OrderItem[] OrderItemList = new OrderItem[DataSource.Config.orderItemIdx];
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            OrderItemList[i] = DataSource.OrderItemList[i];
        }
        return OrderItemList;
        throw new Exception();
    }


    /// <summary>
    /// read single function gets an id of the order item requested and returns it (if it's exist) 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static OrderItem ReadSingle(int Id)
    {
        bool flag = true;
        int i;
        for (i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].ID == Id)
            {
                flag = false;
                break;
            }
        }
        if (flag)
            throw new Exception("this order item does not exist");
        return DataSource.OrderItemList[i];
    }


    /// <summary>
    /// update function gets an order item with updated details and put it in the array instead of the order item exist with this id
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Update(OrderItem obj)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].ID == obj.ID)
            {
                DataSource.OrderItemList[i] = obj;
                flag = false;
            }
        }
        if (flag)
            throw new Exception("this order item does not exist");

    }


    /// <summary>
    /// ReadOrderItemByOrderIdAndProductId function gets an order id and product id of the order item requested and returns it (if it's exist) 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static OrderItem ReadOrderItemByOrderIdAndProductId(int orderId, int productId)
    {
        bool flag = true;
        int i;
        for (i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].OrderID == orderId &&
                DataSource.OrderItemList[i].ProductID == productId)
            {
                flag = false;
                break;
            }
        }
        if (flag)
            throw new Exception("there is no any order item with this order id and Product id");
        return DataSource.OrderItemList[i];
    }


    /// <summary>
    /// ReadOrderItemByOrderId function gets an order id of the order items requested and returns it (it can be more than one) 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static OrderItem[] ReadOrderItemByOrderId(int orderId)
    {
        int counter = 0;
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].OrderID == orderId)
            {
                counter++;
            }
        }
        int index = 0;
        OrderItem[] OrderItemList = new OrderItem[counter];
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].OrderID == orderId)
            {
                OrderItemList[index++] = DataSource.OrderItemList[i];
            }

        }
        if (index == 0)
            throw new Exception("there are no order items with this order id");
        return OrderItemList;
    }
}


