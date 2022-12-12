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
namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {

        private IBl bl;
        public ProductListWindow(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            ProductsListview.ItemsSource = bl.Product.GetProductList();

            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object SelectedItem = AttributeSelector.SelectedItem;
            ProductsListview.ItemsSource  = bl.Product.GetProductByCategoty((BO.eCategory)SelectedItem);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(bl);
            productWindow.Show();
        }


        private void viewListProductDoubleClick(object sender, MouseButtonEventArgs e)
        {


            BO.Product product = bl.Product.GetProductManager((ProductsListview.SelectedItem as BO.ProductForList).ID);
            ProductWindow productWindow = new ProductWindow(bl, product);
            productWindow.Show();

        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }



}
