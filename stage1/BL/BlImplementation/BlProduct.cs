using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using Dal;
namespace BlImplementation
{
    internal class BlProduct:BlApi.IProduct
    {
        private IDal Dal = new DalList();

        public void Add(Product p)
        {
            //Id,name,price,category
            IEnumerable<Product> j = Dal.Product.Get();
            var jh =  Dal.Product.Get();
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductForList> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetCatalog()
        {
            throw new NotImplementedException();
        }

        public Product GetProductCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductManager(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }
    }
}
