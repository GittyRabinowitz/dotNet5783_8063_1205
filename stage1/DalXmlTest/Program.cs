// See https://aka.ms/new-console-template for more information
using Dal.DO;
using DalApi;
Console.WriteLine("Hello, World!");
//using DO;

//using Dal;
//Dal.DalXml d = new Dal.DalXml();
IDal d = DalApi.Factory.Get();

//DalXml d = new DalXml();
//====================order

Order order = new Order();
order.ID = 24;
order.CustomerName = "ttami";
order.CustomerAdress = null;
order.CustomerEmail = "ssuri";
order.OrderDate = DateTime.Now;
order.ShipDate = DateTime.Today;
order.DeliveryDate = null;

d.Order.Add(order);
//d.Order.Delete(3);

Console.WriteLine(d.Order.Get());

d.Order.Update(order);

Console.WriteLine("finish");


//==============product======================
//Product product = new Product();
//product.ID = 100000;
//product.Name = "5050505";
//product.Price = 5050;
//product.InStock = 5050;
//product.Category = eCategory.otherRoom;

//d?.Product.Add(product);
//d?.Product.decreaseInStock(100003,1);
//d?.Product.Delete(100003);

//var product1 = d?.Product.Get();
//var product2 = d?.Product.Get(p=>p.ID==100001);
//var product3 = d?.Product.GetSingle(p=>p.ID==100000);
//d?.Product.Update(product);
Console.WriteLine("success");
