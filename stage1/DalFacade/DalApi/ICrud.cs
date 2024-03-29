﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>
    {
        public int Add(T obj);
        public void Delete(int id);
        public void Update(T obj);
        public T GetSingle(Func<T, bool> func);
        public IEnumerable<T> Get(Func<T, bool> func=null);
    }
}
