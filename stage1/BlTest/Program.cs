using BlImplementation;

/// <summary>
/// Global variables
/// </summary>
Bl bl = new Bl();
BO.Cart cart = new BO.Cart();
cart.Items = new List<BO.OrderItem>();




/// <summary>
/// The function fetches all orders 
/// </summary>
void getOrders()
{
    IEnumerable<BO.OrderForList> orderList = bl.Order.GetOrderList();
    foreach (var item in orderList)
    {
        Console.WriteLine(item);
    }
}


/// <summary>
/// The function brings all the order details
/// </summary>
void getOrderItem()
{
    Console.WriteLine("enter order id");

    int orderId;
    if (!(int.TryParse(Console.ReadLine(), out orderId)))
        throw new BO.BlInvalideData("invalid integer");

    BO.Order BoOrder = bl.Order.GetOrderDetails(orderId);
    Console.WriteLine(BoOrder);
}


/// <summary>
/// The function updates the date the order was sent
/// </summary>
void updateOrderShipping()
{
    Console.WriteLine("enter order id");

    int orderId;
    if (!(int.TryParse(Console.ReadLine(), out orderId)))
        throw new BO.BlInvalideData("invalid integer");

    BO.Order BoOrder = bl.Order.updateShippedOrder(orderId);
    Console.WriteLine(BoOrder);
}


/// <summary>
/// The function updates the date the order delivered
/// </summary>
void updateOrderDelivery()
{
    Console.WriteLine("enter order id");

    int orderId;
    if (!(int.TryParse(Console.ReadLine(), out orderId)))
        throw new BO.BlInvalideData("invalid integer");

    BO.Order BoOrder = bl.Order.updateDeliveryedOrder(orderId);
    Console.WriteLine(BoOrder);
}


/// <summary>
/// The function offers the user to perform actions on orders:
/// receive all orders, receive order details, update shipping date and update arrival date
/// </summary>
void orders()
{
    Console.WriteLine("enter the choice: 1.get orders list 2. get order items. 3.update shipping 4.update delivery");
    int choice;
    if (!(int.TryParse(Console.ReadLine(), out choice)))
        throw new BO.BlInvalideData("invalid integer");

    switch (choice)
    {
        case 1:
            getOrders();
            break;
        case 2:
            getOrderItem();
            break;
        case 3:
            updateOrderShipping();
            break;
        case 4:
            updateOrderDelivery();
            break;
        default:
            break;
    }

}



/// <summary>
/// The function fetches all products 
/// </summary>
void getProducts()
{
    IEnumerable<BO.ProductForList> orderList = bl.Product.GetProductList();
    foreach (var item in orderList)
    {
        Console.WriteLine(item);
    }
}


/// <summary>
/// The function brings all the products details
/// </summary>
void getCatalog()
{
    IEnumerable<BO.ProductItem> productItems = bl.Product.GetCatalog();
    foreach (var item in productItems)
    {
        Console.WriteLine(item);
    }
}


/// <summary>
/// The function fetches a specific product
/// </summary>
void getProductManager()
{
    Console.WriteLine("enter product id");

    int productId;
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new BO.BlInvalideData("invalid integer");

    BO.Product item = bl.Product.GetProductManager(productId);
    Console.WriteLine(item);
}



/// <summary>
/// The function fetches a specific product
/// </summary>
void getProductCustomer()
{
    Console.WriteLine("enter product id");

    int productId;
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new BO.BlInvalideData("invalid integer");

    BO.Product item = bl.Product.GetProductCustomer(productId);
    Console.WriteLine(item);
}


/// <summary>
/// The function receives product data and adds a product
/// </summary>
void addProduct()
{
    BO.Product product = new BO.Product();

    product.ID = BO.BoConfig.ProductID;

    Console.WriteLine("enter product name");
    product.Name = Console.ReadLine();

    Console.WriteLine("enter product price");
    product.Price = Convert.ToDouble(Console.ReadLine());

    Console.WriteLine("enter the Product's category: 1 - kitchen, 2 - washRoom, 3 - otherRoom");
    int choice = Convert.ToInt32(Console.ReadLine());
    product.Category = (BO.eCategory)choice;

    Console.WriteLine("enter product amount in stock");
    product.InStock = Convert.ToInt32(Console.ReadLine());

    bl.Product.Add(product);
}


/// <summary>
/// The function deletes a product
/// </summary>
void deleteProduct()
{
    Console.WriteLine("enter product id");

    int productId;
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new BO.BlInvalideData("invalid integer");

    bl.Product.Delete(productId);
}



/// <summary>
/// The function receives product data and updates a product
/// </summary>
void updateProduct()
{
    BO.Product BoProduct = new BO.Product();
    Console.WriteLine("enter product id");
    BoProduct.ID = int.Parse(Console.ReadLine());


    string name;
    Console.WriteLine("enter new name for the Product");
    name = Console.ReadLine();
    if (!string.IsNullOrEmpty(name))
        BoProduct.Name = name;

    Console.WriteLine("enter the new category for the Product: 1 - kitchen, 2 - washRoom, 3 - otherRoom");
    string choice1;
    choice1 = Console.ReadLine();
    if (!string.IsNullOrEmpty(choice1))
    {
        int choice2 = Convert.ToInt32(choice1);
        BoProduct.Category = (BO.eCategory)choice2;
    }

    Console.WriteLine("enter the new price for the Product");
    string price1 = Console.ReadLine();
    if (!string.IsNullOrEmpty(price1))
    {
        double price2 = Convert.ToDouble(price1);
        BoProduct.Price = price2;
    }

    Console.WriteLine("enter the new amount in stock");
    string inStock1 = Console.ReadLine();
    if (!string.IsNullOrEmpty(inStock1))
    {
        int inStock2 = Convert.ToInt32(inStock1);
        BoProduct.InStock = inStock2;
    }
    else
        BoProduct.InStock = -1;

    bl.Product.Update(BoProduct);
}



