
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
    /// update function gets an order item with updated details and put it in the array instead of the order item exist with this id
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(OrderItem obj)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.OrderItemList.Count(); i++)
        {
            if (((OrderItem)DataSource.OrderItemList[i]).ID == obj.ID)
            {
                DataSource.OrderItemList[i] = obj;
                flag = false;
            }
        }
        if (flag)
            throw new DalIdNotFoundException("this order item does not exist");

    }


    public OrderItem GetSingle(Func<OrderItem, bool> func)
    {
        if (DataSource.OrderItemList.Where(func).ToList().Count() == 0)
            throw new DalIdNotFoundException("this order item does not exist");
        return (DataSource.OrderItemList.Where(func).ToArray()[0]);
    }
}


