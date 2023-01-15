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

        private void EmptyButton_Click(object sender, RoutedEventArgs e)
        {
            cart?.Items?.ForEach(item => { cart.Items.Remove(item); });
            cart.TotalPrice = 0;

        }

        private void updateAmount_Button_Click(object sender, RoutedEventArgs e)
        {

            //להוסיף try and catch
            BO.OrderItem itemToUpdate = (BO.OrderItem)((Button)sender).DataContext;
            cart = bl.Cart.Update(cart, itemToUpdate.ID, itemToUpdate.Amount);
        }

        private void deleteOrderItemBtn(object sender, RoutedEventArgs e)
        {
            BO.OrderItem itemToRemove = (BO.OrderItem)((Button)sender).DataContext;

            cart?.Items?.Remove(itemToRemove);

        }
    }
}
