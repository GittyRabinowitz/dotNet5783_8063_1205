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
namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        IBl bl;
        BO.Cart cart;
        public CartWindow(IBl bl, BO.Cart cart)
        {
            InitializeComponent();
            this.bl = bl;
            this.cart = cart;
            this.DataContext = cart;
            ProductsListview.ItemsSource = cart.Items;
        }

        private void cartConfirmationBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.Cart.CartConfirmation(cart, cart.CustomerName, cart.CustomerEmail, cart.CustomerAddress);
        }
    }
}
