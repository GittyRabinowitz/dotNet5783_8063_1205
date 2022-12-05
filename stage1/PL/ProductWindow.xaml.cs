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
        public ProductWindow(IBl bl, Object obj)
        {
            if (obj == null)
            {
                //add
            }
            else
            {
                //update
                //לשים את כל הנתונים של האוביקט ב textbox
                //שם של הכפתור נקודה ויזיביליתי...
            }



            this.bl = bl;
            InitializeComponent();
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product();
            product.Name = NameTxt.Text;
            product.Price = int.Parse(PriceTxt.Text);
            product.Category =(BO.eCategory)CategoriesSelector.SelectedItem;
            product.InStock = int.Parse(InStockTxt.Text);
            bl.Product.Add(product);
        }
    }
}
