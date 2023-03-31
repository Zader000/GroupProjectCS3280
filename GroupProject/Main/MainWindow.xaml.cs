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

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SearchWindow wndSearch;
        
        public MainWindow()
        {
            InitializeComponent();
            wndSearch = new SearchWindow();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

        }

        private void SearchClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //this.Hide();              
                //Unsure why this is working
                SearchWindow.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

      

        //After search window is closed, check property SelectedInvoiceId in the Search window to see if an invoice is selected. If so load invoice.

        //After Items window is closed, check property HasItemsBeenChanged in the Items window to see if any items were updated. If so re-load items in combo box.
    }
}
