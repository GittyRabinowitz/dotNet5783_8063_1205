using BlApi;
using Dal;

namespace BlImplementation;

internal class BlCart : ICart
{
    private DalApi.IDal Dal = new DalList();



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

            Dal.DO.Product DoProduct = Dal.Product.GetSingleByPredicate(p=>p.ID==id);


            bool flag = true;


            foreach (var item in cart.Items)
            {
                if (item.ProductID == id)
                {
                    if (DoProduct.InStock <= 0)
                        throw new BO.BlOutOfStockException("This product is out of stock");

                    item.Amount += 1;
                    item.TotalPrice += DoProduct.Price;
                    cart.TotalPrice += DoProduct.Price;

                    flag = false;
                }
            }

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
                cart.Items.Add(BoOrderItem);
                cart.TotalPrice += DoProduct.Price;

            }

            return cart;
        }
        catch (DalApi.DalIdNotFoundException exc)
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
    public BO.Cart Update(BO.Cart cart, int id, int newAmount)
    {
        try
        {
            bool flag = true;

            // Dal.DO.Product DoProduct = Dal.Product.GetSingle(id);

            Dal.DO.Product DoProduct = Dal.Product.GetSingleByPredicate(p=>p.ID==id);

            foreach (var item in cart.Items)
            {
                if (item.ProductID == id)
                {
                    flag = false;
                    if (newAmount > item.Amount)
                    {
                        if (newAmount - item.Amount > DoProduct.InStock)
                            throw new BO.BlOutOfStockException("This product is not available in this amount");

                        item.TotalPrice += item.Price * (newAmount - item.Amount);
                        cart.TotalPrice += item.Price * (newAmount - item.Amount);
                        item.Amount = newAmount;
                    }
                    else if (newAmount == 0)
                    {
                        cart.TotalPrice -= item.TotalPrice;
                        cart.Items.Remove(item);
                    }
                    else if (newAmount < item.Amount)
                    {
                        cart.TotalPrice -= item.Price * (item.Amount - newAmount);

                        item.TotalPrice = item.Price * newAmount;
                        item.Amount = newAmount;
                    }

                    break;
                }
            }


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



                cart.TotalPrice += BoOrderItem.TotalPrice;

            }

            return cart;
        }
        catch (DalApi.DalIdNotFoundException exc)
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

            foreach (BO.OrderItem oi in cart.Items)
            {
                //Dal.DO.Product DoProduct = Dal.Product.GetSingle(oi.ProductID);

                Dal.DO.Product DoProduct = Dal.Product.GetSingleByPredicate(p=>p.ID==oi.ProductID);

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

            }
        }
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
    }
}


