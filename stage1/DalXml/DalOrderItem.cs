using Dal.DO;
using DalApi;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class DalOrderItem : IOrderItem
{

    /// <summary>
    /// the function gets order item and adds it to the xml file
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem obj)
    {

        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("../../xml/OrderItem.xml");
        List<DO.OrderItem> orders = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();

        XElement? rootConfig = XDocument.Load("../../xml/config.xml").Root;
        XElement? id = rootConfig?.Element("OrderItemID");
        int orderId = Convert.ToInt32(id?.Value);
        obj.ID = orderId;
        orderId++;
        id.Value = orderId.ToString();
        rootConfig?.Save("../../xml/config.xml");
        orders?.Add(obj);
        StreamWriter writer = new StreamWriter("../../xml/OrderItem.xml");
        ser.Serialize(writer, orders);
        writer.Close();
        return obj.ID;
    }


    /// <summary>
    /// the function gets order item id and deletes it from the xml file
    /// </summary>
    /// <param name="id"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("../../xml/OrderItem.xml");
        List<DO.OrderItem> orders = (List<DO.OrderItem>)ser.Deserialize(reader);
        if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no orders exist"); }
        reader.Close();
        OrderItem order = orders.Where(o => o.ID == id).FirstOrDefault();
        if (order.ID==0) { throw new Dal.DalIdNotFoundException("this order item does not exist"); }
        orders.Remove(order);

        StreamWriter writer = new StreamWriter("../../xml/OrderItem.xml");
        ser.Serialize(writer, orders);
        writer.Close();
    }



    /// <summary>
    ///  the function returns all the order items or accordaring the func
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem> Get(Func<OrderItem, bool> func = null)
    {
        if (func == null)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;

            XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
            StreamReader reader = new StreamReader("../../xml/OrderItem.xml");
            List<DO.OrderItem> orders = (List<DO.OrderItem>)ser.Deserialize(reader);
            if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no orders exist"); }
            reader.Close();
            return orders;
        }
        else
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "OrderItems";
            xRoot.IsNullable = true;

            XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
            StreamReader reader = new StreamReader("../../xml/OrderItem.xml");
            List<DO.OrderItem> orders = (List<DO.OrderItem>)ser.Deserialize(reader);
            if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no orders exist"); }
            reader.Close();
            orders = orders.Where(func).ToList();
            return orders;

        }
    }


    /// <summary>
    /// the function returns order item accordaring the func it gets
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem GetSingle(Func<OrderItem, bool> func)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("../../xml/OrderItem.xml");
        List<DO.OrderItem> orders = (List<DO.OrderItem>)ser.Deserialize(reader);
        if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no orders exist"); }
        reader.Close();
        OrderItem oi = orders.Where(func).FirstOrDefault();
        if (oi.ID == 0) { throw new Dal.DalIdNotFoundException("this order item does not exist"); }
        return oi;
    }


    /// <summary>
    /// the function gets order item and updates it to the xml file
    /// </summary>
    /// <param name="obj"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem obj)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("../../xml/OrderItem.xml");
        List<DO.OrderItem> orders = (List<DO.OrderItem>)ser.Deserialize(reader);
        if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no order items exist"); }
        reader.Close();
        OrderItem o = orders.Where(oi => oi.ID == obj.ID).FirstOrDefault();
        if (o.ID==0) { throw new Dal.DalIdNotFoundException("this order item does not exist"); }
        orders.Remove(o);
        orders.Add(obj);

        StreamWriter writer = new StreamWriter("../../xml/OrderItem.xml");
        ser.Serialize(writer, orders);
        writer.Close();
    }
}