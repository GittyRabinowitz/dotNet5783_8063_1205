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
        public Order(IBl bl, int orderID)
        {

            InitializeComponent();
            this.bl = bl;
            BO.Order order = bl.Order.GetOrderDetails(orderID);
            this.DataContext = order;
        }
    }
}
