
using Dal.DO;
namespace Dal;

public class DataSource
{
    public static readonly int Rand;


    public const int minNumOfProducts = 10;
    public const int minNumOfOrders = 20;
    public const int minNumOfOrderItems = 40;


    internal static List<Product> ProductList = new List<Product>();
    internal static List<Order> OrderList = new List<Order>();
    internal static List<OrderItem> OrderItemList = new List<OrderItem>();

    static Random rand = new Random();


    /// <summary>
    /// CreateProductList function initializes the data in the array ProductList
    /// </summary>
    static private void CreateProductList()
    {
        (string, eCategory)[] namesCategoryArr = {
            ("microwen", eCategory.kitchen) ,
            ("oven", eCategory.kitchen),
            ("toaster", eCategory.kitchen),
            ("refrigirator", eCategory.kitchen),
            ("washDishes", eCategory.kitchen),
            ("washMachine", eCategory.washRoom),
            ("meyabesh", eCategory.washRoom),
            ("airContioner", eCategory.otherRoom),
            ("computer", eCategory.otherRoom)
        };

        for (int i = 0; i < minNumOfProducts; i++)
        {
            Product p = new Product();
            int number = (int)rand.NextInt64(namesCategoryArr.Length);
            p.ID = Config.ProductID;
            p.Name = namesCategoryArr[number].Item1;
            p.Price = (int)rand.NextInt64(350, 3000);
            p.Category = namesCategoryArr[number].Item2;
            p.InStock = (int)rand.NextInt64(0, 200);
            ProductList.Add(p);
        }
    }


    /// <summary>
    /// CreateOrderList function initializes the data in the array OrderList
    /// </summary>
    static private void CreateOrderList()
    {

        (string, string, string)[] customerDetailsArr = {
            ("Gitty", "g@g.com", "Achinoam"),
            ("Tova","t@t.com","Eidelson") ,
            ("Sara","s@s.com","Bloy") ,
            ("Rivka","r@r.com","Yirmiahu") ,
            ("Rachel","r33@r.com","Bar ilan") ,
            ("Leah","l@l.com","Levinzon")
        };

        for (int i = 0; i < minNumOfOrders; i++)
        {
            Order o = new Order();
            int index = (int)rand.NextInt64(customerDetailsArr.Length);
            o.ID = Config.OrderID;
            o.CustomerName = customerDetailsArr[index].Item1;
            o.CustomerEmail = customerDetailsArr[index].Item2;
            o.CustomerAdress = customerDetailsArr[index].Item3;
            o.OrderDate = DateTime.MinValue;
            o.ShipDate = o.OrderDate + TimeSpan.FromDays(10);
            o.DeliveryDate = o.ShipDate + TimeSpan.FromDays(20);

            OrderList.Add(o);
        }
    }


    /// <summary>
    /// CreateOrderItemList function initializes the data in the array OrderItemList
    /// </summary>
    static private void CreateOrderItemList()
    {
        for (int i = 0; i < minNumOfOrderItems;)
        {
            int OrderIndex = (int)rand.NextInt64(OrderList.Count());
            int numOfProducts = (int)rand.NextInt64(1, 4);
            for (int j = 0; j < numOfProducts; j++)
            {
                int ProductIndex = (int)rand.NextInt64(ProductList.Count());
                if (ProductList[ProductIndex].InStock == 0)
                    continue;
                OrderItem oi = new OrderItem();

                oi.ID = Config.OrderItemId;
                oi.ProductID = ProductList[ProductIndex].ID;
                oi.OrderID = OrderList[OrderIndex].ID;
                oi.Amount = (int)rand.NextInt64(1, ProductList[ProductIndex].InStock);
                oi.Price = oi.Amount * ProductList[ProductIndex].Price;
                OrderItemList.Add(oi);
                i++;
            }
        }
    }


    /// <summary>
    /// initialization the arrays
    /// </summary>
    static private void s_Initialize()
    {
        CreateProductList();
        CreateOrderList();
        CreateOrderItemList();
    }


    /// <summary>
    /// initialization the arrays
    /// </summary>
    static DataSource()
    {

        s_Initialize();
    }


    /// <summary>
    /// Declarations of variables
    /// </summary>
    public static class Config
    {
        private static int productID = 99999;
        public static int ProductID { get { productID++; return productID; } }
        private static int orderID = 99999;
        public static int OrderID { get { orderID++; return orderID; } }
        private static int orderItemId = 99999;
        public static int OrderItemId { get { orderItemId++; return orderItemId; } }

    }
}
