using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using Dal.DO;
using System.Runtime.CompilerServices;

namespace Dal;


internal class DalOrder : IOrder
{


    /// <summary>
    /// the function gets order and adds it to the xml file
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order obj)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
        StreamReader reader = new StreamReader("../../xml/Order.xml");
        List<DO.Order> orders = (List<DO.Order>)ser.Deserialize(reader);
        reader.Close();

        XElement? rootConfig = XDocument.Load("../../xml/config.xml").Root;
        XElement? id = rootConfig?.Element("OrderID");
        int orderId = Convert.ToInt32(id?.Value);
        obj.ID = orderId;
        orderId++;
        id.Value = orderId.ToString();
        rootConfig?.Save("../../xml/config.xml");

        orders?.Add(obj);
        StreamWriter writer = new StreamWriter("../../xml/Order.xml");
        ser.Serialize(writer, orders);
        writer.Close();
        return obj.ID;
    }



    /// <summary>
    /// the function gets order id and deletes it from the xml file
    /// </summary>
    /// <param name="id"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
        StreamReader reader = new StreamReader("../../xml/Order.xml");
        List<DO.Order> orders = (List<DO.Order>)ser.Deserialize(reader);
        if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no products exist"); }
        reader.Close();
        Order order = orders.Where(o => o.ID == id).FirstOrDefault();
        if (order.ID == 0) { throw new Dal.DalIdNotFoundException("this order does not exist"); }
        orders.Remove(order);

        StreamWriter writer = new StreamWriter("../../xml/Order.xml");
        ser.Serialize(writer, orders);
        writer.Close();
    }


    /// <summary>
    /// the function returns all the orders or accordaring the func
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> Get(Func<Order, bool> func = null)
    {
        if (func == null)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;

            XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
            StreamReader reader = new StreamReader("../../xml/Order.xml");
            List<DO.Order> orders = (List<DO.Order>)ser.Deserialize(reader);
            if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no orders exist"); }
            reader.Close();
            return orders;
        }
        else
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Orders";
            xRoot.IsNullable = true;

            XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
            StreamReader reader = new StreamReader("../../xml/Order.xml");
            List<DO.Order> orders = (List<DO.Order>)ser.Deserialize(reader);
            if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no orders exist"); }
            reader.Close();
            orders = orders.Where(func).ToList();
            return orders;
        }
    }


    /// <summary>
    /// the function returns order accordaring the func it gets
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order GetSingle(Func<Order, bool> func)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
        StreamReader reader = new StreamReader("../../xml/Order.xml");
        List<DO.Order> orders = (List<DO.Order>)ser.Deserialize(reader);
        if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no orders exist"); }
        reader.Close();
        Order order = orders.Where(func).FirstOrDefault();
        if (order.ID == 0) { throw new Dal.DalIdNotFoundException("this order does not exist"); }
        return order;
    }


    /// <summary>
    /// the function gets order and updates it to the xml file
    /// </summary>
    /// <param name="obj"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order obj)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
        StreamReader reader = new StreamReader("../../xml/Order.xml");
        List<DO.Order> orders = (List<DO.Order>)ser.Deserialize(reader);
        if (orders.Count() == 0) { throw new Dal.DalNoEntitiesFound("no orders exist"); }
        reader.Close();
        Order order = orders.Where(o => o.ID == obj.ID).FirstOrDefault();
        if (order.ID == 0) { throw new Dal.DalIdNotFoundException("this order does not exist"); }
        orders.Remove(order);
        orders.Add(obj);

        StreamWriter writer = new StreamWriter("../../xml/Order.xml");
        ser.Serialize(writer, orders);
        writer.Close();
    }
}
