using Dal;
using Dal.DO;
using DalApi;

/// <summary>
/// The main program offers the user to manage products, orders and order items in each of them,
/// you can create a new one, see a specific object, see all objects, update and delete
/// </summary>
/// 


IDal DalListEntity = new DalList();
int choice;
DataSource ds = new DataSource();


do
{
    Console.WriteLine("enter your choice:" +
    " 0- Exit," +
    " 1- Product crud," +
    " 2- order crud," +
    " 3- order item crud");
    choice = Convert.ToInt32(Console.ReadLine());

    try
    {
        switch (choice)
        {
            case (int)eEntityOptions.Exit:
                break;
            case (int)eEntityOptions.Product:
                productCrud();
                break;
            case (int)eEntityOptions.Order:
                orderCrud();
                break;
            case (int)eEntityOptions.OrderItem:
                orderItemCrud();
                break;
            default:
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }

} while (choice != 0);



/// <summary>
/// AddProduct function receives data from the user, creates an object from it
/// and sends the object to a function that will insert it into the array of products
/// </summary>
void AddProduct()
{
    Product currentProduct = new Product();
    currentProduct.ID = DataSource.Config.ProductID;
    Console.WriteLine("enter name for the new Product");
    currentProduct.Name = Console.ReadLine();
    Console.WriteLine("enter price for the new Product");
    currentProduct.Price = (Convert.ToDouble(Console.ReadLine()));
    Console.WriteLine("enter the Product's category: 1 - kitchen, 2 - washRoom, 3 - otherRoom");
    int choice = Convert.ToInt32(Console.ReadLine());
    currentProduct.Category = (eCategory)choice;
    Console.WriteLine("enter amount in stock");
    currentProduct.InStock = Convert.ToInt32(Console.ReadLine());
    DalListEntity.Product.Add(currentProduct);
}


/// <summary>
/// The function receives the ID of the object that the user wants to update,
/// then receives the data to update, updates the object and sends it to a function
/// that will insert it into the appropriate array
/// </summary>
void UpdateProduct()
{
    bool flag = true;
    Console.WriteLine("enter the id of the Product you want to Update");
    int id = Convert.ToInt32(Console.ReadLine());
    Product p = DalListEntity.Product.GetSingleByPredicate(p=>p.ID==id);

    if (p.ID != null)
    {
        Console.WriteLine("the product's details of the product you want to update:");
        Console.WriteLine(p);
        string name;
        Console.WriteLine("enter new name for the Product");
        name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name))
            p.Name = name;

        Console.WriteLine("enter the new category for the Product: 1 - kitchen, 2 - washRoom, 3 - otherRoom");
        string choice1;
        choice1 = Console.ReadLine();
        if (!string.IsNullOrEmpty(choice1))
        {
            int choice2 = Convert.ToInt32(choice1);
            p.Category = (eCategory)choice2;
        }

        Console.WriteLine("enter the new price for the Product");
        string price1 = Console.ReadLine();
        if (!string.IsNullOrEmpty(price1))
        {
            double price2 = Convert.ToDouble(price1);
            p.Price = price2;
        }

        Console.WriteLine("enter the new amount in stock");
        string inStock1 = Console.ReadLine();
        if (!string.IsNullOrEmpty(inStock1))
        {
            int inStock2 = Convert.ToInt32(inStock1);
            p.InStock = inStock2;
        }

        DalListEntity.Product.Update(p);
        flag = false;
    }
    if (flag)
        throw new NotImplementedException("the item you want to Update does not exist");
}


