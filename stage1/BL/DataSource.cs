
using Dal.DO;
namespace Dal;

static public class DataSource
{
    public static readonly int Rand;

   public const int numOfProducts = 50;
    public const int numOfOrders = 100;
    public const int numOfOrderItems = 200;

    internal static Product[] ProductList = new Product[numOfProducts];
    internal static Order[] OrderList = new Order[numOfOrders];
    internal static OrderItem[] OrderItemList = new OrderItem[numOfOrderItems];

    static Random rand = new Random();
 
  
    static private void CreateProductList()
    {
        string[] productNames = { "כסא" , "שולחן" };
        for (int i = 0; i < 10; i++, Config.productIdx++)
        {
            ProductList[i] = new Product();
            int number = (int)rand.NextInt64(productNames.Length);
            ProductList[i].ID = Config.ProductID;


            ProductList[i].Name = productNames[number];
            ProductList[i].Price = (int)rand.NextInt64(350,5000);
            int x= (int)rand.NextInt64(1, 3);
            ProductList[i].Category = (eCategory)x;
            ProductList[i].InStock = (int)rand.NextInt64(50);
        }
    }
    static private void CreateOrderList()
    {
        string[] customerNames = { "Gitty", "Tova" };
        string[] customerEmails = { "g@g.com", "t@t.com" };
        string[] customerAdresses = { "achinoam", "eidelson" };
       
        for (int i = 0; i < 20; i++,Config.orderIdx++)
        {
            TimeSpan PeriodOrderToShip = TimeSpan.FromDays(10);
            TimeSpan PeriodShipToDelivery = TimeSpan.FromDays(20);
            OrderList[i] = new Order();
            int index = (int)rand.NextInt64(customerNames.Length);

            OrderList[i].ID = Config.OrderID;
            OrderList[i].CustomerName = customerNames[index];
            OrderList[i].CustomerEmail = customerEmails[index];
            OrderList[i].CustomerAdress = customerAdresses[index];
            OrderList[i].OrderDate = DateTime.MinValue;
            //OrderList[i].ShipDate = OrderList[i].OrderDate+PeriodOrderToShip;
            //OrderList[i].DeliveryDate =OrderList[i].ShipDate+PeriodShipToDelivery;
            OrderList[i].ShipDate = OrderList[i].OrderDate+ TimeSpan.FromDays(10);
            OrderList[i].DeliveryDate =OrderList[i].ShipDate+TimeSpan.FromDays(20);
        }
    }
    static private void CreateOrderItemList()
    {

        for (int i = 0; i < 40; i++)
        {
            int OrderIndex = (int)rand.NextInt64(OrderList.Length);
            int numOfProducts = (int)rand.NextInt64(1, 4);
            for (int j = 0; j < numOfProducts; j++,Config.orderItemIdx++)
            {
                int ProductIndex = (int)rand.NextInt64(ProductList.Length);
                OrderItemList[i] = new OrderItem();
                OrderItemList[i].ID=Config.OrderItemId;
                OrderItemList[i].ProductID = ProductList[ProductIndex].ID;
                OrderItemList[i].OrderID = OrderList[OrderIndex].ID;
                OrderItemList[i].Amount = (int)rand.NextInt64(1, ProductList[ProductIndex].InStock);
                ProductList[ProductIndex].InStock-=OrderItemList[i].Amount;
                OrderItemList[i].Price = OrderItemList[i].Amount*ProductList[ProductIndex].Price;
            }
        } 
    }
    static private void s_Initialize()
    {
        CreateProductList();
        CreateOrderList();
        CreateOrderItemList();
    }
    static DataSource()
    {
        s_Initialize();
    }
    public static class Config
    {
        private static int productID = 100000;
        public static int ProductID { get { return productID++; } }
        private static int orderID = 100000;
        public static int OrderID { get { return orderID++; } }
        private static int orderItemId = 100000;
        public static int OrderItemId { get { return orderItemId++; } }


        internal static int productIdx = 0;
        internal static int orderIdx = 0;
        internal static int orderItemIdx = 0;
    }
}

