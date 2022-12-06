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
                UpdateBtn.Visibility = Visibility.Hidden;
                CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
                //add
            }
            else
            {
                AddBtn.Visibility = Visibility.Hidden;
                this.productId = obj.ID;
                //update
                NameTxt.Text = obj.Name;
                PriceTxt.Text = obj.Price.ToString();
                CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
                CategoriesSelector.SelectedItem = obj.Category;
                InStockTxt.Text = obj.InStock.ToString();
            }
        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product();
            product.Name = NameTxt.Text;
            product.Price = int.Parse(PriceTxt.Text);
            product.Category = (BO.eCategory)CategoriesSelector.SelectedItem;
            product.InStock = int.Parse(InStockTxt.Text);
            bl.Product.Add(product);
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product();
            product.ID = productId;
            product.Name = NameTxt.Text;
            product.Price = int.Parse(PriceTxt.Text);
            product.Category = (BO.eCategory)CategoriesSelector.SelectedItem;
            product.InStock = int.Parse(InStockTxt.Text);
            bl.Product.Update(product);
        }
    }
}
