using BlApi;
using Dal;
namespace BlImplementation
{
    internal class BlProduct : IProduct
    {
        private DalApi.IDal Dal = new DalList();

        public IEnumerable<BO.ProductForList> GetProductList()
        {
            try
            {
                IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.Get();

                List<BO.ProductForList> productsForList = new List<BO.ProductForList>();
                foreach (Dal.DO.Product DoProduct in existingProductsList)
                {
                    BO.ProductForList ProductForList = new BO.ProductForList();
                    ProductForList.ID = BO.BoConfig.ProductForListID;
                    ProductForList.Name = DoProduct.Name;
                    ProductForList.Price = DoProduct.Price;
                    ProductForList.Category = (BO.eCategory)DoProduct.Category;
                    productsForList.Add(ProductForList);
                }

                if (productsForList.Count() == 0)
                    throw new BO.BlNoEntitiesFound("No products found");
                return productsForList;
            }
            catch (BO.BlNoEntitiesFound)
            {
                throw new BO.BlNoEntitiesFound("No products found");
            }

        }

        public IEnumerable<BO.ProductItem> GetCatalog()
        {
            try
            {

                IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.Get();

                List<BO.ProductItem> productItemsList = new List<BO.ProductItem>();
                foreach (Dal.DO.Product DoProduct in existingProductsList)
                {
                    BO.ProductItem BoProductItem = new BO.ProductItem();

                    BoProductItem.ID = DoProduct.ID;
                    BoProductItem.Name = DoProduct.Name;
                    BoProductItem.Price = DoProduct.Price;
                    BoProductItem.Category = (BO.eCategory)DoProduct.Category;
                    BoProductItem.Amount = 1;//האם לאתחך בזה???
                    BoProductItem.InStock = DoProduct.InStock;
                    productItemsList.Add(BoProductItem);
                }

                if (productItemsList.Count() == 0)
                    throw new BO.BlNoEntitiesFound("No products found");
                return productItemsList;

            }
            catch (BO.BlNoEntitiesFound)
            {
                throw new BO.BlNoEntitiesFound("No products found");
                throw;
            }
        }

        public BO.Product GetProductManager(int id)
        {
            try
            {


                BO.Product BoProduct = new BO.Product();
                if (id > 0)
                {
                    Dal.DO.Product DoProduct = new Dal.DO.Product();

                    DoProduct = Dal.Product.GetSingle(id);

                    BoProduct.ID = BO.BoConfig.ProductID;
                    BoProduct.Name = DoProduct.Name;
                    BoProduct.Price = DoProduct.Price;
                    BoProduct.Category = (BO.eCategory)DoProduct.Category;
                    BoProduct.InStock = DoProduct.InStock;
                    return BoProduct;
                }
                else
                {
                    throw new BO.BlInvalideData("id cant be negative");
                }
            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);

            }

        }

        public BO.Product GetProductCustomer(int id)
        {
            try
            {

                BO.Product BoProduct = new BO.Product();
                if (id > 0)
                {
                    Dal.DO.Product DoProduct = new Dal.DO.Product();

                    DoProduct = Dal.Product.GetSingle(id);



                    BoProduct.ID = BO.BoConfig.ProductID;
                    BoProduct.Name = DoProduct.Name;
                    BoProduct.Price = DoProduct.Price;
                    BoProduct.Category = (BO.eCategory)DoProduct.Category;
                    BoProduct.InStock = DoProduct.InStock;
                    return BoProduct;
                }
                else
                {
                    throw new BO.BlInvalideData("id cant be negative");
                }

            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);

            }
        }


        public void Add(BO.Product BoProduct)
        {
            try
            {

                Dal.DO.Product DoProduct = new Dal.DO.Product();
                if (BoProduct.ID < 0)
                    throw new BO.BlInvalideData("id cant be negative");

                if (BoProduct.Name == "")
                    throw new BO.BlInvalideData("name cant be null");

                if (BoProduct.Price <= 0)
                    throw new BO.BlInvalideData("price cant be zero or negative");

                if (BoProduct.InStock < 0)
                    throw new BO.BlInvalideData("inStock cant be negative");


                DoProduct.ID = DataSource.Config.ProductID;
                DoProduct.Name = BoProduct.Name;
                DoProduct.Price = BoProduct.Price;
                DoProduct.Category = (Dal.DO.eCategory)BoProduct.Category;
                DoProduct.InStock = BoProduct.InStock;
                Dal.Product.Add(DoProduct);

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
                Dal.DO.Product DoProduct = Dal.Product.GetSingle(id);

                IEnumerable<Dal.DO.OrderItem> orderItems = Dal.OrderItem.Get();
                foreach (Dal.DO.OrderItem DoOrderItem in orderItems)
                {
                    if (DoOrderItem.ProductID == id)
                    {
                        throw new BO.BlProductExistInOrders("this product exist in orders");
                    }
                }

                Dal.Product.Delete(id);

            }
            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);
            }
            catch (BO.BlProductExistInOrders)
            {
                throw new BO.BlProductExistInOrders("this product exist in orders");
            }
        }

        public void Update(BO.Product BOProduct)
        {
            try
            {
                Dal.DO.Product DoProduct = Dal.Product.GetSingle(BOProduct.ID);

                if(BOProduct.Name!=null)
                    DoProduct.Name = BOProduct.Name;

                if (BOProduct.Price!=0)
                    DoProduct.Price = BOProduct.Price;

                if(BOProduct.Category!=0)
                    DoProduct.Category = (Dal.DO.eCategory)BOProduct.Category;


                if (BOProduct.InStock > 0)
                    DoProduct.InStock = BOProduct.InStock;


                Dal.Product.Update(DoProduct);

            }

            catch (DalApi.DalEntityNotFoundException exc)
            {
                throw new BO.BlIdNotExist(exc);
            }
        }
    }
}