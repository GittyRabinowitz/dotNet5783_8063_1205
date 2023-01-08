using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{

    //public class PlIdNotExist : Exception
    //{
    //    public PlIdNotExist(Exception inner) : base("ID does not exist", inner) { }
    //    public override string Message => "ID does not exist";
    //}

    //public class PlIdAlreadyExist : Exception
    //{
    //    public PlIdAlreadyExist(Exception inner) : base("ID already exist", inner) { }
    //    public override string Message => "ID already exist";
    //}

    //public class PlNoEntitiesFound : Exception
    //{
    //    public PlNoEntitiesFound(string message) : base(message) { }
    //    public override string Message => "No entities found";
    //}

    //public class PlNoEntitiesFoundInDal : Exception
    //{
    //    public PlNoEntitiesFoundInDal(Exception inner) : base("no entities found", inner)
    //    {

    //    }
    //    public override string Message => "No entities found";
    //}

    //public class PlProductExistInOrders : Exception
    //{
    //    public PlProductExistInOrders(string message) : base(message) { }
    //}

    public class PlInvalideData : Exception
    {
        public PlInvalideData(string messege) : base(messege) { }
    }

    //public class PlOutOfStockException : Exception
    //{
    //    public PlOutOfStockException(string messege) : base(messege) { }
    //}
    //public class PlNullValueException : Exception
    //{
    //    public override string Message =>
    //                    "null value exception";

    //}

    //public class PlUpdateException : Exception
    //{
    //    public PlUpdateException(string messege) : base(messege) { }
    //}

}
