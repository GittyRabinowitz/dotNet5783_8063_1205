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
namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductCatalog.xaml
    /// </summary>
    public partial class ProductCatalog : Window
    {
        private IBl bl;
        public ProductCatalog(IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            ProductsListview.ItemsSource = bl.Product.GetCatalog();

            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object SelectedItem = AttributeSelector.SelectedItem;
                ProductsListview.ItemsSource = bl.Product.GetCatalog((BO.eCategory)SelectedItem);
            }
            catch (BO.BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void viewListProductDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {

                ProductWindow productWindow = new ProductWindow(bl, (ProductsListview.SelectedItem as BO.ProductItem).ID, false);
                productWindow.Show();
            }
            catch (BO.BlIdNotExist exc)
            {

                MessageBox.Show(exc.Message);
            }
        }


        private void ViewCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}


