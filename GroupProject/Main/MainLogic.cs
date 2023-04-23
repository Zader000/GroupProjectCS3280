using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using GroupProject.Data;
using GroupProject.Items;

namespace GroupProject.Main
{
    public class MainLogic
    {
        private readonly clsDataAccess _dataAccess;

        public MainLogic()
        {
            _dataAccess = new clsDataAccess();
        }
        //GetAllItems return List<clsItem>
        //SaveNewInvoice(clsInvoice)
        //EditInvoice(clsOldInvoice, clsNewInvoice)
        //GetInvoice(InvoiceNumber) returns clsInvoice - Get the invoice and items for the selected invoice from search window


        public Invoice GetInvoiceById(int invoiceId)
        {
            // Get the invoice from the database
            DataSet ds = _dataAccess.ExecuteQuery(MainSql.GetInvoiceByIdQuery(invoiceId));

            // Check if the query returned any data
            if (ds.Tables.Count == 0)
            {
                MessageBox.Show("No invoice found");
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("No invoice found");
            }
            // Get the invoice data
            int id = ds.Tables[0].Rows[0].Field<int>("ID");
            int invoiceNumber = ds.Tables[0].Rows[0].Field<int>("InvoiceNumber");
            string customerName = ds.Tables[0].Rows[0].Field<string>("CustomerName") ?? "";
            // Convert the date to a string
            string invoiceDate = ds.Tables[0].Rows[0].Field<DateTime>("InvoiceDate").ToString();
            // Convert the decimal to a double
            double invoiceAmount = (double)ds.Tables[0].Rows[0].Field<Decimal>("InvoiceAmount");

            // Return the invoice
            return new Invoice(id, invoiceNumber, customerName, invoiceDate, invoiceAmount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Item> LoadItemsFromDataBase() 
        {
            IList<Item> items = new List<Item>();
            string query = clsItemsSQL.getItems();
            DataSet ds = data.ExecuteQuery(query);

            foreach (DataRow row in ds.Tables[0].Rows) 
            {
                string ItemCode = row["ItemCode"].ToString();
                string ItemDesc = row["ItemDesc"].ToString();
                string Cost = row["Cost"].ToString();
                items.Add(new Item(ItemCode, ItemDesc, Cost));
            }
            return items;
        }


        /// <summary>
        /// Used to update the invoice amount on call based on invoice number and invoice amount
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="invoiceAmount"></param>
        public void UpdateInvoiceAmount(int invoiceNumber, double invoiceAmount)
        {
            // Create the query
            string query = MainSql.UpdateInvoiceAmountStatement(invoiceNumber, invoiceAmount);
            // Execute the query
            _dataAccess.ExecuteNonQuery(query);
        }

        public void InsertInvoices(int invoiceNumber, string customerName, string invoiceDate, double invoiceAmount) 
        {
            // Create the query
            string query = MainSql.InsertInvoice(invoiceNumber, customerName, invoiceDate, invoiceAmount);
            // Execute the query
            _dataAccess.ExecuteNonQuery(query);
        }

        public void Delete(int num) 
        {
            // Create the query
            string query = MainSql.Delete(num);
            // Execute the query
            _dataAccess.ExecuteNonQuery(query);
        }
    }
}
