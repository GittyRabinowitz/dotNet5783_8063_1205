
using Dal.DO;
using DalApi;
namespace Dal.UseObjects;

/// <summary>
/// class for crud actions for an order
/// </summary>

internal class DalOrder:IOrder
{

    /// <summary>
    /// create function gets an object and insert it into the orders array
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>the object's id</returns>
    /// <exception cref="NotImplementedException"></exception>
    public int Add(Order obj)
    {
        obj.ID = DataSource.Config.OrderID;
        DataSource.OrderList.Add(obj);
        return obj.ID;
    }


    /// <summary>
    /// delete function gets an id of the object requsted to be deleted and deletes it from the orders array
    /// </summary>
    /// <param name="Id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Delete(int Id)
    {
        bool flag = true;

        for (int i = 0; i < DataSource.OrderList.Count(); i++)
        {
            if (DataSource.OrderList[i].ID == Id)
            {
                DataSource.OrderList.Remove(DataSource.OrderList[i]);
                flag = false;
            }
        }

        if (flag)
            throw new DalIdNotFoundException("this order does not exist");
    }


    /// <summary>
    /// read function copies all orders exists in the orders array and returns it
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<Order> Get()
    {
        List<Order> OrderList = new List<Order>();
        for (int i = 0; i < DataSource.OrderList.Count(); i++)
        {
            OrderList.Add(DataSource.OrderList[i]);
        }

        return OrderList;
    }


    /// <summary>
    /// read single function gets an id of the order requested and returns it (if it's exist) 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Order GetSingle(int Id)
    {
        bool flag = true;
        int i;
        for (i = 0; i < DataSource.OrderList.Count(); i++)
        {
            if (DataSource.OrderList[i].ID == Id)
            {
                flag = false;
                break;
            }
        }
        if (flag)
            throw new DalIdNotFoundException("this order does not exist");
        return DataSource.OrderList[i];

    }


    /// <summary>
    /// update function gets an order with updated details and put it in the array instead of the order exist with this id
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(Order obj)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.OrderList.Count(); i++)
        {
            if (DataSource.OrderList[i].ID == obj.ID)
            {
                DataSource.OrderList[i] = obj;
                flag = false;
                break;
            }
        }
        if (flag)
            throw new DalIdNotFoundException("this order does not exist");
    }
}

