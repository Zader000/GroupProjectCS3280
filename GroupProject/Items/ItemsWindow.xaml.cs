using GroupProject.Data;
using GroupProject.Items;
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

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for ItemsWindow.xaml
    /// </summary>
    public partial class ItemsWindow : Window
    {
        private clsItemsLogic _logic = new clsItemsLogic();
        private string code, cost, desc;
        private clsDataAccess _data;
        public ItemsWindow()
        {
            InitializeComponent();
            _logic.LoadItemsFromDatabase();
            _data = new clsDataAccess();
            RefreshDataGrid();
        }


        /// <summary>
        /// function to refresh the datagrid, prints out all the items in the datagrid.
        /// </summary>
        private void RefreshDataGrid()
        {
            //dataGrid.ItemsSource = _logic.getItems();
            string query = "SELECT * FROM ItemDesc";
            DataSet table = _data.ExecuteQuery(query);
            dataGrid.ItemsSource = table.Tables[0].DefaultView;
        }


        /// <summary>
        /// Takes the user input and allows them to edit an item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            code = txtCode.Text;
            cost = txtCost.Text;
            desc = txtDesc.Text;
            if(code == null || cost == null || desc == null)
            {
                lblDisplay.Content = "Please make sure you fill out all fields for the\n" +
                    "item you want to edit.";

                return;
            }
            else
            {
                _logic.UpdateItem(desc, cost, code);
                lblDisplay.Content = "Item added to database.";
                RefreshDataGrid();
            }
        }

        /// <summary>
        /// Method that deletes an item from the database. It checks whether an item appears on an invoice and 
        /// makes sure the user actually wants to delete it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //make use of business logic functions to allow user to delete the items.
            
            code = txtCode.Text;
            cost = txtCost.Text;
            desc = txtDesc.Text;
            if (_logic.checkItem(code))
            {
                MessageBoxResult result= MessageBox.Show("Warning: This item appears in an invoice. Do you still want to " +
                    "delete the item?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    if (code == null || cost == null || desc == null)
                    {
                        lblDisplay.Content = "Please make sure you have added the correct matching data to \n" +
                            "delete the appropriate item.";
                        return;
                    }
                    else
                    {
                        _logic.DeleteItem(code);
                        lblDisplay.Content = "Item deleted from database.";
                        RefreshDataGrid();
                    }
                }
            }
        }


        /// <summary>
        /// Adds the data to the datagrid, using the user input. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            code = txtCode.Text;
            cost = txtCost.Text;
            desc = txtDesc.Text;
            if (code == null || cost == null || desc == null)
            {
                lblDisplay.Content = "Please make sure you are adding content to all textboxes.";
                return;
            }
            else
            {
                _logic.AddItem(code, desc, cost);
                lblDisplay.Content = "Item added to database.";
                RefreshDataGrid();
            }
            
        }


    }
}
