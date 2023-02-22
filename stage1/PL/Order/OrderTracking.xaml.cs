﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using System.Collections.ObjectModel;
using BO;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        private IBl bl;
        private int orderID;
        Window lastWindow;



        public OrderTracking(IBl bl, int orderID, Window _lastWindow)
        {
            try
            {
                InitializeComponent();
                this.lastWindow = _lastWindow;
                this.orderID = orderID;
                this.bl = bl;
                BO.OrderTracking ot = bl.Order.orderTracking(orderID);
                this.DataContext = ot;
                var a = new ObservableCollection<Tuple<DateTime, BO.eOrderStatus>>(ot.DateAndTrack);
                statusDetailes.ItemsSource = a;
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);

            }
        }


        private void OrderDetialsButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order(bl, orderID, false, this);
            order.Show();
            this.Hide();
        }



        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            lastWindow.Show();
            this.Close();
        }
    }
}