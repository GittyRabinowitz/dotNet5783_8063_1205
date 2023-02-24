using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BlApi;
using BO;

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

        ObservableCollection<BO.ProductForList> productsCollection;
        Window lastWindow;
       // public Array enumValues { get; set; }


      //  public bool isUpdateAndDelete { get; set; }
        public ProductWindow(IBl bl, Window _lastWindow, int id = 0, bool isDynamic = true, BO.Cart cart = null,
            ObservableCollection<BO.ProductForList> _productsCollection = null)
        {
            try
            {
                InitializeComponent();
               // bool isUpdateAndDelete;
                this.bl = bl;
                this.lastWindow = _lastWindow;
                if (_productsCollection != null)
                    this.productsCollection = _productsCollection;
                if (cart != null)
                    this.cart = cart;
                CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
                if (id == 0)//add
                {
                    this.DataContext = product;
                    UpdateBtn.Visibility = Visibility.Hidden;
                    DeleteBtn.Visibility = Visibility.Hidden;

                    addToCartBtn.Visibility = Visibility.Hidden;
                   // isUpdateAndDelete = false;
                }
                else
                {
                    AddBtn.Visibility = Visibility.Hidden;
                    if (!isDynamic)//Display only
                    {
                        productItem = bl.Product.GetProductCustomer(id, cart);
                        this.DataContext = productItem;
                        UpdateBtn.Visibility = Visibility.Hidden;
                        DeleteBtn.Visibility = Visibility.Hidden;
                       // isUpdateAndDelete = false;
                        NameTxt.IsReadOnly = true;
                        PriceTxt.IsReadOnly = true;
                        CategoriesSelector.IsEnabled = false;
                        InStockTxt.IsReadOnly = true;
                        
                    }
                    else
                    {
                        //update
                       // isUpdateAndDelete = true;
                        product = bl.Product.GetProductManager(id);
                        this.DataContext = product;
                        addToCartBtn.Visibility = Visibility.Hidden;
                        
                    }
                }
            }
            catch (BlIdNotExist exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (product.Name == null | product.Price == 0 | product.Category == null | product.InStock == 0)
                    throw new PlInvalideData("invalid data");

                int id = bl.Product.Add(product);
                product.ID = id;
                productsCollection?.Add(Convert.convertProductToProductForList(product));
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
            finally
            {
                lastWindow.Show();
                this.Close();
            }
        }



        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (product.Name == null | product.Price == 0 | product.Category == null | product.InStock == 0)
                    throw new PlInvalideData("invalid data");
                bl.Product.Update(product);

                productsCollection?.Remove(productsCollection?.Where(x => x.ID == product?.ID)?.FirstOrDefault());
                productsCollection?.Add(Convert.convertProductToProductForList(product));

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
            finally
            {
                lastWindow.Show();
                this.Close();
            }
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.Delete(product.ID);
                productsCollection?.Remove(productsCollection?.Where(x => x.ID == product?.ID)?.FirstOrDefault());
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
            finally
            {
                lastWindow.Show();
                this.Close();
            }
        }


        private void addToCartBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cart = bl.Cart.Add(cart, productItem.ID);
                MessageBox.Show("successfully added to cart");
                lastWindow.Show();
                this.Close();
            }
            catch (BlIdNotExist exc)
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
