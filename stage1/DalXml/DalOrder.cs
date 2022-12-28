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
        //להוסיף מזהה
        //לכתוב מאיזה קובץ
        XElement? configRoot = XDocument.Load("config.xml").Root;
        int productId = Convert.ToInt32(configRoot?.Element("ID")?.Element("ProductID")?.Value);
        configRoot?.Element("ID")?.Element("ProductID")?.SetValue(productId+1);

        //StreamReader reader = new StreamReader("../../Order.xml");
        StreamWriter writer = new StreamWriter("../../Order.xml");
        XmlSerializer xmlSerializer=new XmlSerializer(typeof(Order));
        xmlSerializer.Serialize(writer, obj);
        writer.Close();




        //string sayala = JsonSerializer.Serialize<Student>(ayala);

        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> Get(Func<Order, bool> func = null)
    {
        if (func==null)
        {
            StreamReader reader = new StreamReader("../../Order.xml");
            //var orders = reader.ReadToEnd();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));

            //foreach (var item in orders)
            //{
            //   var f= xmlSerializer.Deserialize(reader);
            //}
           Order b = (Order)xmlSerializer.Deserialize(reader);
            
        
            reader.Close();
        }
        else
        {

        }
        throw new NotImplementedException();
    }

    public Order GetSingle(Func<Order, bool> func)
    {
        throw new NotImplementedException();
    }

    public void Update(Order obj)
    {
        throw new NotImplementedException();
    }
}
