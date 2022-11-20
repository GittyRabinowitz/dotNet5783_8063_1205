using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IProduct
    {

        public IEnumerable<ProductForList> GetProductList();
        public IEnumerable<ProductItem> GetProductItems();
        public Product GetProduct(int id);

        public void Add(Product p);



    }
}
