
using Dal.DO;
namespace Dal;

static public class DataSource
{
    public static readonly int Rand;

    public const int maxNumOfProducts = 50;
    public const int maxNumOfOrders = 100;
    public const int maxNumOfOrderItems = 200;


    public const int minNumOfProducts = 10;
    public const int minNumOfOrders = 20;
    public const int minNumOfOrderItems = 40;


    internal static Product[] ProductList = new Product[maxNumOfProducts];
    internal static Order[] OrderList = new Order[maxNumOfOrders];
    internal static OrderItem[] OrderItemList = new OrderItem[maxNumOfOrderItems];

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
            ProductList[i] = new Product();
            int number = (int)rand.NextInt64(namesCategoryArr.Length);
            ProductList[i].ID = Config.ProductID;
            ProductList[i].Name = namesCategoryArr[number].Item1;
            ProductList[i].Price = (int)rand.NextInt64(350, 3000);
            ProductList[i].Category = namesCategoryArr[number].Item2;
            ProductList[i].InStock = (int)rand.NextInt64(0, 200);
            Config.productIdx++;
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
            OrderList[i] = new Order();
            int index = (int)rand.NextInt64(customerDetailsArr.Length);
            OrderList[i].ID = Config.OrderID;
            OrderList[i].CustomerName = customerDetailsArr[index].Item1;
            OrderList[i].CustomerEmail = customerDetailsArr[index].Item2;
            OrderList[i].CustomerAdress = customerDetailsArr[index].Item3;
            OrderList[i].OrderDate = DateTime.MinValue;
            OrderList[i].ShipDate = OrderList[i].OrderDate + TimeSpan.FromDays(10);
            OrderList[i].DeliveryDate = OrderList[i].ShipDate + TimeSpan.FromDays(20);
            Config.orderIdx++;
        }
    }


    /// <summary>
    /// CreateOrderItemList function initializes the data in the array OrderItemList
    /// </summary>
    static private void CreateOrderItemList()
    {
        for (int i = 0; i < minNumOfOrderItems;)
        {
            int OrderIndex = (int)rand.NextInt64(Config.orderIdx);
            int numOfProducts = (int)rand.NextInt64(1, 4);
            for (int j = 0; j < numOfProducts; j++)
            {
                int ProductIndex = (int)rand.NextInt64(Config.productIdx);
                if (ProductList[ProductIndex].InStock == 0)
                    continue;
                OrderItemList[Config.orderItemIdx] = new OrderItem();
                OrderItemList[Config.orderItemIdx].ID = Config.OrderItemId;
                OrderItemList[Config.orderItemIdx].ProductID = ProductList[ProductIndex].ID;
                OrderItemList[Config.orderItemIdx].OrderID = OrderList[OrderIndex].ID;
                OrderItemList[Config.orderItemIdx].Amount = (int)rand.NextInt64(1, ProductList[ProductIndex].InStock);
                ProductList[ProductIndex].InStock -= OrderItemList[i].Amount;
                OrderItemList[Config.orderItemIdx].Price = OrderItemList[i].Amount * ProductList[ProductIndex].Price;
                Config.orderItemIdx++;
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
        public static int ProductID { get { productID++; return productID; }  }
        private static int orderID = 99999;
        public static int OrderID { get { orderID++; return orderID;  }  }
        private static int orderItemId = 99999;
        public static int OrderItemId { get { orderItemId++; return orderItemId; }  }


        internal static int productIdx = 0;
        internal static int orderIdx = 0;
        internal static int orderItemIdx = 0;
    }
}
