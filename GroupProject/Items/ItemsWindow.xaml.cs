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
            //dataGrid.ItemsSource = _logic.Items;
            _data = new clsDataAccess();
            //_data.PopulateItemGrid();
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            //dataGrid.ItemsSource = _logic.getItems();
            string query = "SELECT * FROM ItemDesc";
            DataSet table = _data.ExecuteQuery(query);
            dataGrid.ItemsSource = table.Tables[0].DefaultView;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            /*code = txtCode.Text;
            cost = txtCost.Text;
            desc = txtDesc.Text;
            //make use of business logic functions to allow user to edit the items.
            var selectedItem = dataGrid.SelectedItem as Item;

            if (selectedItem != null)
            {
                *//*AddEditItemWindow addEditItemWindow = new AddEditItemWindow(selectedItem);
                addEditItemWindow.Owner = this;

                if (addEditItemWindow.ShowDialog() == true)
                {
                    _logic.UpdateItem(addEditItemWindow.Item, *//*pass in the value to edit here.*//*);
                    RefreshDataGrid();
                }*//*
            }*/
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
                _logic.UpdateItem(code, desc, cost);
                lblDisplay.Content = "Item added to database.";
            }
        }


        

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //make use of business logic functions to allow user to delete the items.
            /*var selectedItem = dataGrid.SelectedItem as Item;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    _logic.DeleteItem(selectedItem);
                    RefreshDataGrid();
                }
            }*/
            code = txtCode.Text;
            cost = txtCost.Text;
            desc = txtDesc.Text;
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
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            /*code = txtCode.Text;
            cost = txtCost.Text;
            desc = txtDesc.Text;
            //make use of business logic functions to allow user to add the items.
            AddEditItemWindow addEditItemWindow = new AddEditItemWindow();
            addEditItemWindow.Owner = this;

            if (addEditItemWindow.ShowDialog() == true)
            {
                _logic.AddItem(addEditItemWindow.Item);
                RefreshDataGrid();
            }*/
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
                _logic.AddItem(desc, cost, code);
                lblDisplay.Content = "Item added to database.";
            }
        }
    }
}
