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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductCatalog.xaml
    /// </summary>
    public partial class ProductCatalog : Window
    {
        private IBl bl;
        private BO.Cart cart;
        Window lastWindow;


        public ObservableCollection<BO.ProductItem> productItemsCollection { get; set; }
        public Array enumValues { get; set; }

        public ProductCatalog(IBl bl, BO.Cart cart, Window _lastWindow)
        {
            try
            {
                InitializeComponent();
                this.lastWindow = _lastWindow;
                this.bl = bl;
                this.cart = cart;
                this.productItemsCollection = new ObservableCollection<ProductItem>(bl.Product.GetCatalog());
                this.DataContext = this;
                enumValues = Enum.GetValues(typeof(BO.eCategory));
            }
            catch (BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }



        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object SelectedItem = AttributeSelector.SelectedItem;  
                productItemsCollection.Clear();
                foreach (var item in bl.Product.GetCatalog((BO.eCategory)SelectedItem))
                {
                    productItemsCollection.Add(item);
                }
            }
            catch (BO.BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }



        private void viewListProductDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ProductWindow productWindow = new ProductWindow(bl, this, (ProductsListview.SelectedItem as BO.ProductItem).ID, false, cart);
                productWindow.Show();
                this.Hide();
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }


        private void ViewCart_Click(object sender, RoutedEventArgs e)
        {
            Cart.CartWindow cartWindow = new Cart.CartWindow(bl, cart, this);
            cartWindow.Show();
            this.Hide();
        }



        private void GroupByCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productItemsCollection.Clear();

                var products = bl.Product.GetCatalog();
                var groupedProducts = from item in products group item by item.Category into q orderby q.Count() select q;

                groupedProducts.ToList().ForEach(group =>
                {
                    group.ToList().ForEach(item =>
                    {
                        productItemsCollection.Add(item);
                    });
                });
            }
            catch (BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            lastWindow.Show();
            this.Close();
        }
    }
}


