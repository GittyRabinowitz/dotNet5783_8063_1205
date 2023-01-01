using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

using Dal.DO;
using DalApi;
using System.Xml.Linq;

internal class DalProduct : IProduct
{

    public Dal.DO.Product deepCopy(XElement? xmlProduct)
    {
        Dal.DO.Product product = new Product();
        product.ID = Convert.ToInt32(xmlProduct?.Element("ID")?.Value);
        product.Name = xmlProduct?.Element("Name")?.Value;
        product.Price = Convert.ToInt32(xmlProduct?.Element("Price")?.Value);
        product.Category = (Dal.DO.eCategory)Convert.ToInt32(xmlProduct?.Element("Category")?.Value);
        product.InStock = Convert.ToInt32(xmlProduct?.Element("InStock")?.Value);
        return product;
    }

    public int Add(Product obj)
    {
        //לכתוב מאיזה קובץ
        XElement? configRoot = XDocument.Load("config.xml").Root;
        int productId = Convert.ToInt32(configRoot?.Element("ID")?.Element("ProductID")?.Value);
        configRoot?.Element("ID")?.Element("ProductID")?.SetValue(productId+1);

        XElement product = new("Product",
                new XElement("ID", productId),
                new XElement("Name", obj.Name),
                new XElement("Price", obj.Price),
                new XElement("Category", obj.Category),
                new XElement("InStock", obj.InStock)
                );
        XElement? root = XDocument.Load("../../Product.xml").Root;
        root?.Element("Products")?.Add(product);
        root?.Save("..\\..\\Product.xml");
        return obj.ID;
        throw new NotImplementedException();
    }

    public void decreaseInStock(int id, int amountToDecrease)
    {
        XElement? root = XDocument.Load("../../Product.xml").Root;

        XElement? product = root?.Elements("Products")?.Elements("Product")?.
                    Where(p => p.Attribute("ID")?.Value == id.ToString()).FirstOrDefault();//האם צריך פה tostring?
        product?.Attribute("InStock")?.SetValue(Convert.ToInt32(product.Attribute("InStock")?.Value)-amountToDecrease);

        root?.Save("../../Product.xml");
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load("..\\..\\Product.xml").Root;

        XElement? product = root?.Elements("Products")?.Elements("Product")?.
                    Where(p => p.Attribute("ID")?.Value == id.ToString()).FirstOrDefault();//האם צריך פה tostring?
        product?.Remove();
        root?.Save("..\\..\\Product.xml");
        throw new NotImplementedException();
    }

    public IEnumerable<Product> Get(Func<Product, bool> func = null)
    {
        if (func==null)
        {
            XElement? root = XDocument.Load("..\\..\\Product.xml").Root;
            IEnumerable<XElement>? xmlProductList = root?.Elements("Products")?.Elements("Product").ToList();
            root?.Save("..\\..\\Product.xml");
            List<Product> productList = new List<Product>();
            foreach (var xmlProduct in xmlProductList)
            {
                productList.Add(deepCopy(xmlProduct));
            }
            return productList;
            throw new NotImplementedException();
        }
        else
        {
            XElement? root = XDocument.Load("..\\..\\Product.xml").Root;
            List<XElement>? xmlProductList = root?.Elements("Products")?.Elements("Product").ToList();
            root?.Save("..\\..\\Product.xml");
            List<Product> productList = new List<Product>();
            foreach (var xmlProduct in xmlProductList)
            {
                productList.Add(deepCopy(xmlProduct));
            }
            var products = productList.Where(func).ToList();

            return products;
        }
    }

    public Product GetSingle(Func<Product, bool> func)
    {
        XElement? root = XDocument.Load("..\\..\\Product.xml").Root;
        List<XElement>? xmlProductList = root?.Elements("Products")?.Elements("Product").ToList();
        root?.Save("..\\..\\Product.xml");
        List<Product> productList = new List<Product>();
        foreach (var xmlProduct in xmlProductList)
        {
            productList.Add(deepCopy(xmlProduct));
        }
        var product = productList.Where(func).FirstOrDefault();
        return product;
        throw new NotImplementedException();
    }

    public void Update(Product obj)
    {
        XElement? root = XDocument.Load("..\\..\\Product.xml").Root;

        XElement? product = root?.Elements("Products")?.Elements("Product")?.
                    Where(p => p.Attribute("ID")?.Value == obj.ID.ToString()).FirstOrDefault();//האם צריך פה tostring?
        product?.Attribute("Name")?.SetValue(obj.Name);
        product?.Attribute("Price")?.SetValue(obj.Price);
        product?.Attribute("Category")?.SetValue(obj.Category);
        product?.Attribute("InStock")?.SetValue(obj.InStock);


        root?.Save("..\\..\\Product.xml");
        throw new NotImplementedException();
    }
}

