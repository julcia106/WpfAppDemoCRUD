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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppDemoCRUD.Data;

namespace WpfAppDemoCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ProductDBcontext dbContext;
        Product NewProduct = new Product();

        public MainWindow(ProductDBcontext dbContext)
        {
            this.dbContext = dbContext;
            InitializeComponent();
            GetProducts();

            AddNewProductGrid.DataContext = NewProduct;
        }

        // Read operation, assigning records to ItemsSource
        private void GetProducts()
        {
           ProductDG.ItemsSource = dbContext.Products.ToList();
        }

        // Add new product to dbContext
        private void AddProduct(object s, RoutedEventArgs e)
        {
            dbContext.Products.Add(NewProduct);
            dbContext.SaveChanges();
            GetProducts();
            NewProduct = new Product();
            AddNewProductGrid.DataContext = NewProduct;
        }

        Product selectedProduct = new Product();
        private void UpdateProductForEdit(object s, RoutedEventArgs e)
        {
            selectedProduct = (s as FrameworkElement).DataContext as Product;
            UpdateProductGrid.DataContext = selectedProduct;
        }

        private void UpdateProduct(object s, RoutedEventArgs e)
        {
            dbContext.Update(selectedProduct);
            dbContext.SaveChanges();
            GetProducts();
        }

        private void DeleteProduct(object s, RoutedEventArgs e)
        {
            var productToBeDeleted = (s as FrameworkElement).DataContext as Product;
            dbContext.Products.Remove(productToBeDeleted);
            dbContext.SaveChanges();
            GetProducts();
        }
    }
}
