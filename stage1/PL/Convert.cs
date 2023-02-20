using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Data;
using BO;
namespace PL
{
    internal class Convert
    {
        public static BO.ProductForList convertProductToProductForList(BO.Product product)
        {
            BO.ProductForList productForList = new BO.ProductForList()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category
            };

            return productForList;
        }


        public static BO.OrderForList convertOrderToOrderForList(BO.Order order)
        {
            BO.OrderForList orderForList = new BO.OrderForList()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                AmountOfItems = order.Items.Count
            };
            return orderForList;
        }
    }


    public class ShipedToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (eOrderStatus)value == 0 ? "Visible" : "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DeliveryToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (eOrderStatus)value == (eOrderStatus)1 ? "Visible" : "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
