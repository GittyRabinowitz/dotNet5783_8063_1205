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

            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductForList> GetProductList()
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.Get();

            List<ProductForList> productList=new List<ProductForList>();
            foreach (var item in existingProductsList)
            {
                ProductForList p=new ProductForList();
                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Category = (eCategory)item.Category;
                productList.Add(p);
            }

            if(productList.Count()==0)
                throw new NoEntitiesFound("No products found");
            return productList;

        }


        public IEnumerable<ProductItem> GetCatalog()
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.Get();

            List<ProductItem> productList = new List<ProductItem>();
            foreach (var item in existingProductsList)
            {
                ProductItem p = new ProductItem();
                
                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Category = (eCategory)item.Category;
                //p.Amount = 1; במה לאתחל????
                p.InStock = item.InStock;
                productList.Add(p);
            }

            if (productList.Count() == 0)
                throw new NoEntitiesFound("No products found");
            return productList;
        }

        public Product GetProductCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProductManager(int id)
        {
            Product localProduct=new Product();
            if (id > 0)
            {
                Dal.DO.Product product =  Dal.Product.GetSingle(id);
                localProduct.ID = product.ID;
                localProduct.Name = product.Name;
                localProduct.Price = product.Price;
                localProduct.Category = (eCategory)product.Category;
                localProduct.InStock = product.InStock;
                return localProduct;
            }
            else
            {
                throw new BO.EntityNotFoundException("this product does not exist");
            }
            
            
        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }
    }
}
