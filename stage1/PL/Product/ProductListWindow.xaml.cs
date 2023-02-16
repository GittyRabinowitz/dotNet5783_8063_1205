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

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {

        private IBl bl;
        ObservableCollection<BO.ProductForList> productsCollection;
        ObservableCollection<BO.OrderForList> ordersCollection;
        Window lastWindow;
        public ProductListWindow(IBl bl, Window _lastWindow)
        {
            try
            {

                InitializeComponent();

                this.lastWindow = _lastWindow;

                this.bl = bl;
                AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));




                productsCollection = new ObservableCollection<BO.ProductForList>(bl.Product.GetProductList());
                ProductsListview.ItemsSource = productsCollection;



                ordersCollection = new ObservableCollection<OrderForList>(bl.Order.GetOrderList());
                OrderListview.ItemsSource = ordersCollection;

            }
            catch (BO.BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object SelectedItem = AttributeSelector.SelectedItem;
                ProductsListview.ItemsSource = bl.Product.GetProductByCategoty((BO.eCategory)SelectedItem);
            }
            catch (BO.BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(bl, _lastWindow: this, _productsCollection: productsCollection);
            productWindow.Show();
            this.Hide();
        }


        private void viewListProductDoubleClick(object sender, MouseButtonEventArgs e)
        {

            try
            {

                ProductWindow productWindow = new ProductWindow(bl, (ProductsListview.SelectedItem as BO.ProductForList).ID, _lastWindow: this, _productsCollection: productsCollection);
                productWindow.Show();
                this.Hide();
            }
            catch (BO.BlIdNotExist exc)
            {

                MessageBox.Show(exc.Message);
            }
        }

        private void viewListOrderDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Order.Order orderWindow = new Order.Order(bl, (OrderListview.SelectedItem as BO.OrderForList).ID, true, this, ordersCollection);
            orderWindow.Show();
            this.Hide();
        }
    }



}
