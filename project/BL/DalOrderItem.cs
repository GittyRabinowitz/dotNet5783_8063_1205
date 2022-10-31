
using Dal.DO;
namespace Dal.UseObjects;
public class DalOrderItem
{
    public static int Create(OrderItem obj)
    {
        if (DataSource.Config.orderItemIdx>= DataSource.numOfOrderItems)
        {
            throw new NotImplementedException("There is no space available for your order item");
        }
        for ( int i=0; i<DataSource.Config.orderItemIdx; i++)
        {
            if(DataSource.OrderItemList[i].ID==obj.ID)
            {
                throw new NotImplementedException("this order item already exist");
            }
        }
        DataSource.OrderItemList[DataSource.Config.orderItemIdx++] =obj;
        return obj.ID;
        throw new NotImplementedException();
    }

   public static void Delete(int Id)
    {
        bool flag = true;
        for (int i = 0; i<DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].ID==Id)
            {
                for (int j = i; j<DataSource.Config.orderItemIdx; j++)
                {
                    DataSource.OrderItemList[j]=DataSource.OrderItemList[j+1];

                }
                DataSource.Config.orderItemIdx--;
                flag=false;
            }
        }
        if(flag)
            throw new NotImplementedException("this order item does not exist");

    }

    public static OrderItem[] Read()
    {
        OrderItem[] OrderItemList = new OrderItem[DataSource.Config.orderItemIdx];
        for (int i = 0; i<DataSource.Config.orderItemIdx; i++)
        {
            OrderItemList[i]=DataSource.OrderItemList[i];
        }
        return OrderItemList;
        throw new NotImplementedException();
    }

   public static OrderItem ReadSingle(int Id)
    {
        bool flag = true;
        int i;
        for (i = 0; i<DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].ID==Id)
            {
                flag=false;
                break;
            }
        }
        if(flag)
            throw new NotImplementedException("this order item does not exist");
        return DataSource.OrderItemList[i];
    }

    public static void Update(OrderItem obj)
    {
        bool flag = true;
        for (int i = 0; i<DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].ID==obj.ID)
            {
                DataSource.OrderItemList[i]=obj;
                flag=false;
            }
        }
        if(flag)
            throw new NotImplementedException("this order item does not exist");

    }

    public static OrderItem ReadOrderItemByOrderIdAndProductId(int orderId, int productId)
    {
        bool flag = true;
        int i;
        for (i = 0; i<DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].OrderID==orderId &&
                DataSource.OrderItemList[i].ProductID==productId)
            {
                flag=false;
                break;
            }
        }
        if (flag)
            throw new NotImplementedException("there is no any order item with this order id and product id");
        return DataSource.OrderItemList[i];
    }

    public static OrderItem[] ReadOrderItemByOrderId(int orderId)
    {
        int index = 0;
        OrderItem[] OrderItemList = new OrderItem[DataSource.Config.orderItemIdx];
        for (int i = 0; i<DataSource.Config.orderItemIdx; i++)
        {
            if (DataSource.OrderItemList[i].OrderID==orderId)
            {
                OrderItemList[index++]=DataSource.OrderItemList[i];
            }
            
        }
        if (index==0)
            throw new NotImplementedException("there are no order items with this order id");
        return OrderItemList;
    }
}


