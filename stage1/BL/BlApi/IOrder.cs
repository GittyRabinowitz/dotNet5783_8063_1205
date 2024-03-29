﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetOrderList();//manager
    public Order GetOrderDetails(int id);//manager and customer
    public Order updateShippedOrder(int id);//manager
    public Order updateDeliveryedOrder(int id);//manager
    public OrderTracking orderTracking(int orderId);
    public int? ChooseOrder();

}
