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

        public IEnumerable<ProductForList> GetProductList();//manager
        public IEnumerable<ProductItem> GetCatalog();//catalog
        public Product GetProductManager(int id);
        public Product GetProductCustomer(int id);
        public void Add(Product p);//manager
        public void Delete(int id);//manager
        public void Update(Product p);//manager


    }
}
