﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public class EntityNotFoundException : Exception
    {
        //public override string Message =>
        //                "EntityNotFound";
        public EntityNotFoundException(string message) :
                                base(message)
        {
        }

    }
    public class EntityAlreadyExistException : Exception
    {
        public EntityAlreadyExistException(string message) :
                                        base(message)
        {
        }

    }
}