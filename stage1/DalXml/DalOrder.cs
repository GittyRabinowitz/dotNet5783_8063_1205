using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dal;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using Dal.DO;


internal class DalOrder : IOrder
{
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

    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
        StreamReader reader = new StreamReader("../../xml/Order.xml");
        List<DO.Order> orders = (List<DO.Order>)ser.Deserialize(reader);
        reader.Close();
       Order order= orders.Where(o => o.ID == id).FirstOrDefault();
        orders.Remove(order);
     
        StreamWriter writer = new StreamWriter("../../xml/Order.xml");
        ser.Serialize(writer, orders);
        writer.Close();
    }

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
            reader.Close();
            orders = orders.Where(func).ToList();
            return orders;

        }
    }

    public Order GetSingle(Func<Order, bool> func)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
        StreamReader reader = new StreamReader("../../xml/Order.xml");
        List<DO.Order> orders = (List<DO.Order>)ser.Deserialize(reader);
        reader.Close();
        Order o = orders.Where(func).FirstOrDefault();
        return o;
    }

    public void Update(Order obj)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Orders";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Order>), xRoot);
        StreamReader reader = new StreamReader("../../xml/Order.xml");
        List<DO.Order> orders = (List<DO.Order>)ser.Deserialize(reader);
        reader.Close();
        Order o = orders.Where(order=>order.ID==obj.ID).FirstOrDefault();
        orders.Remove(o);
        orders.Add(obj);

        StreamWriter writer = new StreamWriter("../../xml/Order.xml");
        ser.Serialize(writer, orders);
        writer.Close();
    }
}
