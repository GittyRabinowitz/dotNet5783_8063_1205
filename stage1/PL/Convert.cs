using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
