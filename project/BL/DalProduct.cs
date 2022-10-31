
using Dal.DO;
namespace Dal.UseObjects;

public  class DalProduct
{
    public static int Create(Product obj)
    {
        if(DataSource.Config.productIdx>= DataSource.numOfProducts)
        {
            throw new NotImplementedException("There is no space available for your product");

        }
        for (int i = 0; i<DataSource.Config.productIdx; i++)
        {
            if (DataSource.ProductList[i].ID==obj.ID)
            {
                throw new NotImplementedException("this product already exist");

            }
        }
         DataSource.ProductList[DataSource.Config.productIdx++] = obj;
        return obj.ID;
    }

    public static void Delete(int Id)
    {
        bool flag = true;
        for (int i = 0; i<DataSource.Config.productIdx; i++)
        {
            if (DataSource.ProductList[i].ID==Id)
            {
                for (int j = i; j<DataSource.Config.productIdx; j++)
                {
                    DataSource.ProductList[j]=DataSource.ProductList[j+1];
                    
                }
                DataSource.Config.productIdx--;
                flag=false;
            }
        }
        if(flag)
            throw new NotImplementedException("this product does not exist");
    }

    public static Product[] Read()
    {
        Product[] ProductList = new Product[DataSource.Config.productIdx];
        for (int i = 0; i<DataSource.Config.productIdx; i++)
        {
            ProductList[i]=DataSource.ProductList[i];
        }
        return ProductList;
        throw new NotImplementedException();
    }

    public static Product ReadSingle(int Id)
    {
        bool flag = true;
        int i;
        for(i=0; i<DataSource.Config.productIdx; i++)
        {
            if (DataSource.ProductList[i].ID==Id)
            {
                flag=false;
                break;
            }
        }
        if(flag)
            throw new NotImplementedException("this product does not exist");
        return DataSource.ProductList[i];
    }

    public static void Update(Product obj)
    {
        bool flag = true;

        for (int i = 0; i<DataSource.Config.productIdx; i++)
        {
            if (DataSource.ProductList[i].ID==obj.ID)
            {
                DataSource.ProductList[i]=obj;
                flag=false;
            }
        }
        if (flag)
            throw new NotImplementedException("this product does not exist");
    }
}

