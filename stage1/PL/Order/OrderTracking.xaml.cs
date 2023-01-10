using System;
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
namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        private IBl bl;
        private int orderID;
        public OrderTracking(IBl bl, int orderID)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.bl=bl;
            BO.OrderTracking ot = bl.Order.orderTracking(orderID);
            this.DataContext = ot;
            var a= new ObservableCollection<Tuple<DateTime, BO.eOrderStatus>>(ot.DateAndTrack);
            statusDetailes.ItemsSource = a;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e) => new Order(bl,orderID, false).Show();
    }
}
