using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GroupProject.Search;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        /// <summary>
        /// Dictionary showing combo box display names to DB table names
        /// </summary>
        private IDictionary<string, string> FieldsDict { get; }
        
        /// <summary>
        /// Invoice info to be accessed from other windows
        /// </summary>
        public Invoice SelectedInvoice { get; private set; }
        
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

        /// <summary>
        /// Queries the database for invoices where the selected field equals the given value.
        /// Sets the DataGrid Context to show the results of the query.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Changes what input is shown based on the combobox selection.
        /// If it is the Invoice Date it will show a date input and otherwise a text input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// No invoice is selected and sets DialogResult to false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        /// <summary>
        /// Gets the invoice double clicked by the user from the DataGrid.
        /// Sets SelectedInvoice to the invoice to be used from other windows
        /// Sets DialogResult to true then closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RowDoubleClick_OnHandler(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = (DataGridRow)sender;
            SelectedInvoice = (Invoice)row.Item;
            DialogResult = true;
            Close();
        }
        
        /// <summary>
        /// Handles errors by showing the message and stack trace of the exception.
        /// Sets loading to false just in case the exception was thrown while data was loading.
        /// </summary>
        /// <param name="ex">The thrown exception</param>
        private void HandleError(Exception ex)
        {
            MessageBox.Show(ex.Message);
            MessageBox.Show(ex.StackTrace);
            Loading(false);
        }
    }
}