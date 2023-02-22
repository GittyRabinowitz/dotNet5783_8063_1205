
using Dal.DO;
using DalApi;
using System.Runtime.CompilerServices;

namespace Dal.UseObjects;

/// <summary>
/// class for crud actions for an order
/// </summary>

internal class DalOrder : IOrder
{

    /// <summary>
    /// create function gets an object and insert it into the orders array
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>the object's id</returns>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> Get(Func<Order, bool> func = null)
    {
        //List<Order> OrderList = new List<Order>();
        //for (int i = 0; i < DataSource.OrderList.Count(); i++)
        //{
        //    OrderList.Add(DataSource.OrderList[i]);
        //}

        //return OrderList;
        if (DataSource.OrderList.Count() == 0)
            throw new DalNoEntitiesFound("No orders found");

        return (func == null ? DataSource.OrderList : DataSource.OrderList.Where(func).ToList());
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order GetSingle(Func<Order, bool> func)
    {
        if (DataSource.OrderList.Where(func).ToList().Count() == 0)
            throw new DalIdNotFoundException("this order does not exist");
        return (DataSource.OrderList.Where(func).ToArray()[0]);
    }


    /// <summary>
    /// update function gets an order with updated details and put it in the array instead of the order exist with this id
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
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

