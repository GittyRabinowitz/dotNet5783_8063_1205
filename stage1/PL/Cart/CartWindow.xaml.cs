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
using BO;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        IBl bl;
        public BO.Cart cart { get; set; }
        public ObservableCollection<BO.OrderItem> items { get; set; }
        Window lastWindow;



        public CartWindow(IBl bl, BO.Cart cart, Window _lastWindow)
        {
            InitializeComponent();

            this.lastWindow = _lastWindow;
            this.bl = bl;
            this.cart = cart;
            items = new ObservableCollection<BO.OrderItem>(cart.Items);
            this.DataContext = this;
        }


        private void cartConfirmationBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.CartConfirmation(cart, cart.CustomerName, cart.CustomerEmail, cart.CustomerAddress);
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
            finally
            {
                lastWindow.Show();
                this.Close();
            }
        }


        private void decreaseQuantityBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem itemToUpdate = (BO.OrderItem)((Button)sender).DataContext;
                cart = bl.Cart.Update(cart, itemToUpdate.ProductID, itemToUpdate.Amount - 1, items);
                totalPriceTxt.Text = cart.TotalPrice.ToString();
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }


        private void IncreaseQuantityBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem itemToUpdate = (BO.OrderItem)((Button)sender).DataContext;
                cart = bl.Cart.Update(cart, itemToUpdate.ProductID, itemToUpdate.Amount + 1, items);
                totalPriceTxt.Text = cart.TotalPrice.ToString();
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }


        private void EmptyButton_Click(object sender, RoutedEventArgs e)
        {
            int numOfItems = cart.Items.Count;
            for (int i = 0; i < numOfItems; i++)
            {
                cart.Items.Remove(cart.Items[0]);
                items.RemoveAt(0);
            }
            cart.TotalPrice = 0;
            totalPriceTxt.Text = cart.TotalPrice.ToString();
        }


        private void deleteOrderItemBtn(object sender, RoutedEventArgs e)
        {
            BO.OrderItem itemToRemove = (BO.OrderItem)((Button)sender).DataContext;
            cart.TotalPrice = cart.TotalPrice - itemToRemove.TotalPrice;
            cart?.Items?.Remove(itemToRemove);
            items.Remove(itemToRemove);
            totalPriceTxt.Text = cart.TotalPrice.ToString();
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            lastWindow.Show();
            this.Close();
        }
    }
}
