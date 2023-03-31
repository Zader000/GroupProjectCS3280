using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GroupProject.Search;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// Expects the user to search for and select an invoice by single clicking a row and clicking the select button
    /// or double clicking a row. The selected invoice is stored in the SelectedInvoice to be accessed by the other windows.  
    /// </summary>
    public partial class SearchWindow : Window
    {
        /// <summary>
        /// Invoice info to be accessed from other windows
        /// </summary>
        public Invoice SelectedInvoice { get; private set; }
        
        /// <summary>
        /// Dictionary showing combo box display names to DB table names
        /// </summary>
        private IDictionary<string, string> FieldsDict { get; }

        /// <summary>
        /// All invoices from the database
        /// </summary>
        private IList<Invoice> AllInvoices { get; }
        
        public SearchWindow()
        {
            InitializeComponent();

            Loading(true);
            AllInvoices = SearchLogic.GetInvoices(null, null, null);
            InvoiceNumberCB.ItemsSource = from inv in AllInvoices select inv.InvoiceNumber.ToString();
            InvoiceAmountCB.ItemsSource = (from inv in AllInvoices select inv.InvoiceAmount.ToString())
                .Distinct().OrderBy(double.Parse);
            SearchResultsDataGrid.DataContext = AllInvoices;
            Loading(false);
        }

        /// <summary>
        /// Gets the info from the user interface and searches the database. Then populates the DataGrid with the Info
        /// </summary>
        private async void Search()
        {
            try
            {
                Loading(true);
                SearchResultsDataGrid.DataContext = SearchLogic.GetInvoices(
                    InvoiceNumberCB.SelectedIndex == -1 ? null : InvoiceNumberCB.SelectedValue.ToString(), 
                    InvoiceAmountCB.SelectedIndex == -1 ? null : InvoiceAmountCB.SelectedValue.ToString(),
                    InvDatePicker.SelectedDate?.ToShortDateString());
                Loading(false);
            }
            catch (Exception ex)
            {
                await HandleError(ex);
            }
        }

        /// <summary>
        /// Creates a visual queue for the user to show that the search is searching.
        /// </summary>
        /// <param name="isLoading">If the program is currently loading data</param>
        private async void Loading(bool isLoading)
        {
            try
            {
                Opacity = isLoading ? 0.8 : 1;
                InvoiceAmountCB.IsEnabled = !isLoading;
                InvoiceNumberCB.IsEnabled = !isLoading;
                InvDatePicker.IsEnabled = !isLoading;
                ClearBtn.IsEnabled = !isLoading;
            }
            catch (Exception ex)
            {
                await HandleError(ex);
            }
        }

        /// <summary>
        /// No invoice is selected and sets DialogResult to false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = false;
                Close();
            }
            catch (Exception ex)
            {
                await HandleError(ex);
            }
        }

        /// <summary>
        /// Gets the invoice double clicked by the user from the DataGrid.
        /// Sets SelectedInvoice to the invoice to be used from other windows
        /// Sets DialogResult to true then closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RowDoubleClick_OnHandler(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridRow row = (DataGridRow)sender;
                SelectedInvoice = (Invoice)row.Item;
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                await HandleError(ex);
            }
        }
        
        /// <summary>
        /// Handles errors by showing the message and stack trace of the exception.
        /// Sets loading to false just in case the exception was thrown while data was loading.
        /// </summary>
        /// <param name="ex">The thrown exception</param>
        private async Task HandleError(Exception ex)
        {
            Loading(false);
            await ShowNotification(ex.Message, 3);
        }

        /// <summary>
        /// Shows a label with a given message for a number of seconds
        /// </summary>
        /// <param name="message">The text the notification shows</param>
        /// <param name="seconds">The amount of seconds the message stays on the screen</param>
        private async Task ShowNotification(string message, int seconds)
        {
            // if I did a try catch here it would cause a recursive death loop
            NotificationLbl.Visibility = Visibility.Visible;
            NotificationLbl.Content = message;
            await Task.Delay(seconds * 1000);
            NotificationLbl.Content = string.Empty;
            NotificationLbl.Visibility = Visibility.Collapsed;
        }
        
        /// <summary>
        /// Sets the selected invoice to the invoice represented by the row the user clicked on to allow
        /// for other windows to access it. If no invoice is highlighted a message will appear to notify
        /// the user how to select an invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SelectBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedInvoice = (Invoice)SearchResultsDataGrid.SelectedItem;
                if (SelectedInvoice != null)
                {
                    DialogResult = true;
                    Close();
                    return;
                }
                await ShowNotification("Please click or double click an invoice in the table to select it", 3);
            }
            catch (Exception ex)
            {
                await HandleError(ex);
            }
        }

        /// <summary>
        /// Clears the user input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBtn_OnClick(object sender, RoutedEventArgs e)
        {
            InvoiceNumberCB.SelectedIndex = -1;
            InvoiceAmountCB.SelectedIndex = -1;
            InvDatePicker.SelectedDate = null;
        }

        /// <summary>
        /// Searches on database on changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvDatePicker_OnSelectedDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Searches on database on changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceAmountCB_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Searches on database on changing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InvoiceNumberCB_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }
    }
}