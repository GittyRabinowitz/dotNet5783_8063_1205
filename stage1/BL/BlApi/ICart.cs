﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface ICart
{
    public Cart Add(Cart c, int id);
    public Cart Update(Cart c, int id, int newAmount, ObservableCollection<BO.OrderItem> items);
    public void CartConfirmation(Cart c, string customerName, string customerEmail, string customerAddress);


}
