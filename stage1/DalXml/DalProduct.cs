using Dal.DO;
using DalApi;
using System.Xml.Linq;

namespace Dal;


internal class DalProduct : IProduct
{

    /// <summary>
    /// the function gets XElement and convert it to Dal.DO.Product
    /// </summary>
    /// <param name="xmlProduct"></param>
    /// <returns></returns>
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


    /// <summary>
    /// the function gets product and adds it to the xml file
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
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


    /// <summary>
    /// the function gets product id and amount to decrease and updates the product's instock
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amountToDecrease"></param>
    public void decreaseInStock(int id, int amountToDecrease)
    {
        XElement? root = XDocument.Load("../../xml/Product.xml").Root;

        XElement? product = root?.Descendants("Product")?.
                    Where(p => p.Element("ID")?.Value == id.ToString()).FirstOrDefault();
        if (product == null) { throw new Dal.DalIdNotFoundException("this product does not exist"); }
        int newAmount = Convert.ToInt32(product?.Element("InStock")?.Value) - amountToDecrease;

        product?.Element("InStock")?.SetValue(newAmount);

        root?.Save("../../xml/Product.xml");
    }


    /// <summary>
    /// the function gets product id and deletes it from the xml file
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {
        XElement? root = XDocument.Load("../../xml/Product.xml").Root;

        XElement? product = root?.Elements("Product")?.
                    Where(p => p.Element("ID")?.Value == id.ToString()).FirstOrDefault();
        if (product == null) { throw new Dal.DalIdNotFoundException("this product does not exist"); }
        product?.Remove();
        root?.Save("../../xml/Product.xml");
    }


    /// <summary>
    ///  the function returns all the products or accordaring the func
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
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
            if(productList.Count()==0) { throw new Dal.DalNoEntitiesFound("no products exist"); }
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
            if (productList.Count() == 0) { throw new Dal.DalNoEntitiesFound("no products exist"); }
            var products = productList.Where(func).ToList();
            return products;
        }
    }


    /// <summary>
    /// the function returns product accordaring the func it gets
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
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
        if (productList.Count() == 0) { throw new Dal.DalNoEntitiesFound("no products exist"); }
        var product = productList.Where(func).FirstOrDefault();
        return product;
    }


    /// <summary>
    /// the function gets product and updates it to the xml file
    /// </summary>
    /// <param name="obj"></param>
    public void Update(Product obj)
    {
        XElement? root = XDocument.Load("../../xml/Product.xml").Root;

        XElement? product = root?.Elements("Product")?.
                    Where(p => p.Element("ID")?.Value == obj.ID.ToString()).FirstOrDefault();
        if (product == null) { throw new Dal.DalIdNotFoundException("this product does not exist"); }
        product?.Element("Name")?.SetValue(obj.Name);
        product?.Element("Price")?.SetValue(obj.Price);
        product?.Element("Category")?.SetValue(obj.Category);
        product?.Element("InStock")?.SetValue(obj.InStock);

        root?.Save("../../xml/Product.xml");
    }
}

