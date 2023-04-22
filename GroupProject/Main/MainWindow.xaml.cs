using GroupProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using GroupProject.Main;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SearchWindow wndSearch;
        private ItemsWindow wndItem;
        private MainLogic _logic;

        private int InvoiceID { get; set; }
        
        private Invoice SelectedInvoice { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            _logic = new MainLogic();

        }

        private void SearchClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OpenSearchWindow();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}");
            }
        }

   
        private void GoToSearch(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenSearchWindow();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}");
            }
        }

        private void OpenSearchWindow()
        {
            try
            {
                wndSearch = new SearchWindow();
                wndSearch.ShowDialog();
                
                if (!(wndSearch.DialogResult ?? false))
                    return;
                // Get the invoice ID from the search window
                InvoiceID = wndSearch.SelectedInvoiceID;
                // Get the invoice from the database
                SelectedInvoice = _logic.GetInvoiceById(InvoiceID);
                // Set the invoice in the data grid
                InvoiceDataGrid.ItemsSource = null;
                InvoiceDataGrid.ItemsSource = new List<Invoice> { SelectedInvoice };
                
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}");
            }
        }

        private void OpenItems(object sender, RoutedEventArgs e)
        {
            try
            {
                wndItem = new ItemsWindow();
                wndItem.ShowDialog();
                if (!(wndItem.DialogResult ?? false))
                {
                    return;
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}");
            }
        }

        //After search window is closed, check property SelectedInvoiceId in the Search window to see if an invoice is selected. If so load invoice.

        //After Items window is closed, check property HasItemsBeenChanged in the Items window to see if any items were updated. If so re-load items in combo box.
    }
}
