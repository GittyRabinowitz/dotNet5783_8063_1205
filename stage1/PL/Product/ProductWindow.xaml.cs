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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl;

        BO.Product product = new BO.Product();
        BO.ProductItem productItem = new BO.ProductItem();
        BO.Cart cart = new BO.Cart();
        public ProductWindow(IBl bl, int id = 0, bool isDynamic = true, BO.Cart cart = null)
        {
            if (cart != null) { this.cart = cart; }
            this.bl = bl;
            InitializeComponent();
            if (id == 0)
            {
                //add
                this.DataContext = product;
                UpdateBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
                CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
                AmountLbl.Visibility = Visibility.Hidden;
                AmountTxt.Visibility = Visibility.Hidden;
                addToCartBtn.Visibility = Visibility.Hidden;

            }
            else
            {
                AddBtn.Visibility = Visibility.Hidden;
                CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
                if (!isDynamic)
                {

                    //תצוגה בלבד

                    productItem = bl.Product.GetProductCustomer(id, cart);
                    this.DataContext = productItem;
                    UpdateBtn.Visibility = Visibility.Hidden;
                    DeleteBtn.Visibility = Visibility.Hidden;

                    NameTxt.IsReadOnly = true;
                    PriceTxt.IsReadOnly = true;
                    CategoriesSelector.IsEnabled = false;
                    InStockTxt.IsReadOnly = true;
                    AmountTxt.IsReadOnly = true;

                }
                else
                {
                    //update
                    product = bl.Product.GetProductManager(id);
                    this.DataContext = product;
                    AmountLbl.Visibility = Visibility.Hidden;
                    AmountTxt.Visibility = Visibility.Hidden;
                    addToCartBtn.Visibility = Visibility.Hidden;
                }


            }
        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (product.Name == null | product.Price == 0 | product.Category == null | product.InStock == 0)
                    throw new PlInvalideData("invalid data");

                bl.Product.Add(product);
                MessageBox.Show("the product was added successfully!!!");
            }
            catch (BO.BlInvalideData exc)
            {

                MessageBox.Show(exc.Message);
            }
            catch (PlInvalideData exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (product.Name == null | product.Price == 0 | product.Category == null | product.InStock == 0)
                    throw new PlInvalideData("invalid data");
                bl.Product.Update(product);
                MessageBox.Show("the product was updated successfully!!!");
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show(exc.Message + " " + exc.InnerException.Message);
            }
            catch (PlInvalideData exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.Delete(product.ID);
                MessageBox.Show("the product was deleted successfully!!!");
            }
            catch (BO.BlProductExistInOrders exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show(exc.Message + " " + exc.InnerException.Message);
            }
        }

        private void addToCartBtn_Click(object sender, RoutedEventArgs e)
        {
            cart = bl.Cart.Add(cart, productItem.ID);
            MessageBox.Show("successfully added to cart");
        }
    }

}
