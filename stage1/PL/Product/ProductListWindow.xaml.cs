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
        public ObservableCollection<BO.ProductForList> productsCollection { get; set; }
        public ObservableCollection<BO.OrderForList> ordersCollection { get; set; }
        public Array enumValues { get; set; }
        Window lastWindow;



        public ProductListWindow(IBl bl, Window _lastWindow)
        {
            try
            {
                InitializeComponent();

                this.lastWindow = _lastWindow;
                this.bl = bl;
                enumValues = Enum.GetValues(typeof(BO.eCategory));

                productsCollection = new ObservableCollection<BO.ProductForList>(bl.Product.GetProductList());
                ordersCollection = new ObservableCollection<OrderForList>(bl.Order.GetOrderList());

                this.DataContext = this;
            }
            catch (BO.BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
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
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }


        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductWindow productWindow = new ProductWindow(bl, this, _productsCollection: productsCollection);
                productWindow.Show();
                this.Hide();
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }


        private void viewListProductDoubleClick(object sender, MouseButtonEventArgs e)
        {

            try
            {
                ProductWindow productWindow = new ProductWindow(bl, _lastWindow: this, (ProductsListview.SelectedItem as BO.ProductForList).ID, _productsCollection: productsCollection);
                productWindow.Show();
                this.Hide();
            }
            catch (BO.BlIdNotExist exc)
            {

                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }

        private void viewListOrderDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Order.Order orderWindow = new Order.Order(bl, (OrderListview.SelectedItem as BO.OrderForList).ID, true, this, ordersCollection);
            orderWindow.Show();
            this.Hide();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            lastWindow.Show();
            this.Close();
        }
    }
}
