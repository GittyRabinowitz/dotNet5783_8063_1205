using System;
using System.Windows;
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


        public ObservableCollection<Tuple<DateTime, BO.eOrderStatus>> statusDetailesTuple { get; set; }
        public eOrderStatus status { get; set; }
        public OrderTracking(IBl bl, int orderID, Window _lastWindow)
        {
            try
            {
                InitializeComponent();
                this.lastWindow = _lastWindow;
                this.orderID = orderID;
                this.bl = bl;

                BO.OrderTracking ot = bl.Order.orderTracking(orderID);
                statusDetailesTuple = new ObservableCollection<Tuple<DateTime, BO.eOrderStatus>>(ot.DateAndTrack);
                status = (eOrderStatus)ot.Status;
                this.DataContext = this;
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
