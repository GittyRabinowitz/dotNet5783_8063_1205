using Dal;
using Dal.DO;
using Dal.UseObjects;

int choice;
do
{
    Console.WriteLine("enter your choice:" +
    " 0- exit," +
    " 1- product crud," +
    " 2- order crud," +
    " 3- order item crud");
    choice=Convert.ToInt32(Console.ReadLine());
    switch (choice)
    {
        case (int)eEntityOptions.exit:
            break;
        case (int)eEntityOptions.product:
            productCrud();
            break;
        case (int)eEntityOptions.order:
            orderCrud();
            break;
        case (int)eEntityOptions.orderItem:
            orderItemCrud();
            break;
        default:
            break;
    }
} while (choice!=0);

//========================================================product
void AddProduct()
{
    string name;
    double price;
    eCategory category;
    int inStock;

    Console.WriteLine("enter name for the new product");
    name=Console.ReadLine();
    Console.WriteLine("enter the product's category: 1 - , 2 - , 3 - ");
    //להוסיף שמות של קטגוריות
    int choice;
    choice=Convert.ToInt32(Console.ReadLine());
    category = (eCategory)choice;
    Console.WriteLine("enter price for the new product");
    price= (Convert.ToDouble(Console.ReadLine()));



    Console.WriteLine("enter amount in stock");
    inStock=Convert.ToInt32(Console.ReadLine());
    Product currentProduct = new Product();

    currentProduct.ID=DataSource.Config.ProductID;
    currentProduct.Name=name;
    currentProduct.Price=price;
    currentProduct.Category=category;
    currentProduct.InStock=inStock;
    DalProduct.Create(currentProduct);
}

void UpdateProduct()
{
    int id;
    bool flag = true;
    Console.WriteLine("enter the id of the product you want to update");
    id=Convert.ToInt32(Console.ReadLine());
    Product p = new Product();
    p=DalProduct.ReadSingle(id);
    if (p.ID!=null)
    {
        string name;
        double price;
        eCategory category;
        int inStock;
        Console.WriteLine("enter new name for the product");
        name=Console.ReadLine();
        Console.WriteLine("enter the new category for the product: 1 - , 2 - , 3 - ");
        //להוסיף שמות של קטגוריות
        int choice;
        choice=Convert.ToInt32(Console.ReadLine());
        category = (eCategory)choice;
        Console.WriteLine("enter the new price for the product");
        price=Console.Read();
        Console.WriteLine("enter the new amount in stock");
        inStock=Convert.ToInt32(Console.ReadLine());
        if (name!=null)
            p.Name=name;
        if (price!=null)
            p.Price=price;
        if (category!=null)
            p.Category=category;
        if (inStock!=null)
            p.InStock=inStock;
        DalProduct.Update(p);
        flag=false;
    }
    if (flag)
        throw new NotImplementedException("the item you want to update does not exist");
}


void productCrud()
{
    int choice;
    Console.WriteLine("enter your choice:" +
        " 1- add product," +
        " 2- view product by id," +
        " 3- view all products," +
        " 4- update product by id," +
        " 5- delete product");
    choice=Convert.ToInt32(Console.ReadLine());
    int id;

    switch (choice)
    {
        case (int)eCrudOptions.add:
            AddProduct();
            break;
        case (int)eCrudOptions.viewById:
            Console.WriteLine("Enter product id to view");
            id=Convert.ToInt32(Console.ReadLine());
            Product p = DalProduct.ReadSingle(id);
            Console.WriteLine(p);
            break;
        case (int)eCrudOptions.viewAll:
            Product[] ProductList = DalProduct.Read();
            foreach (Product product in ProductList)
            { Console.WriteLine(product); }

            break;
        case (int)eCrudOptions.update:
            UpdateProduct();
            break;
        case (int)eCrudOptions.delete:
            Console.WriteLine("Enter product id to delete");
            id=Convert.ToInt32(Console.ReadLine());
            DalProduct.Delete(id);
            break;
        default:
            break;
    }
}
//==========================================================order

