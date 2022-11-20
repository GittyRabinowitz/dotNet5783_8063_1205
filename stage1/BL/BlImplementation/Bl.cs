using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace BlImplementation
{
   sealed public class Bl:IBl
    {
        public ICart Cart => new BlCart();
        public IOrder Order => new BlOrder();
        public IOrderForList OrderForList => new BlOrderForList();
        public IOrderItem OrderItem => new BlOrderItem();
        public IOrderTracking OrderTracking => new BlOrderTracking();
        public IProduct Product => new BlProduct();
        public IProductForList ProductForList => new BlProductForList();
        public IProductItem ProductItem => new BlProductItem();
    }
}
