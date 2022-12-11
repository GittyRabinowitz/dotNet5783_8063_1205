
using Dal.DO;
using DalApi;
//using lin
namespace Dal.UseObjects;
/// <summary>
/// class for crud actions for a product
/// </summary>


internal class DalProduct : IProduct
{

    /// <summary>
    /// create function gets an object and insert it into the products array
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>the object's id</returns>
    /// <exception cref="NotImplementedException"></exception>
    public int Add(Product obj)
    {
        obj.ID = DataSource.Config.ProductID;
        DataSource.ProductList.Add(obj);
        return obj.ID;
    }


    /// <summary>
    /// delete function gets an id of the object requsted to be deleted and deletes it from the products array
    /// </summary>
    /// <param name="Id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Delete(int Id)
    {
        bool flag = true;
        for (int i = 0; i < DataSource.ProductList.Count(); i++)
        {
            if (DataSource.ProductList[i].ID == Id)
            {
                DataSource.ProductList.Remove(DataSource.ProductList[i]);
                flag = false;
            }
        }
        if (flag)
            throw new DalIdNotFoundException("this Product does not exist");
    }


    /// <summary>
    /// read function copies all products exists in the products array and returns it
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<Product> Get(Func<Product,bool> func=null)
    {
        //List<Product> ProductList = new List<Product>();
        //for (int i = 0; i < DataSource.ProductList.Count(); i++)
        //{
        //    ProductList.Add(DataSource.ProductList[i]);
        //}
        //return ProductList;

        return (func == null ? DataSource.ProductList : DataSource.ProductList.Where(func).ToList());
    }



    /// <summary>
    /// update function gets a product with updated details and put it in the array instead of the product exist with this id
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(Product obj)
    {
        bool flag = true;

        for (int i = 0; i < DataSource.ProductList.Count(); i++)
        {
            if (DataSource.ProductList[i].ID == obj.ID)
            {
                DataSource.ProductList[i] = obj;
                flag = false;
                break;
            }
        }
        if (flag)
            throw new DalIdNotFoundException("this Product does not exist");
    }


    /// <summary>
    /// decreaseInStock function gets a product id an amount and updates this product to have amount less in stock
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    public void decreaseInStock(int id, int amount)
    {

        for (int i = 0; i < DataSource.ProductList.Count(); i++)
        {
            if (DataSource.ProductList[i].ID == id)
            {
                Product p = DataSource.ProductList[i];
                p.InStock -= amount;
                DataSource.ProductList[i] = p;
                ;
            }
        }
    }


    public Product GetSingle(Func<Product, bool> func)
    {
        if(DataSource.ProductList.Where(func).ToList().Count()==0)
            throw new DalIdNotFoundException("this Product does not exist");
        return (DataSource.ProductList.Where(func).ToArray()[0]);
    }
}