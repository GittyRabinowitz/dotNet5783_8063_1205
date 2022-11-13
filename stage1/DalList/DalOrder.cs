
using Dal.DO;
namespace Dal.UseObjects;

/// <summary>
/// class for crud actions for an order
/// </summary>

public class DalOrder
{

    /// <summary>
    /// create function gets an object and insert it into the orders array
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>the object's id</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static int Create(Order obj)
    {
        if (DataSource.Config.orderIdx >= DataSource.maxNumOfOrders)
        {
            throw new Exception("There is no space available for your order");

        }

        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (DataSource.OrderList[i].ID == obj.ID)
            {
                throw new Exception("this order already exist");
            }
        }

        DataSource.OrderList[DataSource.Config.orderIdx++] = obj;
        return obj.ID;
    }


    /// <summary>
    /// delete function gets an id of the object requsted to be deleted and deletes it from the orders array
    /// </summary>
    /// <param name="Id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Delete(int Id)
    {
        bool flag = true;

        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (DataSource.OrderList[i].ID == Id)
            {

                for (int j = i; j < DataSource.Config.orderIdx; j++)
                {
                    DataSource.OrderList[j] = DataSource.OrderList[j + 1];
                }
                DataSource.Config.orderIdx--;
                flag = false;
            }
        }

        if (flag)
            throw new Exception("this order does not exist");
    }


    /// <summary>
    /// read function copies all orders exists in the orders array and returns it
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Order[] Read()
    {
        Order[] OrderList = new Order[DataSource.Config.orderIdx];

        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            OrderList[i] = DataSource.OrderList[i];
        }

        return OrderList;
        throw new Exception();
    }


    /// <summary>
    /// read single function gets an id of the order requested and returns it (if it's exist) 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Order ReadSingle(int Id)
    {
        bool flag = true;
        int i;
        for (i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (DataSource.OrderList[i].ID == Id)
            {
                flag = false;
                break;
            }
        }
        if (flag)
            throw new Exception("this order does not exist");
        return DataSource.OrderList[i];

    }


    /// <summary>
    /// update function gets an order with updated details and put it in the array instead of the order exist with this id
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Update(Order obj)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (DataSource.OrderList[i].ID == obj.ID)
            {
                DataSource.OrderList[i] = obj;
                flag = false;
            }
        }
        if (flag)
            throw new Exception("this order does not exist");
    }
}

