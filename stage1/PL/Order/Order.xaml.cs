using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        Window lastWindow;
        public bool isDynamic { get; set; }
        public BO.Order order { get; set; }
        ObservableCollection<BO.OrderForList> ordersCollection;



        public Order(IBl bl, int orderID, bool isDynamic, Window _lastWindow, ObservableCollection<BO.OrderForList> _ordersCollection = null)
        {
            try
            {
                InitializeComponent();
                this.isDynamic = isDynamic;
                this.lastWindow = _lastWindow;
                if (_ordersCollection != null)
                    this.ordersCollection = _ordersCollection;
                this.order = bl.Order.GetOrderDetails(orderID);
                this.currentOrderId = orderID;
                this.DataContext = this;
                this.bl = bl;
            }
            catch (BO.BlIdNotExist exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
            catch (BO.BlNoEntitiesFoundInDal exc)
            {
                MessageBox.Show("inner exception: " + exc.InnerException.Message + "\n" + "exception: " + exc.Message);
            }
        }


        private void updateShippingBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order order = bl.Order.updateShippedOrder(currentOrderId);

                ordersCollection?.Remove(ordersCollection?.Where(x => x.ID == currentOrderId)?.FirstOrDefault());

                ordersCollection?.Add(Convert.convertOrderToOrderForList(order));
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
            finally
            {
                lastWindow.Show();
                this.Close();
            }
        }


        private void updateDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order order = bl.Order.updateDeliveryedOrder(currentOrderId);

                ordersCollection?.Remove(ordersCollection?.Where(x => x.ID == currentOrderId)?.FirstOrDefault());

                ordersCollection?.Add(Convert.convertOrderToOrderForList(order));

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
            finally
            {
                lastWindow.Show();
                this.Close();
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            lastWindow.Show();
            this.Close();
        }
    }
}
