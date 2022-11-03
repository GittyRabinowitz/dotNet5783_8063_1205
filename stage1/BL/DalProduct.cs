
using Dal.DO;
namespace Dal.UseObjects;

/// <summary>
/// class for crud actions for a product
/// </summary>

public class DalProduct
{

    /// <summary>
    /// create function gets an object and insert it into the products array
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>the object's id</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static int Create(Product obj)
    {
        if (DataSource.Config.productIdx >= DataSource.maxNumOfProducts)
        {
            throw new Exception("There is no space available for your Product");

        }
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (DataSource.ProductList[i].ID == obj.ID)
            {
                throw new Exception("this Product already exist");

            }
        }
        DataSource.ProductList[DataSource.Config.productIdx++] = obj;
        return obj.ID;
    }


    /// <summary>
    /// delete function gets an id of the object requsted to be deleted and deletes it from the products array
    /// </summary>
    /// <param name="Id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Delete(int Id)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (DataSource.ProductList[i].ID == Id)
            {
                for (int j = i; j < DataSource.Config.productIdx; j++)
                {
                    DataSource.ProductList[j] = DataSource.ProductList[j + 1];

                }
                DataSource.Config.productIdx--;
                flag = false;
            }
        }
        if (flag)
            throw new Exception("this Product does not exist");
    }


    /// <summary>
    /// read function copies all products exists in the products array and returns it
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Product[] Read()
    {
        Product[] ProductList = new Product[DataSource.Config.productIdx];
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            ProductList[i] = DataSource.ProductList[i];
        }
        return ProductList;
        throw new Exception();
    }


    /// <summary>
    /// read single function gets an id of the product requested and returns it (if it's exist) 
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Product ReadSingle(int Id)
    {
        bool flag = true;
        int i;
        for (i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (DataSource.ProductList[i].ID == Id)
            {
                flag = false;
                break;
            }
        }
        if (flag)
            throw new Exception("this Product does not exist");
        return DataSource.ProductList[i];
    }


    /// <summary>
    /// update function gets a product with updated details and put it in the array instead of the product exist with this id
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Update(Product obj)
    {
        bool flag = true;

        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (DataSource.ProductList[i].ID == obj.ID)
            {
                DataSource.ProductList[i] = obj;
                flag = false;
            }
        }
        if (flag)
            throw new Exception("this Product does not exist");
    }


    /// <summary>
    /// decreaseInStock function gets a product id an amount and updates this product to have amount less in stock
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    public static void decreaseInStock(int id, int amount)
    {

        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (DataSource.ProductList[i].ID == id)
            {
                DataSource.ProductList[i].InStock -= amount;
            }
        }
    }
}