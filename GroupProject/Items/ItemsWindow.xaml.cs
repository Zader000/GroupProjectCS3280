using GroupProject.Items;
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
using System.Windows.Shapes;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for ItemsWindow.xaml
    /// </summary>
    public partial class ItemsWindow : Window
    {
        private clsItemsLogic _logic = new clsItemsLogic();
        public ItemsWindow()
        {
            InitializeComponent();
            _logic.LoadItemsFromDatabase();
            //dataGrid.ItemsSource = _logic.Items;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //make use of business logic functions to allow user to edit the items.
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //make use of business logic functions to allow user to save the items.
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //make use of business logic functions to allow user to delete the items.
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //make use of business logic functions to allow user to add the items.
        }
    }
}
