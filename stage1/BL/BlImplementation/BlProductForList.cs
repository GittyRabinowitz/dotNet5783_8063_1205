﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;
using Dal;

namespace BlImplementation
{
    internal class BlProductForList:IProductForList
    {
        private IDal Dal = new DalList();
    }
}
