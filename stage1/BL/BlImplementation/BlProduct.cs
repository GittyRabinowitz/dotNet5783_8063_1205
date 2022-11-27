using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

using Dal;
namespace BlImplementation
{
    internal class BlProduct :IProduct
    {
        private DalApi.IDal Dal = new DalList();

        public IEnumerable<BO.ProductForList> GetProductList()
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.Get();

            List<BO.ProductForList> productList = new List<BO.ProductForList>();
            foreach (var item in existingProductsList)
            {
                BO.ProductForList p = new BO.ProductForList();
                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Category = (BO.eCategory)item.Category;
                productList.Add(p);
            }

            if (productList.Count() == 0)
                throw new BO.NoEntitiesFound("No products found");
            return productList;

        }

        public IEnumerable<BO.ProductItem> GetCatalog()
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.Get();

            List<BO.ProductItem> productList = new List<BO.ProductItem>();
            foreach (var item in existingProductsList)
            {
                BO.ProductItem p = new BO.ProductItem();

                p.ID = item.ID;
                p.Name = item.Name;
                p.Price = item.Price;
                p.Category = (BO.eCategory)item.Category;
                p.Amount = 1 במה לאתחל????
                p.InStock = item.InStock;
                productList.Add(p);
            }

            if (productList.Count() == 0)
                throw new BO.NoEntitiesFound("No products found");
            return productList;
        }

        public BO.Product GetProductManager(int id)
        {
            BO.Product BOProduct = new BO.Product();
            if (id > 0)
            {
                Dal.DO.Product DOProduct;
                try
                {
                    DOProduct = Dal.Product.GetSingle(id);

                }
                catch (DalApi.DalEntityNotFoundException exc)
                {
                    throw new BO.BlIdNotExist(exc);

                }

                BOProduct.ID = DOProduct.ID;
                BOProduct.Name = DOProduct.Name;
                BOProduct.Price = DOProduct.Price;
                BOProduct.Category = (BO.eCategory)DOProduct.Category;
                BOProduct.InStock = DOProduct.InStock;
                return BOProduct;
            }
            else
            {
                throw new BO.BlInvalideData("id cant be negative");
            }

        }

        public BO.Product GetProductCustomer(int id)
        {
            BO.Product BOProduct = new BO.Product();
            if (id > 0)
            {
                Dal.DO.Product DOProduct;
                try
                {
                    DOProduct = Dal.Product.GetSingle(id);

                }
                catch (DalApi.DalEntityNotFoundException exc)
                {
                    throw new BO.BlIdNotExist(exc);

                }

                BOProduct.ID = DOProduct.ID;
                BOProduct.Name = DOProduct.Name;
                BOProduct.Price = DOProduct.Price;
                BOProduct.Category = (BO.eCategory)DOProduct.Category;
                BOProduct.InStock = DOProduct.InStock;
                return BOProduct;
            }
            else
            {
                throw new BO.BlInvalideData("id cant be negative");
            }
        }


        public void Add(BO.Product BOProduct)
        {
            try
            {

            Dal.DO.Product DOProduct = new Dal.DO.Product();
            if (BOProduct.ID < 0)
                throw new BO.BlInvalideData("id cant be negative");

            if (BOProduct.Name == "")
                throw new BO.BlInvalideData("name cant be null");

            if (BOProduct.Price <= 0)
                throw new BO.BlInvalideData("price cant be zero or negative");

            if (BOProduct.InStock < 0)
                throw new BO.BlInvalideData("inStock cant be negative");
             

                DOProduct.ID = BOProduct.ID;
                DOProduct.Name = BOProduct.Name;
                DOProduct.Price = BOProduct.Price;
                DOProduct.Category = (Dal.DO.eCategory)BOProduct.Category;
                Dal.Product.Add(DOProduct);

            }
            catch (DalApi.DalEntityAlreadyExistException exc)
            {
                throw new BO.BlIdAlreadyExist(exc);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Dal.DO.Product DOProduct = Dal.Product.GetSingle(id);

            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);
            }
            IEnumerable<Dal.DO.OrderItem> orderItems= Dal.OrderItem.Get();
            foreach (var item in orderItems)
            {
                if (item.ProductID == id)
                {
                    throw new BO.BlProductExistInOrders("this product exist in orders");
                }
            }
            
            Dal.Product.Delete(id);
        }

        public void Update(BO.Product BOProduct)
        {
            
            Dal.DO.Product DOProduct = new Dal.DO.Product();
            if (BOProduct.ID < 0)
                throw new BO.BlInvalideData("id cant be negative");

            if (BOProduct.Name == "")
                throw new BO.BlInvalideData("name cant be null");

            if (BOProduct.Price <= 0)
                throw new BO.BlInvalideData("price cant be zero or negative");

            if (BOProduct.InStock < 0)
                throw new BO.BlInvalideData("inStock cant be negative");
            DOProduct.ID = BOProduct.ID;
            DOProduct.Name = BOProduct.Name;
            DOProduct.Price = BOProduct.Price;
            DOProduct.Category = (Dal.DO.eCategory)BOProduct.Category;
            try
            {
                Dal.Product.Update(DOProduct);
            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);
            }
        }
    }
}
