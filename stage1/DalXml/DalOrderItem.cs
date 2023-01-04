using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

using Dal.DO;
using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class DalOrderItem : IOrderItem
{
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

    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("../../xml/OrderItem.xml");
        List<DO.OrderItem> orders = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();
        OrderItem order = orders.Where(o => o.ID == id).FirstOrDefault();
        orders.Remove(order);

        StreamWriter writer = new StreamWriter("../../xml/OrderItem.xml");
        ser.Serialize(writer, orders);
        writer.Close();
    }

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
            reader.Close();
            orders = orders.Where(func).ToList();
            return orders;

        }
    }

    public OrderItem GetSingle(Func<OrderItem, bool> func)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("../../xml/OrderItem.xml");
        List<DO.OrderItem> orders = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();
        OrderItem oi = orders.Where(func).FirstOrDefault();
        return oi;
    }

    public void Update(OrderItem obj)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "OrderItems";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<OrderItem>), xRoot);
        StreamReader reader = new StreamReader("../../xml/OrderItem.xml");
        List<DO.OrderItem> orders = (List<DO.OrderItem>)ser.Deserialize(reader);
        reader.Close();
        OrderItem o = orders.Where(oi => oi.ID == obj.ID).FirstOrDefault();
        orders.Remove(o);
        orders.Add(obj);

        StreamWriter writer = new StreamWriter("../../xml/OrderItem.xml");
        ser.Serialize(writer, orders);
        writer.Close();
    }
}