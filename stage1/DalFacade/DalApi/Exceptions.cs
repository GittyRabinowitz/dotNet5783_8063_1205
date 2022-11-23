using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class DalEntityNotFoundException : Exception
    {
        //public override string Message =>
        //                "EntityNotFound";
        public DalEntityNotFoundException(string message) :
                                base(message)
        {
        }

    }
    public class DalEntityAlreadyExistException : Exception
    {
        public DalEntityAlreadyExistException(string message) :
                                        base(message)
        {
        }

    }
}
