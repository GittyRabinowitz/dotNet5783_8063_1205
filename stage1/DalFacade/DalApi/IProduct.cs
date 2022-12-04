using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DO;
namespace DalApi
{
    public interface IProduct:ICrud<Product>
    {
        public void decreaseInStock(int id, int amount);
        public IEnumerable<Product> GetProductByCategory(eCategory category);
    }
}
