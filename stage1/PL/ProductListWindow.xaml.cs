﻿using System;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {

         private IBl bl = new BlImplementation.Bl();
        public ProductListWindow()
        {
            InitializeComponent();
             ProductsListview.ItemsSource = bl.Product.GetProductList();


            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object SelectedItem = AttributeSelector.SelectedItem;
            IEnumerable<BO.ProductForList> lst = bl.Product.GetProductByCategoty(SelectedItem);
             ProductsListview.ItemsSource = lst;

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }



}
