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
            lastWindow.Show();
            this.Close();
        }


        private void decreaseQuantityBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem itemToUpdate = (BO.OrderItem)((Button)sender).DataContext;
            cart = bl.Cart.Update(cart, itemToUpdate.ProductID, itemToUpdate.Amount - 1, items);

            this.DataContext = cart;



          //  items = new ObservableCollection<BO.OrderItem>(cart.Items);
         //   ProductsListview.ItemsSource = items;
        }


        private void IncreaseQuantityBtn_Click(object sender, RoutedEventArgs e)
        {

            BO.OrderItem itemToUpdate = (BO.OrderItem)((Button)sender).DataContext;
            cart = bl.Cart.Update(cart, itemToUpdate.ProductID, itemToUpdate.Amount + 1, items);

            this.DataContext = cart;
           // items = new ObservableCollection<BO.OrderItem>(cart.Items);
           // ProductsListview.ItemsSource = items;
       
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
         //   this.DataContext = cart;
          
        }

        private void deleteOrderItemBtn(object sender, RoutedEventArgs e)
        {
            BO.OrderItem itemToRemove = (BO.OrderItem)((Button)sender).DataContext;

            cart.TotalPrice -= itemToRemove.TotalPrice;
            cart?.Items?.Remove(itemToRemove);

         //   this.DataContext = cart;
            items.Remove(itemToRemove);
         
          

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            lastWindow.Show();
            this.Close();
        }
    }
}
