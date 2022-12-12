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
        private int productId;
        public ProductWindow(IBl bl, BO.Product? obj = null)
        {

            this.bl = bl;
            InitializeComponent();
            if (obj == null)
            {
                //add
                UpdateBtn.Visibility = Visibility.Hidden;
                DeleteBtn.Visibility = Visibility.Hidden;
                CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
                NameTxt.Text = "";
                PriceTxt.Text = "";
                InStockTxt.Text = "";
            }
            else
            {
                //update
                AddBtn.Visibility = Visibility.Hidden;
                this.productId = obj.ID;
                NameTxt.Text = obj.Name;
                PriceTxt.Text = obj.Price.ToString();
                CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
                CategoriesSelector.SelectedItem = obj.Category;
                InStockTxt.Text = obj.InStock.ToString();
            }
        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Product product = new BO.Product();

                if (NameTxt.Text == "")
                    throw new PlInvalideData("invalid data");
                product.Name = NameTxt.Text;

                int tmp;
                if (!(int.TryParse(PriceTxt.Text, out tmp)))
                    throw new PlInvalideData("invalid data");
                product.Price = int.Parse(PriceTxt.Text);

                if (CategoriesSelector.SelectedItem == null)
                    throw new PlInvalideData("invalid data");
                product.Category = (BO.eCategory)CategoriesSelector.SelectedItem;

                if (!(int.TryParse(InStockTxt.Text, out tmp)))
                    throw new PlInvalideData("invalid data");
                product.InStock = int.Parse(InStockTxt.Text);


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

                BO.Product product = new BO.Product();
                product.ID = productId;


                if (NameTxt.Text == "")
                    throw new PlInvalideData("invalid data");
                product.Name = NameTxt.Text;

                int tmp;
                if (!(int.TryParse(PriceTxt.Text, out tmp)))
                    throw new PlInvalideData("invalid data");
                product.Price = int.Parse(PriceTxt.Text);

                if (CategoriesSelector.SelectedItem == null)
                    throw new PlInvalideData("invalid data");
                product.Category = (BO.eCategory)CategoriesSelector.SelectedItem;

                if (!(int.TryParse(InStockTxt.Text, out tmp)))
                    throw new PlInvalideData("invalid data");
                product.InStock = int.Parse(InStockTxt.Text);


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
                bl.Product.Delete(productId);
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
    }

}
