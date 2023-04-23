using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GroupProject.Items;
using GroupProject.Main;
using Application = System.Windows.Application;

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
        private IList<Item> Items;

        private int InvoiceID { get; set; }
        private Item SelectedItem { get; set; }

        private Invoice SelectedInvoice { get; set; }

        private IList<Item> InvoiceItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            _logic = new MainLogic();

            Items = _logic.LoadItemsFromDataBase();
            ItemsComboBox.ItemsSource = (from item in Items select $"{item.Code}. {item.Description}").ToList();
            InvoiceItems = new List<Item>();

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
                DateLabelInvoice.Content = ("Invoice Date: " + SelectedInvoice.InvoiceDate);
                EditNumber.Text = SelectedInvoice.InvoiceNumber.ToString();
                EditAmount.Text = SelectedInvoice.InvoiceAmount.ToString();
                MainTabControl.SelectedIndex = 1;

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

        private void AddInvoice(object sender, RoutedEventArgs e)
        {
            if (InvoiceNumText.Text == "" || CustomerText.Text == "" || AmountText.Text == "" || DateText.Text == "")
            {
                ErrorLabel.Content = "Error! Invoice fields must all be filled out!";
                return;
            }

            ErrorLabel.Content = "";
            _logic.InsertInvoices(Int32.Parse(InvoiceNumText.Text), CustomerText.Text, DateText.Text, Double.Parse(AmountText.Text));
            int i = 1;
            foreach (Item item in InvoiceItems)
            {
                _logic.InsertInvoiceLineItems(Int32.Parse(InvoiceNumText.Text), i, item.Code);
                i++;
            }
            InvoiceNumText.Text = "";
            CustomerText.Text = "";
            AmountText.Text = "";
            DateText.Text = "";
            InvoiceItems.Clear();
            ItemDataGrid.ItemsSource = null;
            SelectedInvoice = _logic.GetMostRecentlyAddedInvoice();
            DateLabelInvoice.Content = ("Invoice Date: " + SelectedInvoice.InvoiceDate);
            EditNumber.Text = SelectedInvoice.InvoiceNumber.ToString();
            EditAmount.Text = SelectedInvoice.InvoiceAmount.ToString();
            MainTabControl.SelectedIndex = 1;
        }

        private void EditInvoice(object sender, RoutedEventArgs e)
        {
            if (EditAmount.Text == "" || EditNumber.Text == "")
            {
                ErrorLabel.Content = "Error! Number and amount must be filled in!";
                return;
            }
            ErrorLabel.Content = "";

            _logic.UpdateInvoiceAmount(Int32.Parse(EditNumber.Text), Double.Parse(EditAmount.Text));
        }

        private void DeleteInvoice(object sender, RoutedEventArgs e)
        {
            if (DeleteText.Text == "")
            {
                ErrorLabel.Content = "Error! You must first enter an invoice number!";
                return;
            }
            ErrorLabel.Content = "";

            _logic.Delete(Int32.Parse(DeleteText.Text));
        }

        //After search window is closed, check property SelectedInvoiceId in the Search window to see if an invoice is selected. If so load invoice.

        //After Items window is closed, check property HasItemsBeenChanged in the Items window to see if any items were updated. If so re-load items in combo box.


        private void ItemsComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedItem = (from item in Items where $"{item.Code}. {item.Description}" == ItemsComboBox.SelectedItem.ToString() select item)
                    .FirstOrDefault() ?? throw new Exception("No Matching Item Found");
                ReadOnly.Text = SelectedItem.Cost.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void AddComboBoxItem(object sender, RoutedEventArgs e)
        {
            if (ItemsComboBox.SelectedItem == null)
            {
                ErrorLabel.Content = "Error! No item is selected!";
                return;
            }
            ErrorLabel.Content = "";
            InvoiceItems.Add(SelectedItem);
            ItemDataGrid.ItemsSource = null;
            ItemDataGrid.ItemsSource = InvoiceItems;

        }

        private void DeleteComboItem(object sender, RoutedEventArgs e)
        {
            if (ItemsComboBox.SelectedItem == null)
            {
                ErrorLabel.Content = "Error! No item is selected!";
                return;
            }
            ErrorLabel.Content = "";
            InvoiceItems.Remove(SelectedItem);
        }
    }
}
