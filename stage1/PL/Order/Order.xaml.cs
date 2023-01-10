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
namespace PL.Order
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        private IBl bl;
        public Order(IBl bl, int orderID, bool isDynamic)
        {
            BO.Order order = bl.Order.GetOrderDetails(orderID);
            this.DataContext = order;
            
            InitializeComponent();


            //לעשות כשהפרמטר הבוליאני true 
            //שאפשר לעדכן שילוח או הגעת משלוח


            this.bl = bl;

            ProductsListview.ItemsSource = order.Items;
            if (!isDynamic)
            {
                nameTxt.IsReadOnly = true;
                emailTxt.IsReadOnly = true;
                addressTxt.IsReadOnly = true;
                orderDateTxt.IsReadOnly = true;
                shipDateTxt.IsReadOnly = true;
                deliveryTxt.IsReadOnly = true;
                statusTxt.IsReadOnly = true;
                totalPriceTxt.IsReadOnly = true;
            }
        }
    }
}
