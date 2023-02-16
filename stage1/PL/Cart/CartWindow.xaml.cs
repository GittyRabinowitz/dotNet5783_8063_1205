using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<BO.OrderItem> items = new ObservableCollection<BO.OrderItem>();
        Window lastWindow;
        public CartWindow(IBl bl, BO.Cart cart, Window _lastWindow)

        {
            InitializeComponent();

            this.lastWindow = _lastWindow;

            this.bl = bl;
            this.cart = cart;
            this.DataContext = cart;

            items = new ObservableCollection<BO.OrderItem>(cart.Items);
            ProductsListview.ItemsSource = items;
        }

        private void cartConfirmationBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.Cart.CartConfirmation(cart, cart.CustomerName, cart.CustomerEmail, cart.CustomerAddress);
        }
        private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem itemToUpdate = (BO.OrderItem)((Button)sender).DataContext;
            cart = bl.Cart.Update(cart, itemToUpdate.ProductID, itemToUpdate.Amount - 1);

            this.DataContext = cart;
            items = new ObservableCollection<BO.OrderItem>(cart.Items);
            ProductsListview.ItemsSource = items;
        }

        private void addProductBtn_Click(object sender, RoutedEventArgs e)
        {

            BO.OrderItem itemToUpdate = (BO.OrderItem)((Button)sender).DataContext;
            cart = bl.Cart.Update(cart, itemToUpdate.ProductID, itemToUpdate.Amount + 1);

            this.DataContext = cart;
            items = new ObservableCollection<BO.OrderItem>(cart.Items);
            ProductsListview.ItemsSource = items;

        }
        private void EmptyButton_Click(object sender, RoutedEventArgs e)
        {

            int numOfItems = cart.Items.Count;
            for (int i = 0; i < numOfItems; i++)
            {
                cart.Items.Remove(cart.Items[0]);
            }

            //cart?.Items?.ForEach(item => { cart.Items.Remove(item); });
            //לטפל ב פור הזה
            cart.TotalPrice = 0;
            this.DataContext = cart;
            ProductsListview.ItemsSource = null;
        }

        private void updateAmount_Button_Click(object sender, RoutedEventArgs e)
        {

            //להוסיף try and catch
            BO.OrderItem itemToUpdate = (BO.OrderItem)((Button)sender).DataContext;
            cart = bl.Cart.Update(cart, itemToUpdate.ProductID, itemToUpdate.Amount);

            this.DataContext = cart;
            items = new ObservableCollection<BO.OrderItem>(cart.Items);
            ProductsListview.ItemsSource = items;

        }

        private void deleteOrderItemBtn(object sender, RoutedEventArgs e)
        {
            BO.OrderItem itemToRemove = (BO.OrderItem)((Button)sender).DataContext;

            cart.TotalPrice -= itemToRemove.TotalPrice;
            cart?.Items?.Remove(itemToRemove);

            this.DataContext = cart;
            items = new ObservableCollection<BO.OrderItem>(cart.Items);
            ProductsListview.ItemsSource = items;

        }
    }
}
