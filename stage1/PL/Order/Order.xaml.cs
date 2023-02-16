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
namespace PL.Order
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        private IBl bl;
        private int currentOrderId;
        ObservableCollection<BO.OrderForList> ordersCollection;
        Window lastWindow;
        public Order(IBl bl, int orderID, bool isDynamic, Window _lastWindow, ObservableCollection<BO.OrderForList> _ordersCollection = null)
        {

            this.lastWindow = _lastWindow;

            if (_ordersCollection != null)
            {
                this.ordersCollection = _ordersCollection;
            }
            BO.Order order = bl.Order.GetOrderDetails(orderID);
            this.currentOrderId = orderID;
            this.DataContext = order;

            InitializeComponent();

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
                updateDeliveryBtn.Visibility = Visibility.Hidden;
                updateShippingBtn.Visibility = Visibility.Hidden;
            }
        }

        private void updateShippingBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order order = bl.Order.updateShippedOrder(currentOrderId);


                ordersCollection?.Remove(ordersCollection?.Where(x => x.ID == currentOrderId)?.FirstOrDefault());

                ordersCollection?.Add(Convert.convertOrderToOrderForList(order));


                this.DataContext=order;
            }
            catch (BO.BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show(exc.Message + " " + exc.InnerException.Message);
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show(exc.Message + " " + exc.InnerException.Message);
            }
            catch (BO.BlUpdateException exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void updateDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               BO.Order order= bl.Order.updateDeliveryedOrder(currentOrderId);

                ordersCollection?.Remove(ordersCollection?.Where(x => x.ID == currentOrderId)?.FirstOrDefault());

                ordersCollection?.Add(Convert.convertOrderToOrderForList(order));

                this.DataContext = order;
            }
            catch (BO.BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show(exc.Message + " " + exc.InnerException.Message);
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show(exc.Message + " " + exc.InnerException.Message);
            }
            catch (BO.BlUpdateException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
