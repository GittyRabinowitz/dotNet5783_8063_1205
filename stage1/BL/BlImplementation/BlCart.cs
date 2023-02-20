using BlApi;
using System.Collections.ObjectModel;

namespace BlImplementation;

internal class BlCart : ICart
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();



    /// <summary>
    /// the functuon gets a cart and product id and adds this product to the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlOutOfStockException"></exception>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public BO.Cart Add(BO.Cart cart, int id)
    {
        try
        {
            //Dal.DO.Product DoProduct = Dal.Product.GetSingle(id);

            Dal.DO.Product DoProduct = Dal.Product.GetSingle(p => p.ID == id);


            bool flag = true;



            cart?.Items?.ForEach(item =>
            {
                if (item?.ProductID == id)
                {
                    if (DoProduct.InStock <= 0)
                        throw new BO.BlOutOfStockException("This product is out of stock");

                    item.Amount += 1;
                    item.TotalPrice += DoProduct.Price;
                    cart.TotalPrice += DoProduct.Price;

                    flag = false;
                }
            });


            if (flag)
            {
                if (DoProduct.InStock <= 0)
                    throw new BO.BlOutOfStockException("This product is out of stock");

                BO.OrderItem BoOrderItem = new BO.OrderItem();

                BoOrderItem.ID = BO.BoConfig.OrderItemID;
                BoOrderItem.Name = DoProduct.Name;
                BoOrderItem.ProductID = id;
                BoOrderItem.Amount = 1;
                BoOrderItem.Price = DoProduct.Price;
                BoOrderItem.TotalPrice = DoProduct.Price;
                if (cart?.Items == null)
                {
                    cart.Items = new List<BO.OrderItem>();
                }
                cart.Items.Add(BoOrderItem);
                cart.TotalPrice += DoProduct.Price;

            }

            return cart;
        }
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
        catch (Dal.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }

    }



    /// <summary>
    /// the functuon gets a cart and product id and new amount and updates this product's amount in the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlOutOfStockException"></exception>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public BO.Cart Update(BO.Cart cart, int id, int newAmount, ObservableCollection<BO.OrderItem> items)
    {
        try
        {
            bool flag = true;



            Dal.DO.Product DoProduct = Dal.Product.GetSingle(p => p.ID == id);



            int num = cart.Items.Count;

            for (int i = 0; i < num; i++)
            {
                if (cart.Items[i]?.ProductID == id)
                {
                    flag = false;
                    if (newAmount > cart.Items[i].Amount)
                    {
                        if (newAmount - cart.Items[i].Amount > DoProduct.InStock)
                            throw new BO.BlOutOfStockException("This product is not available in this amount");
                        BO.OrderItem item = items[i];
                        cart.Items[i].TotalPrice += cart.Items[i].Price * (newAmount - cart.Items[i].Amount);

                        item.TotalPrice = cart.Items[i].TotalPrice;

                        cart.TotalPrice += cart.Items[i].Price * (newAmount - cart.Items[i].Amount);
                        cart.Items[i].Amount = newAmount;
                        item.Amount = newAmount;
                        DoProduct.InStock -= (newAmount - cart.Items[i].Amount);
                        items.RemoveAt(i);

                        items.Insert(i, item);
                    }
                    else if (newAmount == 0)
                    {
                        cart.TotalPrice -= cart.Items[i].TotalPrice;

                        DoProduct.InStock += cart.Items[i].Amount;
                        cart.Items.Remove(cart.Items[i]);
                        items.RemoveAt(i);
                    }
                    else if (newAmount < cart.Items[i].Amount)
                    {
                        cart.TotalPrice -= cart.Items[i].Price * (cart.Items[i].Amount - newAmount);
                        BO.OrderItem item = items[i];
                        cart.Items[i].TotalPrice = cart.Items[i].Price * newAmount;
                        cart.Items[i].Amount = newAmount;
                        item.TotalPrice = cart.Items[i].TotalPrice;
                        item.Amount = newAmount;
                        items.RemoveAt(i);

                        items.Insert(i, item);
                        DoProduct.InStock += (cart.Items[i].Amount - newAmount);
                    }
                    break;
                };
            }
            //cart?.Items?.ForEach(item =>
            //{

            //    if (item?.ProductID == id)
            //    {
            //        flag = false;
            //        if (newAmount > item.Amount)
            //        {
            //            if (newAmount - item.Amount > DoProduct.InStock)
            //                throw new BO.BlOutOfStockException("This product is not available in this amount");

            //            item.TotalPrice += item.Price * (newAmount - item.Amount);
            //            cart.TotalPrice += item.Price * (newAmount - item.Amount);
            //            item.Amount = newAmount;
            //            DoProduct.InStock -= (newAmount - item.Amount);
            //        }
            //        else if (newAmount == 0)
            //        {
            //            cart.TotalPrice -= item.TotalPrice;
            //            cart.Items.Remove(item);
            //            DoProduct.InStock += item.Amount;
            //        }
            //        else if (newAmount < item.Amount)
            //        {
            //            cart.TotalPrice -= item.Price * (item.Amount - newAmount);

            //            item.TotalPrice = item.Price * newAmount;
            //            item.Amount = newAmount;

            //            DoProduct.InStock += (item.Amount - newAmount);
            //        }
            //    };
            //});



            if (flag && newAmount <= DoProduct.InStock)
            {
                BO.OrderItem BoOrderItem = new BO.OrderItem();
                BoOrderItem.ID = BO.BoConfig.OrderItemID;

                BoOrderItem.Name = DoProduct.Name;
                BoOrderItem.ProductID = DoProduct.ID;
                BoOrderItem.Price = DoProduct.Price;
                BoOrderItem.Amount = newAmount;
                BoOrderItem.TotalPrice = DoProduct.Price * newAmount;
                cart.Items.Add(BoOrderItem);
                items.Add(BoOrderItem);




                cart.TotalPrice += BoOrderItem.TotalPrice;
                DoProduct.InStock -= newAmount;
            }

            return cart;
        }
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
        catch (Dal.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }

    }



    /// <summary>
    /// the function gets cart and user details and creates DO.Order and DO.OrderItem with the details of the cart.Items
    /// and adds it to the data layer
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="customerName"></param>
    /// <param name="customerEmail"></param>
    /// <param name="customerAddress"></param>
    /// <exception cref="BO.BlNullValueException"></exception>
    /// <exception cref="BO.BlInvalideData"></exception>
    /// <exception cref="BO.BlOutOfStockException"></exception>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public void CartConfirmation(BO.Cart cart, string customerName, string customerEmail, string customerAddress)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(customerEmail);
            bool isValidEmail = (addr.Address == customerEmail);

            if (customerName == "" || customerAddress == "")
            {
                throw new BO.BlNullValueException();
            }
            if (!isValidEmail)
            {
                throw new BO.BlInvalideData("invalid email");
            }

            bool canContine = true;
            int productInStock;

            Dal.DO.Order DoOrder = new Dal.DO.Order();

            DoOrder.ID = 0;
            DoOrder.OrderDate = DateTime.Now;
            DoOrder.ShipDate = DateTime.MinValue;
            DoOrder.DeliveryDate = DateTime.MinValue;
            DoOrder.CustomerName = customerName;
            DoOrder.CustomerEmail = customerEmail;
            DoOrder.CustomerAdress = customerAddress;

            int orderId = Dal.Order.Add(DoOrder);

            cart?.Items?.ForEach(oi =>
            {



                Dal.DO.Product DoProduct = Dal.Product.GetSingle(p => p.ID == oi.ProductID);

                if (oi.Amount < 0)
                {
                    throw new BO.BlInvalideData("negative value");
                }
                if (DoProduct.InStock < oi.Amount)
                {
                    throw new BO.BlOutOfStockException("This product is out of stock");
                }
                if (canContine)
                {
                    Dal.DO.OrderItem DoOrderItem = new Dal.DO.OrderItem();

                    DoOrderItem.ID = 0;
                    DoOrderItem.ProductID = oi.ProductID;
                    DoOrderItem.OrderID = orderId;
                    DoOrderItem.Amount = oi.Amount;
                    DoOrderItem.Price = oi.Price;

                    Dal.OrderItem.Add(DoOrderItem);

                    Dal.Product.decreaseInStock(DoProduct.ID, oi.Amount);

                }

            });


        }
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
        catch (Dal.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
    }
}


