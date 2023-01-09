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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBl bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEntry_Click(object sender, RoutedEventArgs e) => new ProductListWindow(bl).Show();

        private void OrderTracking(object sender, RoutedEventArgs e) => new Order.OrderTracking(bl, int.Parse(orderIDTxt.Text)).Show();

        private void NewOrder(object sender, RoutedEventArgs e)=>new Product.ProductCatalog(bl).Show();
    }
}
