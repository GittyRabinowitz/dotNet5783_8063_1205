// See https://aka.ms/new-console-template for more information
using Dal.DO;

Console.WriteLine("Hello, World!");
//using DO;

//using Dal;
Dal.DalXml d = new Dal.DalXml();
//DalXml d = new DalXml();


Order order = new Order();
order.ID = 24;
order.CustomerName = "ttami";
order.CustomerAdress = null;
order.CustomerEmail = "ssuri";
order.OrderDate = null;
order.ShipDate = DateTime.Today;
order.DeliveryDate = null;
d.Order.Add(order);
////d.Order.Delete(3);

//Console.WriteLine(d.Order.Read());

//d.Order.Update(order);

//Console.WriteLine("finish");


//Product product = new Product();
//product.ID = 111111;
//product.Name = "new";
//product.Price = 5.5;
//product.InStock = 15;
//product.Category = eCategory.kitchen;

//d.Product.Add(product);
//Console.WriteLine("success");
