﻿using BlApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;

internal class BlProduct : IProduct
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();



    /// <summary>
    /// The function fetches all products from DataSource.ProductList and for each of them creates a productforlist obj
    /// and returns them
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlNoEntitiesFound"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList> GetProductList()
    {
        try
        {
            IEnumerable<Dal.DO.Product> existingProductsList;
            lock (Dal)
            {
                existingProductsList = Dal.Product.Get();
            }

            List<BO.ProductForList> productsForList = new List<BO.ProductForList>();




            productsForList.AddRange(existingProductsList.Select(DoProduct =>
            {
                BO.ProductForList ProductForList = new BO.ProductForList();

                ProductForList.ID = DoProduct.ID;
                ProductForList.Name = DoProduct.Name;
                ProductForList.Price = DoProduct.Price;
                ProductForList.Category = (BO.eCategory)DoProduct.Category;
                return ProductForList;
            }));

            return productsForList;

        }
        catch (DalApi.DalNoEntitiesFound exc)
        {
            throw new BO.BlNoEntitiesFoundInDal(exc);
        }
        catch (Dal.DalNoEntitiesFound exc)
        {
            throw new BO.BlNoEntitiesFoundInDal(exc);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList> GetProductByCategoty(BO.eCategory category)
    {
        try
        {
            IEnumerable<Dal.DO.Product> lst;
            lock (Dal)
            {
                lst = Dal.Product.Get(p => p.Category == (Dal.DO.eCategory)category);
            }
            List<BO.ProductForList> productsForList = new List<BO.ProductForList>();


            productsForList.AddRange(lst.Select(DoProduct =>
               {
                   BO.ProductForList ProductForList = new BO.ProductForList();

                   ProductForList.ID = DoProduct.ID;
                   ProductForList.Name = DoProduct.Name;
                   ProductForList.Price = DoProduct.Price;
                   ProductForList.Category = (BO.eCategory)DoProduct.Category;
                   return ProductForList;
               }).ToList());



            return productsForList;
        }
        catch (DalApi.DalNoEntitiesFound exc)
        {

            throw new BO.BlNoEntitiesFoundInDal(exc);
        }
        catch (Dal.DalNoEntitiesFound exc)
        {

            throw new BO.BlNoEntitiesFoundInDal(exc);
        }
    }



    /// <summary>
    /// The function fetches all products from DataSource.ProductList and for each of them creates a productitem obj
    /// and returns them
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlNoEntitiesFound"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem> GetCatalog(BO.eCategory? category)
    {
        try
        {
            IEnumerable<Dal.DO.Product> existingProductsList;
            if (category != null)
            {
                lock (Dal)
                {
                    existingProductsList = Dal.Product.Get(p => (BO.eCategory)p.Category == category);
                }
            }
            else
            {
                lock (Dal)
                {
                    existingProductsList = Dal.Product.Get();
                }
            }

            List<BO.ProductItem> productItemsList = new List<BO.ProductItem>();


            productItemsList.AddRange(existingProductsList.Select(DoProduct =>
            {
                BO.ProductItem BoProductItem = new BO.ProductItem();

                BoProductItem.ID = DoProduct.ID;
                BoProductItem.Name = DoProduct.Name;
                BoProductItem.Price = DoProduct.Price;
                BoProductItem.Category = (BO.eCategory)DoProduct.Category;
                BoProductItem.Amount = 0;
                if (DoProduct.InStock > 0)
                {
                    BoProductItem.InStock = true;
                }
                else
                {
                    BoProductItem.InStock = false;
                }
                return BoProductItem;
            }));


            return productItemsList;

        }
        catch (DalApi.DalNoEntitiesFound exc)
        {

            throw new BO.BlNoEntitiesFoundInDal(exc);
        }
        catch (Dal.DalNoEntitiesFound exc)
        {

            throw new BO.BlNoEntitiesFoundInDal(exc);
        }
    }



    /// <summary>
    /// The function fetches a specific product with the id the function got
    /// </summary>
    /// <param name="id"></param>
    /// <returns>BO.Product</returns>
    /// <exception cref="BO.BlInvalideData"></exception>
    /// <exception cref="BO.BlIdNotExist"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product GetProductManager(int id)
    {
        try
        {
            BO.Product BoProduct = new BO.Product();
            if (id > 0)
            {
                //Dal.DO.Product DoProduct = new Dal.DO.Product();
                //DoProduct = Dal.Product.GetSingle(id);

                Dal.DO.Product DoProduct;
                lock (Dal) 
                {
                    DoProduct = Dal.Product.GetSingle(p => p.ID == id);
                }
                  
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
        catch (Dal.DalIdNotFoundException exc)
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem GetProductCustomer(int id, BO.Cart cart)
    {
        try
        {

            if (id > 0)
            {
                //Dal.DO.Product DoProduct = Dal.Product.GetSingle(id);
                Dal.DO.Product DoProduct;
                lock (Dal) 
                {
                    DoProduct = Dal.Product.GetSingle(p => p.ID == id);
                }


                BO.ProductItem BoProductItem = new BO.ProductItem();

                BoProductItem.ID = DoProduct.ID;
                BoProductItem.Name = DoProduct.Name;
                BoProductItem.Price = DoProduct.Price;
                BoProductItem.Category = (BO.eCategory)DoProduct.Category;
                BoProductItem.Amount = 0;
                if (DoProduct.InStock > 0)
                {
                    BoProductItem.InStock = true;
                }
                else
                {
                    BoProductItem.InStock = false;
                }


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
        catch (Dal.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);

        }
    }



    /// <summary>
    /// the function gets a BO.Product creates with its details a DO.Product and adds it to the data layer
    /// </summary>
    /// <param name="BoProduct"></param>
    /// <exception cref="BO.BlInvalideData"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(BO.Product BoProduct)
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
        int productId;
        lock (Dal)
        {
            productId = Dal.Product.Add(DoProduct);
        }
        return productId;

    }




    /// <summary>
    /// the function gets aproduct id and deletes this product 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlIdNotExist"></exception>
    /// <exception cref="BO.BlProductExistInOrders"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        try
        {
            Dal.DO.Product DoProduct;
            lock (Dal)
            {
                DoProduct = Dal.Product.GetSingle(p => p.ID == id);
            }

            IEnumerable<Dal.DO.OrderItem> orderItems = Dal.OrderItem.Get();

            var items = from DoOrderItem in orderItems
                        where DoOrderItem.ProductID == id
                        select DoOrderItem;
            if (items.ToList().Count() > 0)
            {
                throw new BO.BlProductExistInOrders("this product exist in orders");
            }
            lock (Dal)
            {
                Dal.Product.Delete(id);
            }

        }
        catch (DalApi.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
        catch (BO.BlProductExistInOrders)
        {
            throw new BO.BlProductExistInOrders("this product exist in orders");
        }
        catch (DalApi.DalNoEntitiesFound exc)
        {
            throw new BO.BlNoEntitiesFoundInDal(exc);
        }
        catch (Dal.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
        catch (Dal.DalNoEntitiesFound exc)
        {
            throw new BO.BlNoEntitiesFoundInDal(exc);
        }
    }




    /// <summary>
    /// the function gets a BO.Product creates with its details a DO.Product and updates it to the data layer
    /// </summary>
    /// <param name="BOProduct"></param>
    /// <exception cref="BO.BlIdNotExist"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(BO.Product BOProduct)
    {
        try
        {
            //Dal.DO.Product DoProduct = Dal.Product.GetSingle(BOProduct.ID);
            Dal.DO.Product DoProduct;
            lock (Dal)
            {
                DoProduct = Dal.Product.GetSingle(p => p.ID == BOProduct.ID);
            }



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
        catch (Dal.DalIdNotFoundException exc)
        {
            throw new BO.BlIdNotExist(exc);
        }
    }


}
