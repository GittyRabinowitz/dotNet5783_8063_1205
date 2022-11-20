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
    internal class BlCart:ICart
    {
        private IDal Dal = new DalList();

        public Cart Add(Cart c, int id)
        {
            throw new NotImplementedException();
        }

        public void CartConfirmation(Cart c, string customerName, string customerEmail, string customerAddress)
        {
            throw new NotImplementedException();
        }

        public Cart Update(Cart c, int id, int newAmount)
        {
            throw new NotImplementedException();
        }
    }
}
