using System;
using System.Collections.Generic;
using System.Data;
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
using GroupProject.Search;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private IDictionary<string, string> FieldsDict { get; }

        public SearchWindow()
        {
            InitializeComponent();
            SearchIconImg.Source = new BitmapImage(new Uri(@"../../../Assets/Images/search-sm.png", UriKind.Relative));
            FieldsDict = new Dictionary<string, string>
            {
                { "ID", "ID" },
                { "Invoice #", "InvoiceNumber" },
                { "Customer", "CustomerName" },
                { "Date Invoiced", "InvoiceDate" }
            };
            SearchFieldComboBox.ItemsSource = FieldsDict.Keys;
        }

        private async void SearchBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Loading(true);
                IList<Invoice> invoices = new List<Invoice>();

                await foreach (Invoice i in SearchSql.SearchInvoices(
                                   FieldsDict[SearchFieldComboBox.SelectedValue.ToString()],
                                   SearchFieldComboBox.SelectedValue.ToString() == "Date Invoiced"
                                       ? InvDatePicker.SelectedDate.Value.ToShortDateString()
                                       : SearchTextBox.Text))
                {
                    invoices.Add(i);
                }

                SearchResultsDataGrid.DataContext = invoices;
                Loading(false);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a visual queue for the user to show that the search is searching.
        /// </summary>
        /// <param name="isLoading"></param>
        private void Loading(bool isLoading)
        {
            Opacity = isLoading ? 0.8 : 1;
            SearchBtn.IsEnabled = !isLoading;
            SearchFieldComboBox.IsEnabled = !isLoading;
            SearchTextBox.IsEnabled = !isLoading;
        }

        private void HandleError(Exception ex)
        {
            MessageBox.Show(ex.Message);
            MessageBox.Show(ex.StackTrace);
            Loading(false);
        }

        private void SearchFieldComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchFieldComboBox.SelectedValue.ToString() == "Date Invoiced")
            {
                SearchTextBox.Visibility = Visibility.Collapsed;
                InvDatePicker.Visibility = Visibility.Visible;
            }
            else
            {
                InvDatePicker.Visibility = Visibility.Collapsed;
                SearchTextBox.Visibility = Visibility.Visible;
            }
        }

        private void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}