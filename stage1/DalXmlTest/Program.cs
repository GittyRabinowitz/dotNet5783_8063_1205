// See https://aka.ms/new-console-template for more information
using Dal.DO;
using DalApi;
Console.WriteLine("Hello, World!");

//Dal.DalXml d = new Dal.DalXml();
IDal d = DalApi.Factory.Get();

//DalXml d = new DalXml();



//==============product======================
Product product = new Product();
product.ID = 100000;
product.Name = "5050505";
product.Price = 5050;
product.InStock = 5050;
product.Category = eCategory.otherRoom;

//d?.Product.Add(product);
//d?.Product.decreaseInStock(100003,1);
//d?.Product.Delete(100003);

//var product1 = d?.Product.Get();
//var product2 = d?.Product.Get(p=>p.ID==100001);
//var product3 = d?.Product.GetSingle(p=>p.ID==100000);
//d?.Product.Update(product);
Console.WriteLine("success");


//====================order

Order order = new Order();
order.ID = 1;
order.CustomerName = "aaaa";
order.CustomerAdress = "ccccc";
order.CustomerEmail = "bbb";
order.OrderDate = DateTime.Now;
order.ShipDate = DateTime.Today;
order.DeliveryDate = DateTime.Now;

//d.Order.Add(order);
//d.Order.Delete(100015);
//d.Order.Delete(24);
//d.Order.Get();
//d.Order.Get(o=>o.CustomerName== "ttami");

//Console.WriteLine(d.Order.Get());
//d.Order.GetSingle(o => o.CustomerName == "ttami");
//d.Order.Update(order);

Console.WriteLine("finish");

//=====================orderitem
OrderItem oi = new OrderItem();
oi.ID = 100001;
oi.OrderID = 100002;
oi.ProductID = 100003;
oi.Price = 50;
oi.Amount = 5;
d.OrderItem.Add(oi);
d.OrderItem.Delete(100000);
d.OrderItem.Get();
d.OrderItem.Get(oi=>oi.ID==100000);
d.OrderItem.GetSingle(oi => oi.Price == 50);
d.OrderItem.Update(oi);


Console.WriteLine("we finisheddddddddddd");