void AddOrder()
{
    string customerName;
    string customerEmail;
    string customerAdress;
    Console.WriteLine("enter customer name for the new order");
    customerName=Console.ReadLine();
    Console.WriteLine("enter customer email for the new order");
    customerEmail=Console.ReadLine();
    Console.WriteLine("enter customer adress for the new order");
    customerAdress=Console.ReadLine();

    Order currentOrder = new Order();

    currentOrder.ID=DataSource.Config.OrderID;
    currentOrder.CustomerName=customerName;
    currentOrder.CustomerEmail=customerEmail;
    currentOrder.CustomerAdress=customerAdress;
    currentOrder.OrderDate=DateTime.Now; ;
    currentOrder.ShipDate=currentOrder.OrderDate+TimeSpan.FromDays(10);
    currentOrder.DeliveryDate=currentOrder.ShipDate+ TimeSpan.FromDays(20);
    DalOrder.Create(currentOrder);

}

void UpdateOrder()
{
    int id;
    bool flag = true;
    Console.WriteLine("enter the id of the order you want to update");
    id=Convert.ToInt32(Console.ReadLine());
    Order o = DalOrder.ReadSingle(id);
    if (o.ID!=null)
    {
        string customerName;
        string customerEmail;
        string customerAdress;
        DateTime orderDate;
        DateTime shipDate;
        DateTime deliveryDate;

        Console.WriteLine("enter new customer name for the order");
        customerName=Console.ReadLine();
        Console.WriteLine("enter new customer email for the order");
        customerEmail=Console.ReadLine();
        Console.WriteLine("enter new customer adress for the order");
        customerAdress=Console.ReadLine();

        //לא למחוק
        Console.WriteLine("enter new order date for the order");
        orderDate=Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("enter new ship date for the order");
        shipDate=Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("enter new delivery date for the order");
        deliveryDate=Convert.ToDateTime(Console.ReadLine());

        if (customerName!=null)
            o.CustomerName=customerName;
        if (customerEmail!=null)
            o.CustomerEmail=customerEmail;
        if (customerAdress!=null)
            o.CustomerAdress=customerAdress;

        if (orderDate!=null)
            o.OrderDate=orderDate;
        if (shipDate!=null)
            o.ShipDate=shipDate;
        if (deliveryDate!=null)
            o.DeliveryDate=deliveryDate;

        DalOrder.Update(o);
        flag=false;
    }
    if (flag)
        throw new NotImplementedException("the item you want to update does not exist");
}

void orderCrud()
{
    int choice;
    Console.WriteLine("enter your choice:" +
        " 1- add order," +
        " 2- view order by id," +
        " 3- view all orders," +
        " 4- update order by id," +
        " 5- delete order");
    choice=Convert.ToInt32(Console.ReadLine());
    int id;
    switch (choice)
    {
        case (int)eCrudOptions.add:
            AddOrder();
            break;
        case (int)eCrudOptions.viewById:
            Console.WriteLine("Enter order id to view");
            id=Convert.ToInt32(Console.ReadLine());
            Order o = new Order();
            o=DalOrder.ReadSingle(id);
            Console.WriteLine(o);
            break;
        case (int)eCrudOptions.viewAll:
            Order[] OrderList = DalOrder.Read();
            foreach (Order order in OrderList)
            { Console.WriteLine(order); }
            break;
        case (int)eCrudOptions.update:
            UpdateOrder();
            break;
        case (int)eCrudOptions.delete:
            Console.WriteLine("Enter order id to delete");
            id=Convert.ToInt32(Console.ReadLine());
            DalOrder.Delete(id);
            break;
        default:
            break;
    }
}

//==================================================order item