/// <summary>
/// the function productCrud offers the user to manage products,
/// you can create a new one, see a specific object, see all objects, update and delete
/// </summary>
void productCrud()
{
    int choice;
    Console.WriteLine("enter your choice:" +
        " 1- Add Product," +
        " 2- view Product by id," +
        " 3- view all products," +
        " 4- Update Product by id," +
        " 5- Delete Product");
    choice = Convert.ToInt32(Console.ReadLine());
    int id;
    try
    {
        switch (choice)
        {
            case (int)eCrudOptions.Add:
                AddProduct();
                break;
            case (int)eCrudOptions.ViewById:
                Console.WriteLine("Enter Product id to view");
                id = Convert.ToInt32(Console.ReadLine());
                Product p = DalListEntity.Product.GetSingleByPredicate(p=>p.ID==id);
                Console.WriteLine(p);
                break;
            case (int)eCrudOptions.ViewAll:
                IEnumerable<Product> ProductList = DalListEntity.Product.Get();
                foreach (Product product in ProductList)
                { Console.WriteLine(product); }
                break;
            case (int)eCrudOptions.Update:
                UpdateProduct();
                break;
            case (int)eCrudOptions.Delete:
                Console.WriteLine("Enter Product id to Delete");
                id = Convert.ToInt32(Console.ReadLine());
                DalListEntity.Product.Delete(id);
                break;
            default:
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}


/// <summary>
/// AddOrder function receives data from the user, creates an object from it
/// and sends the object to the function that will insert it into the array of orders
/// </summary>
void AddOrder()
{
    Order currentOrder = new Order();
    currentOrder.ID = DataSource.Config.OrderID;
    Console.WriteLine("enter customer name for the new order");
    currentOrder.CustomerName = Console.ReadLine();
    Console.WriteLine("enter customer email for the new order");
    currentOrder.CustomerEmail = Console.ReadLine();
    Console.WriteLine("enter customer adress for the new order");
    currentOrder.CustomerAdress = Console.ReadLine();
    currentOrder.OrderDate = DateTime.Now;
    currentOrder.ShipDate = currentOrder.OrderDate + TimeSpan.FromDays(10);
    currentOrder.DeliveryDate = currentOrder.ShipDate + TimeSpan.FromDays(20);
  DalListEntity.Order.Add(currentOrder);
}


/// <summary>
/// The function receives the ID of the object that the user wants to update,
/// then receives the data to update, updates the object and sends it to a function
/// that will insert it into the appropriate array
/// </summary>
void UpdateOrder()
{
    bool flag = true;
    Console.WriteLine("enter the id of the order you want to Update");
    int id = Convert.ToInt32(Console.ReadLine());
    Order o = DalListEntity.Order.GetSingleByPredicate(o=>o.ID==id);
    if (o.ID != null)
    {
        Console.WriteLine("the order's details of the order you want to update:");
        Console.WriteLine(o);
        string customerName;
        string customerEmail;
        string customerAdress;

        Console.WriteLine("enter new customer name for the order");
        customerName = Console.ReadLine();
        if (!string.IsNullOrEmpty(customerName))
            o.CustomerName = customerName;

        Console.WriteLine("enter new customer email for the order");
        customerEmail = Console.ReadLine();
        if (!string.IsNullOrEmpty(customerEmail))
            o.CustomerEmail = customerEmail;

        Console.WriteLine("enter new customer adress for the order");
        customerAdress = Console.ReadLine();
        if (!string.IsNullOrEmpty(customerAdress))
            o.CustomerAdress = customerAdress;

        DateTime orderDate;
        DateTime shipDate;
        DateTime deliveryDate;

        Console.WriteLine("enter new order date for the order");
        string orderDate1 = Console.ReadLine();
        if (!string.IsNullOrEmpty(orderDate1))
        {
            if (DateTime.TryParse(orderDate1, out orderDate))
                o.OrderDate = Convert.ToDateTime(orderDate1);
            else
                throw new Exception("you haven't entered a valid datetime value");
        }

        Console.WriteLine("enter new ship date for the order");
        string shipDate1 = Console.ReadLine();
        if (!string.IsNullOrEmpty(shipDate1))
        {
            if (DateTime.TryParse(shipDate1, out shipDate))
                o.ShipDate = Convert.ToDateTime(shipDate1);
            else
                throw new Exception("you haven't entered a valid datetime value");
        }

        Console.WriteLine("enter new delivery date for the order");
        string deliveryDate1 = Console.ReadLine();
        if (!string.IsNullOrEmpty(deliveryDate1))
        {
            if (DateTime.TryParse(deliveryDate1, out deliveryDate))
                o.DeliveryDate = Convert.ToDateTime(deliveryDate1);
            else
                throw new Exception("you haven't entered a valid datetime value");
        }
        DalListEntity.Order.Update(o);
        flag = false;
    }
    if (flag)
        throw new NotImplementedException("the item you want to Update does not exist");
}


/// <summary>
/// the function orderCrud offers the user to manage orders,
/// you can create a new one, see a specific object, see all objects, update and delete
/// </summary>
void orderCrud()
{
    int choice;
    Console.WriteLine("enter your choice:" +
        " 1- Add order," +
        " 2- view order by id," +
        " 3- view all orders," +
        " 4- Update order by id," +
        " 5- Delete order");
    choice = Convert.ToInt32(Console.ReadLine());
    int id;
    try
    {
        switch (choice)
        {
            case (int)eCrudOptions.Add:
                AddOrder();
                break;
            case (int)eCrudOptions.ViewById:
                Console.WriteLine("Enter order id to view");
                id = Convert.ToInt32(Console.ReadLine());
                Order currentOrder = DalListEntity.Order.GetSingleByPredicate(o=>o.ID==id);
                Console.WriteLine(currentOrder);
                break;
            case (int)eCrudOptions.ViewAll:
                IEnumerable<Order> OrderList = DalListEntity.Order.Get();
                foreach (Order order in OrderList)
                { Console.WriteLine(order); }
                break;
            case (int)eCrudOptions.Update:
                UpdateOrder();
                break;
            case (int)eCrudOptions.Delete:
                Console.WriteLine("Enter order id to Delete");
                id = Convert.ToInt32(Console.ReadLine());
                DalListEntity.Order.Delete(id);
                break;
            default:
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}


/// <summary>
/// AddOrderItem function receives data from the user, creates an object from it
/// and sends the object to the function that will insert it into the array of order items
/// </summary>
/// 
void AddOrderItem()
{
    OrderItem currentOrderItem = new OrderItem();
    Console.WriteLine("enter order id for the new order item");
    currentOrderItem.OrderID = Convert.ToInt32(Console.ReadLine());
    OrderItem oi = DalListEntity.OrderItem.GetSingleByPredicate(oi=>oi.OrderID==currentOrderItem.OrderID);
    if (oi.ID != null)
    {
        Console.WriteLine("enter Product id for the new order item");
        currentOrderItem.ProductID = Convert.ToInt32(Console.ReadLine());
        Product p =  DalListEntity.Product.GetSingleByPredicate(p=>p.ID==currentOrderItem.ProductID);

        if (p.ID != null)
        {
            Console.WriteLine("enter amount for the new order item");
            int amount = Convert.ToInt32(Console.ReadLine());
            if (amount <= p.InStock)
            {
                currentOrderItem.Amount = amount;
                DalListEntity.Product.decreaseInStock(p.ID, currentOrderItem.Amount);
                Console.WriteLine("enter price for the new order item");
                currentOrderItem.Price = Convert.ToInt32(Console.ReadLine());
                currentOrderItem.ID = DataSource.Config.OrderItemID;
               DalListEntity.OrderItem.Add(currentOrderItem);
            }
            else
            {
                throw new NotImplementedException("This Product does not exist in the requested quantity");
            }
        }

    }
}


/// <summary>
/// The function receives the ID of the object that the user wants to update,
/// then receives the data to update, updates the object and sends it to a function
/// that will insert it into the appropriate array
/// </summary>
void UpdateOrderItem()
{
    int id;
    bool flag = true;
    Console.WriteLine("enter the id of the order item you want to Update");
    id = Convert.ToInt32(Console.ReadLine());
    OrderItem currentOrderItem = DalListEntity.OrderItem.GetSingleByPredicate(oi=>oi.ID==id);

    if (currentOrderItem.ID != null)
    {
        Console.WriteLine("the order item's details of the order item you want to update:");
        Console.WriteLine(currentOrderItem);

        Console.WriteLine("enter new order id for the order item");
        string orderId1 = Console.ReadLine();
        if (!string.IsNullOrEmpty(orderId1))
        {
            int orderId2 = Convert.ToInt32(orderId1);
            Order o = DalListEntity.Order.GetSingleByPredicate(o=>o.ID==orderId2);
            if (o.ID != null)
                currentOrderItem.OrderID = orderId2;
            else
                throw new NotImplementedException("this order id does not exist");
        }

        Console.WriteLine("enter new Product id for the order item");
        string productId1 = Console.ReadLine();
        if (!string.IsNullOrEmpty(productId1))
        {
            int productId = Convert.ToInt32(productId1);
            Product p = DalListEntity.Product.GetSingleByPredicate(p=>p.ID==productId);
            if (p.ID != null)
                currentOrderItem.ProductID = productId;
            else
                throw new NotImplementedException("this Product id does not exist");
        }

        Console.WriteLine("enter new price for the order item");
        string price;
        price = Console.ReadLine();
        if (!string.IsNullOrEmpty(price))
        {
            currentOrderItem.Price = Convert.ToDouble(price);
        }

        string amount;
        Console.WriteLine("enter new amount for the order item");
        amount = Console.ReadLine();
        if (!string.IsNullOrEmpty(amount))
        {
            currentOrderItem.Amount = Convert.ToInt32(amount);
        }

       DalListEntity.OrderItem.Update(currentOrderItem);
        flag = false;
    }
    if (flag)
        throw new NotImplementedException("the order item you want to Update does not exist");
}


/// <summary>
/// the function orderItemCrud offers the user to manage order items,
/// you can create a new one, see a specific object by id, see all objects, update, delete
/// see a specific object by order id and product id, and see a specific object by order id
/// </summary>
void orderItemCrud()
{
    int choice;
    Console.WriteLine("enter your choice:" +
        " 1- Add order item," +
        " 2- view order item by id," +
        " 3- view all order items," +
        " 4- Update order item by id," +
        " 5- Delete order, " +
        " 6- view order item by order id and Product id," +
        " 7- view order item by order id");
    choice = Convert.ToInt32(Console.ReadLine());
    int id;
    int orderId;
    int productId;
    OrderItem currentOrderItem;
    IEnumerable<OrderItem> orderItemList;
    try
    {
        switch (choice)
        {
            case (int)eOrderItemOptions.Add:
                AddOrderItem();
                break;
            case (int)eOrderItemOptions.ViewById:
                Console.WriteLine("Enter order item id to view");
                id = Convert.ToInt32(Console.ReadLine());
                currentOrderItem =DalListEntity.OrderItem.GetSingleByPredicate(oi=>oi.ID==id);
                Console.WriteLine(currentOrderItem);
                break;
            case (int)eOrderItemOptions.ViewAll:
                orderItemList = DalListEntity.OrderItem.Get();
                foreach (OrderItem orderItem in orderItemList)
                { Console.WriteLine(orderItem); }
                break;
            case (int)eOrderItemOptions.Update:
                UpdateOrderItem();
                break;
            case (int)eOrderItemOptions.Delete:
                Console.WriteLine("Enter order item id to Delete");
                id = Convert.ToInt32(Console.ReadLine());
               DalListEntity.OrderItem.Delete(id);
                break;
            case (int)eOrderItemOptions.ViewByorderIdAndProductId:
                Console.WriteLine("Enter order id of the order item to view");
                orderId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Product id of the order item to view");
                productId = Convert.ToInt32(Console.ReadLine());
                currentOrderItem = DalListEntity.OrderItem.GetSingleByPredicate(oi=>oi.OrderID== orderId &&oi.ProductID==productId);
                Console.WriteLine(currentOrderItem);
                break;
            case (int)eOrderItemOptions.ViewByOrderId:
                Console.WriteLine("Enter order id of the order item to view");
                orderId = Convert.ToInt32(Console.ReadLine());
                orderItemList = DalListEntity.OrderItem.Get(oi=>oi.OrderID==orderId);
                foreach (OrderItem orderItem in orderItemList)
                { if (orderItem.ID != 0) Console.WriteLine(orderItem); }
                break;
            default:
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}
