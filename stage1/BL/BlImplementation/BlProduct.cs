using BlApi;
using Dal;
namespace BlImplementation;

internal class BlProduct : IProduct
{
    private DalApi.IDal Dal = new DalList();



    /// <summary>
    /// The function fetches all products from DataSource.ProductList and for each of them creates a productforlist obj
    /// and returns them
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlNoEntitiesFound"></exception>
    public IEnumerable<BO.ProductForList> GetProductList()
    {
        try
        {
            IEnumerable<Dal.DO.Product> existingProductsList = Dal.Product.Get();

            List<BO.ProductForList> productsForList = new List<BO.ProductForList>();
            foreach (Dal.DO.Product DoProduct in existingProductsList)
            {
                BO.ProductForList ProductForList = new BO.ProductForList();

                ProductForList.ID = DoProduct.ID;
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
    public IEnumerable<BO.ProductForList> GetProductByCategoty(BO.eCategory category)
    {
        // IEnumerable<Dal.DO.Product> lst = Dal.Product.GetProductByCategory((Dal.DO.eCategory)category);
        IEnumerable<Dal.DO.Product> lst = Dal.Product.Get(p => p.Category == (Dal.DO.eCategory)category);
        List<BO.ProductForList> productsForList = new List<BO.ProductForList>();


        foreach (Dal.DO.Product DoProduct in lst)
        {
            BO.ProductForList ProductForList = new BO.ProductForList();

            ProductForList.ID = DoProduct.ID;
            ProductForList.Name = DoProduct.Name;
            ProductForList.Price = DoProduct.Price;
            ProductForList.Category = (BO.eCategory)DoProduct.Category;
            productsForList.Add(ProductForList);
        }


        if (productsForList.Count() == 0)
            throw new BO.BlNoEntitiesFound("No products found");

        return productsForList;
    }



    /// <summary>
    /// The function fetches all products from DataSource.ProductList and for each of them creates a productitem obj
    /// and returns them
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlNoEntitiesFound"></exception>
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
                BoProductItem.Amount = 0;
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
        }
    }



    /// <summary>
    /// The function fetches a specific product with the id the function got
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Product</returns>
    /// <exception cref="BO.BlInvalideData"></exception>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public BO.Product GetProductManager(int id)
    {
        try
        {
            BO.Product BoProduct = new BO.Product();
            if (id > 0)
            {
                //Dal.DO.Product DoProduct = new Dal.DO.Product();
                //DoProduct = Dal.Product.GetSingle(id);

                Dal.DO.Product DoProduct = Dal.Product.GetSingleByPredicate(p => p.ID == id);
                BoProduct.ID = DoProduct.ID;
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
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);

        }

    }



    /// <summary>
    ///  The function fetches a specific product with the id the function got
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Product</returns>
    /// <exception cref="BO.BlInvalideData"></exception>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public BO.ProductItem GetProductCustomer(int id, BO.Cart cart)
    {
        try
        {

            if (id > 0)
            {
                //Dal.DO.Product DoProduct = Dal.Product.GetSingle(id);
                Dal.DO.Product DoProduct = Dal.Product.GetSingleByPredicate(p => p.ID == id);


                BO.ProductItem BoProductItem = new BO.ProductItem();

                BoProductItem.ID = BO.BoConfig.ProductID;
                BoProductItem.Name = DoProduct.Name;
                BoProductItem.Price = DoProduct.Price;
                BoProductItem.Category = (BO.eCategory)DoProduct.Category;
                BoProductItem.Amount = 0;
                BoProductItem.InStock = DoProduct.InStock;

                return BoProductItem;
            }
            else
            {
                throw new BO.BlInvalideData("id cant be negative");
            }


        }
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);

        }
    }



    /// <summary>
    /// the function gets a BO.Product creates with its details a DO.Product and adds it to the data layer
    /// </summary>
    /// <param name="BoProduct"></param>
    /// <exception cref="BO.BlInvalideData"></exception>
    public void Add(BO.Product BoProduct)
    {

        Dal.DO.Product DoProduct = new Dal.DO.Product();
        if (BoProduct.ID < 0)
            throw new BO.BlInvalideData("id cant be negative");

        if (BoProduct.Name == "")
            throw new BO.BlInvalideData("name cant be null");

        if (BoProduct.Price <= 0)
            throw new BO.BlInvalideData("price cant be zero or negative");

        if ((int)BoProduct.Category != 1 && (int)BoProduct.Category != 2 && (int)BoProduct.Category != 3)
            throw new BO.BlInvalideData("gategory does not exist");

        if (BoProduct.InStock < 0)
            throw new BO.BlInvalideData("inStock cant be negative");


        DoProduct.ID = 0;
        DoProduct.Name = BoProduct.Name;
        DoProduct.Price = BoProduct.Price;
        DoProduct.Category = (Dal.DO.eCategory)BoProduct.Category;
        DoProduct.InStock = BoProduct.InStock;

        Dal.Product.Add(DoProduct);

    }




    /// <summary>
    /// the function gets aproduct id and deletes this product 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlIdNotExist"></exception>
    /// <exception cref="BO.BlProductExistInOrders"></exception>
    public void Delete(int id)
    {
        try
        {
            //Dal.DO.Product DoProduct = Dal.Product.GetSingle(id);
            Dal.DO.Product DoProduct = Dal.Product.GetSingleByPredicate(p => p.ID == id);


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
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
        catch (BO.BlProductExistInOrders)
        {
            throw new BO.BlProductExistInOrders("this product exist in orders");
        }
    }




    /// <summary>
    /// the function gets a BO.Product creates with its details a DO.Product and updates it to the data layer
    /// </summary>
    /// <param name="BOProduct"></param>
    /// <exception cref="BO.BlIdNotExist"></exception>
    public void Update(BO.Product BOProduct)
    {
        try
        {
            //Dal.DO.Product DoProduct = Dal.Product.GetSingle(BOProduct.ID);
            Dal.DO.Product DoProduct = Dal.Product.GetSingleByPredicate(p => p.ID == BOProduct.ID);



            if (BOProduct.Name != null)
                DoProduct.Name = BOProduct.Name;

            if (BOProduct.Price != 0)
                DoProduct.Price = BOProduct.Price;

            if (BOProduct.Category != 0)
                DoProduct.Category = (Dal.DO.eCategory)BOProduct.Category;


            if (BOProduct.InStock > 0)
                DoProduct.InStock = BOProduct.InStock;


            Dal.Product.Update(DoProduct);

        }

        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
    }


}
