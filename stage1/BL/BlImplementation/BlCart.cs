using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;

namespace BlImplementation
{
    internal class BlCart : ICart
    {
        private DalApi.IDal Dal = new DalList();

        public Cart Add(Cart cart, int id)
        {


            Dal.DO.Product DOProduct;
            bool flag = true;
            try
            {
                DOProduct = Dal.Product.GetSingle(id);
            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BlIdNotExist(exc);
            }
            foreach (var item in cart.Items)
            {
                if (item.ProductID == id)
                {
                    if (DOProduct.InStock > 0)
                    {
                        item.Amount += 1;
                        item.Price += DOProduct.Price;
                        cart.TotalPrice += DOProduct.Price;
                    }
                    flag = false;
                }
            }

            if (flag)
            {

                if (DOProduct.InStock > 0)
                {
                    BO.OrderItem newoi = new BO.OrderItem();
                    newoi.ID = //מה לשים במזהה?????
                    newoi.Name = DOProduct.Name;
                    newoi.ProductID = id;
                    newoi.Amount = 1;
                    newoi.Price = DOProduct.Price;
                    newoi.TotalPrice = DOProduct.Price;
                    cart.Items.Add(newoi);
                    cart.TotalPrice += DOProduct.Price;

                }
                else
                {
                    throw new BlOutOfStockException("This product is out of stock");

                }


            }
            return cart;
            throw new NotImplementedException();
        }

        public Cart Update(Cart cart, int id, int newAmount)
        {
            try
            {

                bool flag = true;
                Dal.DO.Product DOProduct = Dal.Product.GetSingle(id);

                foreach (var item in cart.Items)
                {
                    if (item.ProductID == id)
                    {
                        flag = false;
                        if (newAmount > item.Amount)
                        {
                            if (newAmount - item.Amount > DOProduct.InStock)
                                throw new BlOutOfStockException("This product is not available in this amount");

                            item.Amount = newAmount;
                            item.TotalPrice += item.Price * (newAmount - item.Amount);
                            cart.TotalPrice += item.Price * (newAmount - item.Amount);
                        }
                        else if (newAmount < item.Amount)
                        {
                            item.Amount = newAmount;
                            item.TotalPrice = item.Price * newAmount;
                            cart.TotalPrice -= item.Price * (item.Amount - newAmount);
                        }
                        else if (newAmount == 0)
                        {
                            cart.TotalPrice -= item.TotalPrice;
                            cart.Items.Remove(item);
                        }
                    }
                }
                if (flag && newAmount <= DOProduct.InStock)
                {
                    BO.OrderItem oi = new BO.OrderItem();
                    oi.ID=// מה לשים במזהה???? אולי DataSource.Config.OrderItemId;

                    oi.Name = DOProduct.Name;
                    oi.ProductID = DOProduct.ID;
                    oi.Price = DOProduct.Price;
                    oi.Amount = newAmount;
                    oi.TotalPrice = DOProduct.Price * newAmount;
                    cart.Items.Add(oi);
                }
                return cart;


            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BlIdNotExist(exc);
            }

        }



        public void CartConfirmation(Cart cart, string customerName, string customerEmail, string customerAddress)
        {

            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-
                9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
RegexOptions.CultureInvariant | RegexOptions.Singleline);
            Console.WriteLine($"The email is {customerEmail}");
            bool isValidEmail = regex.IsMatch(customerEmail);
            if (customerName == null || customerAddress == null)
            {
                throw new BlNullValueException();
            }
            if (!isValidEmail)
            {
                throw new BlInvalidEmailException();
            }
            bool canContine = true;
            int productInStock;



            Dal.DO.Order DoOrder = new Dal.DO.Order();
            DoOrder.ID = DataSource.Config.OrderID;
            DoOrder.OrderDate = DateTime.Now;
            DoOrder.ShipDate = DateTime.MinValue;
            DoOrder.DeliveryDate = DateTime.MinValue;
            DoOrder.CustomerName = customerName;
            DoOrder.CustomerEmail = customerEmail;
            DoOrder.CustomerAdress = customerAddress;
            int orderId;
            try
            {
                orderId = Dal.Order.Add(DoOrder);

            }
            catch (DalApi.DalEntityAlreadyExistException exc)
            {
                throw new BO.BlIdAlreadyExist(exc);
            }




            foreach (BO.OrderItem oi in cart.Items)
            {
                Dal.DO.Product DOProduct;
                try
                {
                    DOProduct = Dal.Product.GetSingle(oi.ProductID);
                }
                catch (DalApi.DalEntityNotFoundException exc)
                {
                    throw new BlIdNotExist(exc);
                }


                if (oi.Amount < 0)
                {
                    throw new BlNegativeAmountException();
                }
                if (DOProduct.InStock < oi.Amount)
                {
                    throw new BlOutOfStockException("This product is out of stock");
                }
                if (canContine)
                {
                    Dal.DO.OrderItem DoOrderItem = new Dal.DO.OrderItem();

                    DoOrderItem.ID = DataSource.Config.OrderItemId;
                    DoOrderItem.ProductID = oi.ProductID;
                    DoOrderItem.OrderID = orderId;
                    DoOrderItem.Amount = oi.Amount;
                    DoOrderItem.Price = oi.TotalPrice;

                    try
                    {
                        Dal.OrderItem.Add(DoOrderItem);

                    }
                    catch (DalApi.DalEntityAlreadyExistException exc)
                    {
                        throw new BO.BlIdAlreadyExist(exc);
                    }


                    Dal.Product.decreaseInStock(DOProduct.ID, oi.Amount);

                }
            }


            //catch (BlNegativeAmountException ex)
            //{
            //    throw new BlNegativeAmountException();
            //}
            //catch (BlOutOfStockException ex)
            //{
            //    throw new BlOutOfStockException();
            //}
            //catch (BlNullValueException ex)
            //{
            //    throw new BlNullValueException();
            //}
            //catch (BlInvalidEmailException ex)
            //{
            //    throw new BlInvalidEmailException();
            //}
            //catch (BlIdDoesntExistException ex)
            //{
            //    throw new BlIdDoesntExistException();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception();
            //}

        }
    }

}
//}
