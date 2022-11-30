using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BlIdNotExist : Exception
    {
        public BlIdNotExist(Exception inner) : base("ID does not exist", inner){}
        public override string Message => "ID does not exist";
    }

    public class BlIdAlreadyExist : Exception
    {
        public BlIdAlreadyExist(Exception inner) : base("ID already exist", inner){}
        public override string Message => "ID already exist";
    }

    public class BlNoEntitiesFound : Exception
    {
        public BlNoEntitiesFound(string message) : base(message)
        {

        }
        public override string Message => "No entities found";
    }

    public class BlNoEntitiesFoundInDal : Exception
    {
        public BlNoEntitiesFoundInDal(Exception inner) : base("no entities found", inner)
        {

        }
        public override string Message => "No entities found";
    }

    public class BlProductExistInOrders : Exception
    {
        public BlProductExistInOrders(string message) : base(message){}
    }

    public class BlInvalideData : Exception
    {
        public BlInvalideData(string messege) : base(messege){}
    }

    public class BlOutOfStockException : Exception
    {
        public BlOutOfStockException(string messege) : base(messege){}
    }
    public class BlNullValueException : Exception
    {
        public override string Message =>
                        "null value exception";

    }

    public class BlUpdateException : Exception
    {
        public BlUpdateException(string messege) : base(messege){}
    }

}





