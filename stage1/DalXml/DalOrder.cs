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

internal class DalOrder : IOrder
{
    public int Add(Order obj)
    {
        StreamReader reader = new StreamReader("../../config.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
       var a= xmlSerializer.Deserialize(reader);
        //בזמן ריצה לראות מה a מכיל
        //לשים מזהה לאוביקט ולהגדיל את המזהה בקובץ
        //להוסיף מזהה
        //לכתוב מאיזה קובץ
        //XElement? configRoot = XDocument.Load("config.xml").Root;
        //int productId = Convert.ToInt32(configRoot?.Element("ID")?.Element("ProductID")?.Value);
        //configRoot?.Element("ID")?.Element("ProductID")?.SetValue(productId+1);

        //StreamReader reader = new StreamReader("../../Order.xml");
        StreamWriter writer = new StreamWriter("../../Order.xml");
        xmlSerializer.Serialize(writer, obj);
        writer.Close();

        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        StreamReader reader = new StreamReader("../../Order.xml");
        StreamWriter writer = new StreamWriter("../../Order.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));


        List<Order>? orders = (List<Order>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Order o = orders.Where(o => o.ID==id).FirstOrDefault();
        orders.Remove(o);
        xmlSerializer.Serialize(writer, orders);
        writer.Close();
        throw new NotImplementedException();
    }

    public IEnumerable<Order> Get(Func<Order, bool> func = null)
    {
        if (func==null)
        {
            StreamReader reader = new StreamReader("../../Order.xml");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
            IEnumerable<Order>? orders = (IEnumerable<Order>?)xmlSerializer.Deserialize(reader);
            reader.Close();
            return orders;
        }
        else
        {
            StreamReader reader = new StreamReader("../../Order.xml");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
            IEnumerable<Order>? orders = (IEnumerable<Order>?)xmlSerializer.Deserialize(reader);
            reader.Close();
            orders=orders.Where(func).ToList();
            return orders;
        }
        throw new NotImplementedException();
    }

    public Order GetSingle(Func<Order, bool> func)
    {
        StreamReader reader = new StreamReader("../../Order.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
        List<Order>? orders = (List<Order>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        return orders.Where(func).FirstOrDefault();
        throw new NotImplementedException();
    }

    public void Update(Order obj)
    {
        StreamReader reader = new StreamReader("../../Order.xml");
        StreamWriter writer = new StreamWriter("../../Order.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));


        List<Order>? orders = (List<Order>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        
        Order o = orders.Where(o => o.ID==obj.ID).FirstOrDefault();
        orders.Remove(o);

        xmlSerializer.Serialize(writer, obj);
        writer.Close();
        throw new NotImplementedException();
    }
}
