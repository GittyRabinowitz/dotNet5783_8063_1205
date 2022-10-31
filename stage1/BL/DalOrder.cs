

using Dal.DO;

namespace Dal.UseObjects;

public class DalOrder
{
    public static int Create(Order obj)//לקבל פה ישר הזמנה
    {
        if (DataSource.Config.orderIdx>= DataSource.numOfOrders)
        {
            throw new NotImplementedException("There is no space available for your order");

        }
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (DataSource.OrderList[i].ID == obj.ID)
            {
                throw new NotImplementedException("this order already exist");
            }    
        }
        DataSource.OrderList[DataSource.Config.orderIdx++] =obj;
        return obj.ID;
    }

    public static void Delete(int Id)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (DataSource.OrderList[i].ID == Id)
            {

                for(int j=i;j< DataSource.Config.orderIdx; j++)
                {
                    DataSource.OrderList[j] = DataSource.OrderList[j + 1];
                }
                DataSource.Config.orderIdx--;
                flag = false;
            }
     
        }
        if(flag)
            throw new NotImplementedException("this order does not exist");
    }

    public static Order[] Read()
    {
        Order[] OrderList = new Order[DataSource.Config.orderIdx];
        for(int i=0;i< DataSource.Config.orderIdx; i++)
        {
            OrderList[i] = DataSource.OrderList[i];
        }
        return OrderList;
        throw new NotImplementedException();
    }

    public static Order ReadSingle(int Id)
    {
        bool flag = true;
        int i;
        for(i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (DataSource.OrderList[i].ID == Id)
            {
                flag = false;
                break;
            } 
        }
        if (flag)
            throw new NotImplementedException("this order does not exist");
        return DataSource.OrderList[i];

    }

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
            throw new NotImplementedException("this order does not exist");
    }
}

