﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface IProduct
{

    public IEnumerable<ProductForList> GetProductList();//manager customer
    public IEnumerable<ProductForList> GetProductByCategoty(BO.eCategory category);
    public IEnumerable<ProductItem> GetCatalog(eCategory? category=null);//catalog
    public Product GetProductManager(int id);
    public ProductItem GetProductCustomer(int id, BO.Cart cart);
    public int Add(Product p);//manager
    public void Delete(int id);//manager
    public void Update(Product p);//manager


}

