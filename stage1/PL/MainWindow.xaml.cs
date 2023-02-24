using System.Collections.Generic;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BlApi.IBl bl = BlApi.Factory.Get();
        BO.Cart cart = new BO.Cart();

        public MainWindow()
        {
            InitializeComponent();
            cart.Items = new List<BO.OrderItem?>();
        }


        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow productListWindow = new ProductListWindow(bl, this);
            productListWindow.Show();
            this.Hide();
        }


        private void BtnOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            Order.OrderTracking ot = new Order.OrderTracking(bl, int.Parse(orderIDTxt.Text), this);
            ot.Show();
            
            this.Hide();
        }


        private void BtnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            Product.ProductCatalog productCatalog = new Product.ProductCatalog(bl, cart, this);
            productCatalog.Show();
            this.Hide();
        }

        private void SimulatorButton_Click(object sender, RoutedEventArgs e)
        {
            new SimulatorWindow(bl).Show();
            
        }
    }
}
