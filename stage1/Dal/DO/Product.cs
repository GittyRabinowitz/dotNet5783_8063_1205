
namespace Dal.DO;

    public struct Product: IDataObject
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public eCategory Category { get; set; }
    public int InStock { get; set; }
    public override string ToString() => $@"
Product ID={ID}: {Name}, 
category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";
    //public Product(int id, string name, double price, eCategory category, int inStock)
    //{
    //    ID=id;
    //    Name=name;
    //    Price=price;
    //    Category=category;
    //    InStock=inStock;
    //}
}

