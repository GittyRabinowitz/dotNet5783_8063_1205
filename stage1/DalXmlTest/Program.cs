using Dal.DO;
using DalApi;


/// <summary>
/// this file tests all functions we wrote in DalXml project
/// (DalProduct.cs, DalOrder.cs, DalOrderItem.cs)
/// </summary>
/// 


IDal? d = DalApi.Factory.Get();


/// <summary>
/// creating Product and trying DalProduct.cs functions
/// </summary>

Product product = new Product();
product.Name = "washMachine";
product.Price = 1120;
product.InStock = 50;
product.Category = eCategory.washRoom;

d?.Product.Add(product);
d?.Product.decreaseInStock(100003, 1);
d?.Product.Delete(100003);
var product1 = d?.Product.Get();
var product2 = d?.Product.Get(p => p.ID == 100001);
var product3 = d?.Product.GetSingle(p => p.ID == 100000);
d?.Product.Update(product);



/// <summary>
/// creating Order and trying DalOrder.cs functions
/// </summary>

Order order = new Order();
order.CustomerName = "Gitty";
order.CustomerAdress = "Achinoam";
order.CustomerEmail = "g@g";
order.OrderDate = DateTime.Now;
order.ShipDate = DateTime.Today;
order.DeliveryDate = DateTime.Now;

d?.Order.Add(order);
d?.Order.Delete(100002);
d?.Order.Get();
d?.Order.Get(o => o.CustomerName == "ttami");
d?.Order.GetSingle(o => o.CustomerName == "ttami");
d?.Order.Update(order);


/// <summary>
/// creating Order and trying DalOrder.cs functions
/// </summary>

OrderItem oi = new OrderItem();
oi.ID = 100001;
oi.OrderID = 100002;
oi.ProductID = 100003;
oi.Price = 50;
oi.Amount = 5;

d?.OrderItem.Add(oi);
d?.OrderItem.Delete(100000);
d?.OrderItem.Get();
d?.OrderItem.Get(oi => oi.ID == 100000);
d?.OrderItem.GetSingle(oi => oi.Price == 50);
d?.OrderItem.Update(oi);


