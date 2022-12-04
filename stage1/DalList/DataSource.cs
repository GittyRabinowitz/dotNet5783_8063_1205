
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
    internal static readonly Random Randomize = new Random();

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
            p.Price = (int)rand.NextInt64(5, 100);
            p.Category = namesCategoryArr[number].Item2;
            p.InStock = (int)rand.NextInt64(0, 50);
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
            Order order = new Order();
            int index = (int)rand.NextInt64(customerDetailsArr.Length);
            order.ID = Config.OrderID;
            order.CustomerName = customerDetailsArr[index].Item1;
            order.CustomerEmail = customerDetailsArr[index].Item2;
            order.CustomerAdress = customerDetailsArr[index].Item3;


            //randomizes a date from 01/01/2010
            Random ran = new Random();
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            order.OrderDate = start.AddDays(ran.Next(range));


            int dateShipExsist = (int)Randomize.NextInt64(0, 5);
            if (dateShipExsist > 0)
            {
                TimeSpan spanOrderShip = TimeSpan.FromDays(5);
                order.ShipDate = order.OrderDate + spanOrderShip;
                int dateDeliveryExsist = (int)Randomize.NextInt64(0, 5);
                if (dateDeliveryExsist > 0)
                {
                    TimeSpan spanShipDelivery = TimeSpan.FromDays(30);
                    order.DeliveryDate = order.ShipDate + spanShipDelivery;
                }
                else
                    order.DeliveryDate = DateTime.MinValue;
            }
            else
            {
                order.ShipDate = DateTime.MinValue;
                order.DeliveryDate = DateTime.MinValue;
            }

            OrderList.Add(order);
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

                oi.ID = Config.OrderItemID;
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
        private static int orderItemID = 99999;
        public static int OrderItemID { get { orderItemID++; return orderItemID; } }

    }
}