void AddOrderItem()
{
    int orderId;
    int productId;
    double price;
    int amount;

    //האם צריך לבדוק האם האי די של ההזמנה כבר קיים וכנל של המוצר
    Console.WriteLine("enter order id for the new order item");
    orderId=Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("enter product id for the new order item");
    productId=Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("enter price for the new order item");
    price=Console.Read();
    Console.WriteLine("enter amount for the new order item");
    amount=Convert.ToInt32(Console.ReadLine());
    OrderItem currentOrderItem = new OrderItem();

    currentOrderItem.ID=DataSource.Config.OrderItemId;
    currentOrderItem.OrderID=orderId;
    currentOrderItem.ProductID=productId;
    currentOrderItem.Amount=amount;
    currentOrderItem.Price=price;
    DalOrderItem.Create(currentOrderItem);
}

void UpdateOrderItem()
{
    int id;
    bool flag = true;
    Console.WriteLine("enter the id of the order item you want to update");
    id=Convert.ToInt32(Console.ReadLine());
    OrderItem currentOrderItem = DalOrderItem.ReadSingle(id);
    if (currentOrderItem.ID!=null)
    {
        int orderId;
        int productId;
        double price;
        int amount;

        Console.WriteLine("enter new order id for the order item");
        orderId=Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter new product id for the order item");
        productId=Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter new price for the order item");
        price=Console.Read();


        Console.WriteLine("enter new amount for the order item");
        amount=Convert.ToInt32(Console.ReadLine());


        if (orderId!=null)
            currentOrderItem.OrderID=orderId;
        if (productId!=null)
            currentOrderItem.ProductID=productId;
        if (price!=null)
            currentOrderItem.Price=price;
        if (amount!=null)
            currentOrderItem.Amount=amount;
        DalOrderItem.Update(currentOrderItem);
        flag=false;
    }
    if (flag)
        throw new NotImplementedException("the order item you want to update does not exist");
}

void orderItemCrud()
{
    int choice;
    Console.WriteLine("enter your choice:" +
        " 1- add order item," +
        " 2- view order item by id," +
        " 3- view all order items," +
        " 4- update order item by id," +
        " 5- delete order, " +
        " 6- view order item by order id and product id," +
        " 7- view order item by order id");
    choice=Convert.ToInt32(Console.ReadLine());
    int id;
    int orderId;
    int productId;
    OrderItem currentOrderItem;
    OrderItem[] orderItemList;
    switch (choice)
    {
        case (int)eOrderItemOptions.add:
            AddOrderItem();
            break;
        case (int)eOrderItemOptions.viewById:
            Console.WriteLine("Enter order item id to view");
            id=Convert.ToInt32(Console.ReadLine());
            currentOrderItem = DalOrderItem.ReadSingle(id);
            Console.WriteLine(currentOrderItem);
            break;
        case (int)eOrderItemOptions.viewAll:
            orderItemList = DalOrderItem.Read();
            foreach (OrderItem orderItem in orderItemList)
            { Console.WriteLine(orderItem); }
            break;
        case (int)eOrderItemOptions.update:
            UpdateOrderItem();
            break;
        case (int)eOrderItemOptions.delete:
            Console.WriteLine("Enter order item id to delete");
            id=Convert.ToInt32(Console.ReadLine());
            DalOrderItem.Delete(id);
            break;
        case (int)eOrderItemOptions.viewByorderIdAndProductId:
            Console.WriteLine("Enter order id of the order item to view");
            orderId=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter product id of the order item to view");
            productId=Convert.ToInt32(Console.ReadLine());
            currentOrderItem= DalOrderItem.ReadOrderItemByOrderIdAndProductId(orderId, productId);
            Console.WriteLine(currentOrderItem);
            break;
        case (int)eOrderItemOptions.viewByOrderId:
            Console.WriteLine("Enter order id of the order item to view");
            orderId = Convert.ToInt32(Console.ReadLine());
            orderItemList= DalOrderItem.ReadOrderItemByOrderId(orderId);
            foreach (OrderItem orderItem in orderItemList)
            { Console.WriteLine(orderItem); }
            break;
        default:
            break;
    }
}