/// <summary>
/// The function offers the user to perform actions on products:
/// receive all products, receive products details, get a specific product, add, delete and update product
/// </summary>
void products()
{

    Console.WriteLine("enter the choice: 1.get products list 2. get catalog. 3.get product manager 4. get product customer 5.add product 6.delete product 7.update product");

    int choice;
    if (!(int.TryParse(Console.ReadLine(), out choice)))
        throw new BO.BlInvalideData("invalid integer");


    switch (choice)
    {
        case 1:
            getProducts();
            break;
        case 2:
            getCatalog();
            break;
        case 3:
            getProductManager();
            break;
        case 4:
            getProductCustomer();
            break;
        case 5:
            addProduct();
            break;
        case 6:
            deleteProduct();
            break;
        case 7:
            updateProduct();
            break;
        default:

            break;
    }
}



/// <summary>
/// The function receives a product ID from the user and adds the product to the cart
/// </summary>
void addProductToCart()
{
    Console.WriteLine("enter product id");
    int productId;
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new BO.BlInvalideData("invalid integer");
    cart = bl.Cart.Add(cart, productId);
}



/// <summary>
/// The function receives from the user a product ID and quantity and updates the quantity of the product in the cart
/// </summary>
void updateProductAmount()
{
    int productId, newAmount;

    Console.WriteLine("enter product id");
    if (!(int.TryParse(Console.ReadLine(), out productId)))
        throw new BO.BlInvalideData("invalid integer");

    Console.WriteLine("enter new amount for the product");
    if (!(int.TryParse(Console.ReadLine(), out newAmount)))
        throw new BO.BlInvalideData("invalid integer");

    cart = bl.Cart.Update(cart, productId, newAmount);
}



/// <summary>
/// The function receives user data from the user and updates the cart details to the data layer
/// </summary>
void confirmCart()
{

    Console.WriteLine("enter the customer's name");
    string CustomerName = Console.ReadLine();
    if (string.IsNullOrEmpty(CustomerName))
        throw new BO.BlNullValueException();

    Console.WriteLine("enter the customer's email");
    string CustomerEmail = Console.ReadLine();
    if (string.IsNullOrEmpty(CustomerEmail))
        throw new BO.BlNullValueException();

    Console.WriteLine("enter the customer's address");
    string CustomerAddress = Console.ReadLine();
    if (string.IsNullOrEmpty(CustomerAddress))
        throw new BO.BlNullValueException();

    bl.Cart.CartConfirmation(cart, CustomerName, CustomerEmail, CustomerAddress);
}



/// <summary>
/// The function offers the user to perform actions on carts:
/// add product to cart, update product amount and confirm cart
/// </summary>
void carts()
{
    Console.WriteLine("enter the choice: 1.add product to cart 2. update product amount 3.confirm cart");

    int choice;
    if (!(int.TryParse(Console.ReadLine(), out choice)))
        throw new BO.BlInvalideData("invalid integer");

    switch (choice)
    {
        case 1:
            addProductToCart();
            break;
        case 2:
            updateProductAmount();
            break;
        case 3:
            confirmCart();
            break;
        default:
            break;
    }
}




/// <summary>
/// The main program allows the user to perform actions on products, orders and carts
/// </summary>
void main()
{
    int choice;

    try
    {
        do
        {
            Console.WriteLine("enter the entity number: 1. order 2. product 3. cart  0. to exit");

            if (!(int.TryParse(Console.ReadLine(), out choice)))
                throw new BO.BlInvalideData("invalid integer");

            switch (choice)
            {
                case 0:
                    return;
                case 1:
                    orders();
                    break;
                case 2:
                    products();
                    break;
                case 3:
                    carts();
                    break;
                default:
                    Console.WriteLine("wrong choice");
                    break;

            }

        } while (choice != 0);
    }
    catch (BO.BlIdAlreadyExist exc)
    {
        Console.WriteLine("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
    }
    catch (BO.BlIdNotExist exc)
    {
        Console.WriteLine("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
    }
    catch (BO.BlInvalideData exc)
    {

        Console.WriteLine("exception: " + exc.Message);
    }
    catch (BO.BlUpdateException exc)
    {
        Console.WriteLine("exception: " + exc.Message);
    }
    catch (BO.BlNullValueException exc)
    {
        Console.WriteLine("exception: " + exc.Message);
    }
    catch (BO.BlOutOfStockException exc)
    {
        Console.WriteLine("exception: " + exc.Message);
    }
    catch (BO.BlProductExistInOrders exc)
    {
        Console.WriteLine("exception: " + exc.Message);
    }
    catch (BO.BlNoEntitiesFound exc)
    {
        Console.WriteLine("exception: " + exc.Message);
    }
    catch (BO.BlNoEntitiesFoundInDal exc)
    {
        Console.WriteLine("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
    }
}

main();

