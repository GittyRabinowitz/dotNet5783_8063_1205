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
        switch (xmlProduct?.Element("Category")?.Value)
        {
            case "Kitchen":
                product.Category = eCategory.kitchen;
                break;
            case "otherRoom":
                product.Category = eCategory.otherRoom;
                break;
            case "washRoom":
                product.Category = eCategory.washRoom;
                break;
            default:
                break;
        }
       // product.Category = (Dal.DO.eCategory)(Convert.ToInt32(xmlProduct?.Element("Category")?.Value));
        product.InStock = Convert.ToInt32(xmlProduct?.Element("InStock")?.Value);
        return product;
    }

    public int Add(Product obj)
    {
        XElement? configRoot = XDocument.Load("../../xml/config.xml").Root;
        int productId = Convert.ToInt32(configRoot?.Element("ProductID")?.Value);
        configRoot?.Element("ProductID")?.SetValue(productId + 1);
        configRoot?.Save("../../xml/config.xml");
        XElement product = new("Product",
                new XElement("ID", productId),
                new XElement("Name", obj.Name),
                new XElement("Price", obj.Price),
                new XElement("Category", obj.Category),
                new XElement("InStock", obj.InStock)
                );
        XDocument? productLoader = XDocument.Load("../../xml/Product.xml");
        XElement? root = productLoader.Root;
        root?.Add(product);
        productLoader?.Save("../../xml/Product.xml");
        return productId;

    }

    public void decreaseInStock(int id, int amountToDecrease)
    {
        XElement? root = XDocument.Load("../../xml/Product.xml").Root;

        XElement? product = root?.Descendants("Product")?.
                    Where(p => p.Element("ID")?.Value == id.ToString()).FirstOrDefault();
        int newAmount = Convert.ToInt32(product?.Element("InStock")?.Value) - amountToDecrease;

        product?.Element("InStock")?.SetValue(newAmount);

        root?.Save("../../xml/Product.xml");
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load("../../xml/Product.xml").Root;

        XElement? product = root?.Elements("Product")?.
                    Where(p => p.Element("ID")?.Value == id.ToString()).FirstOrDefault();//האם צריך פה tostring?
        product?.Remove();
        root?.Save("../../xml/Product.xml");
    }

    public IEnumerable<Product> Get(Func<Product, bool> func = null)
    {
        if (func == null)
        {
            XElement? root = XDocument.Load("../../xml/Product.xml").Root;
            IEnumerable<XElement>? xmlProductList = root?.Elements("Product").ToList();
            root?.Save("../../xml/Product.xml");
            List<Product> productList = new List<Product>();
            foreach (var xmlProduct in xmlProductList)
            {
                productList.Add(deepCopy(xmlProduct));
            }
            return productList;
        }
        else
        {
            XElement? root = XDocument.Load("../../xml/Product.xml").Root;
            List<XElement>? xmlProductList = root?.Descendants("Product").ToList();
            root?.Save("../../xml/Product.xml");
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
        XElement? root = XDocument.Load("../../xml/Product.xml").Root;
        List<XElement>? xmlProductList = root?.Elements("Product").ToList();
        root?.Save("../../xml/Product.xml");
        List<Product> productList = new List<Product>();
        foreach (var xmlProduct in xmlProductList)
        {
            productList.Add(deepCopy(xmlProduct));
        }
        var product = productList.Where(func).FirstOrDefault();
        return product;
    }

    public void Update(Product obj)
    {
        XElement? root = XDocument.Load("../../xml/Product.xml").Root;

        XElement? product = root?.Elements("Product")?.
                    Where(p => p.Element("ID")?.Value == obj.ID.ToString()).FirstOrDefault();
        product?.Element("Name")?.SetValue(obj.Name);
        product?.Element("Price")?.SetValue(obj.Price);
        product?.Element("Category")?.SetValue(obj.Category);
        product?.Element("InStock")?.SetValue(obj.InStock);


        root?.Save("../../xml/Product.xml");
    }
}

