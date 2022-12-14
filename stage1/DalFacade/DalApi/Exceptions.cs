using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class DalIdNotFoundException : Exception
    {
        public DalIdNotFoundException(string message) : base(message) {}

    }
    public class DalIdAlreadyExistException : Exception
    {
        public DalIdAlreadyExistException(string message) :base(message) {}

    }
    public class DalNoEntitiesFound : Exception
    {
        public DalNoEntitiesFound(string message) : base(message) { }

    }
    
}
