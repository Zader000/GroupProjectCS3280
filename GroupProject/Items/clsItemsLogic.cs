using GroupProject.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Items
{
    public class clsItemsLogic
    {
        private List<Item> items;
        private ItemsWindow components;
        private clsDataAccess data;
        private clsItemsSQL sql;
        public clsItemsLogic()
        {
            items = new List<Item>();
            //components = new ItemsWindow();
            data = new clsDataAccess();
            sql = new clsItemsSQL();
        }

        public List<Item> getItems()
        {
            return items;
        }

        
        public void LoadItemsFromDatabase()
        {
            string query = clsItemsSQL.getItems();
            DataSet ds = data.ExecuteQuery(query);
            
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string itemCode = row["ItemCode"].ToString() ?? "";
                string itemDesc = row["ItemDesc"].ToString() ?? "";
                string cost = row["Cost"].ToString() ?? "";
                items.Add(new Item(itemCode, itemDesc, cost));
            }
        }

        public void AddItem(string ItemCode, string ItemDesc, string Cost)
        {
            // TODO: Implement code to add a new item to the database and _items
            //items.Add(newItem);
            
            data.ExecuteQuery(clsItemsSQL.insertDesc(ItemCode, ItemDesc, Cost));
            
        }



        public void UpdateItem(string ItemDesc, string Cost, string ItemCode)
        {
            // TODO: Implement code to update an item in the database and _items
            
            data.ExecuteNonQuery(clsItemsSQL.updateDesc(ItemDesc, Cost, ItemCode));

        }

        public void DeleteItem(string ItemCode)
        {
            // TODO: Implement code to delete an item from the database and _items
            
            data.ExecuteQuery(clsItemsSQL.deleteDesc(ItemCode));
        }

        /*public bool IsItemInUseOnInvoice(Item item)
        {
            // TODO: Implement code to check if an item is currently in use on an invoice
            // Return true if the item is in use, false otherwise
            foreach (Invoice invoice in clsInvoiceLogic.Invoices)
            {
                foreach (InvoiceLineItem lineItem in invoice.LineItems)
                {
                    if (lineItem.Item == item)
                    {
                        return true;
                    }
                }
            }
            return false;
        }*/
}
}
