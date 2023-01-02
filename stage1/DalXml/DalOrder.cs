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
        StreamReader configReader = new StreamReader("../../../../../xml/config.xml");
        
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(XElement));
        XElement ids = (XElement)xmlSerializer.Deserialize(configReader);
        int id = Convert.ToInt32(ids?.Element("OrderID")?.Value);
        obj.ID = id;
        ids?.Element("OrderID")?.SetValue(id + 1);
        configReader.Close();



        StreamWriter configWriter = new StreamWriter("../../../../../xml/config.xml");
        xmlSerializer.Serialize(configWriter, ids);
        configWriter.Close();


        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        // xRoot.Namespace = "http://www.cpandl.com";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);


        StreamReader orderReader = new StreamReader("../../../../../xml/Order.xml");


        List<Order> lst = (List<Order>)ser.Deserialize(orderReader);

        //XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        // List<Order> lst = (List<Order>)ser?.Deserialize(orderReader);
        ////var lst= ser?.Deserialize(orderReader);
        orderReader.Close();
        StreamWriter orderWriter = new StreamWriter("../../../../../xml/Order.xml");

      //  lst?.Add(obj);לפתוח מהערה
       // ser?.Serialize(writer, lst);לפתוח מהערה
        orderWriter.Close();
        
        return id;

    }

    public void Delete(int id)
    {
        StreamReader reader = new StreamReader("../../Order.xml");
        StreamWriter writer = new StreamWriter("../../Order.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));


        List<Order>? orders = (List<Order>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Order o = orders.Where(o => o.ID == id).FirstOrDefault();
        orders.Remove(o);
        xmlSerializer.Serialize(writer, orders);
        writer.Close();
        throw new NotImplementedException();
    }

    public IEnumerable<Order> Get(Func<Order, bool> func = null)
    {
        if (func == null)
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
            orders = orders.Where(func).ToList();
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

        Order o = orders.Where(o => o.ID == obj.ID).FirstOrDefault();
        orders.Remove(o);

        xmlSerializer.Serialize(writer, obj);
        writer.Close();
        throw new NotImplementedException();
    }
}
