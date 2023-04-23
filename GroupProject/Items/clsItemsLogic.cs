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

        /// <summary>
        /// Method for getting the list of items.
        /// </summary>
        /// <returns>A list of Item objects.</returns>
        public List<Item> getItems()
        {
            return items;
        }


        /// <summary>
        /// Method for loading items from the database and storing them in the items list.
        /// </summary>
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

        /// <summary>
        /// Method for adding a new item to the database and the items list.
        /// </summary>
        /// <param name="ItemCode">The item code of the new item.</param>
        /// <param name="ItemDesc">The item description of the new item.</param>
        /// <param name="Cost">The cost of the new item.</param>
        public void AddItem(string ItemCode, string ItemDesc, string Cost)
        {
            // TODO: Implement code to add a new item to the database and _items
            //items.Add(newItem);
            
            data.ExecuteQuery(clsItemsSQL.insertDesc(ItemCode, ItemDesc, Cost));
            
        }


        /// <summary>
        /// Method for updating an item in the database and the items list.
        /// </summary>
        /// <param name="ItemDesc">The new item description for the item to be updated.</param>
        /// <param name="Cost">The new cost for the item to be updated.</param>
        /// <param name="ItemCode">The item code of the item to be updated.</param>
        public void UpdateItem(string ItemDesc, string Cost, string ItemCode)
        {
            // TODO: Implement code to update an item in the database and _items
            
            data.ExecuteNonQuery(clsItemsSQL.updateDesc(ItemDesc, Cost, ItemCode));

        }

        /// <summary>
        /// Method for deleting an item from the database and the items list.
        /// </summary>
        /// <param name="ItemCode">The item code of the item to be deleted.</param>
        public void DeleteItem(string ItemCode)
        {
            // TODO: Implement code to delete an item from the database and _items
            
            data.ExecuteQuery(clsItemsSQL.deleteDesc(ItemCode));
        }


        /// <summary>
        /// Checks if an item appears in an invoice by querying the database.
        /// </summary>
        /// <param name="itemCode">The code of the item to check.</param>
        /// <returns>Returns true if the item appears in an invoice, false otherwise.</returns>
        public bool checkItem(string itemCode)
        {
            string query = clsItemsSQL.getInvoiceNum(itemCode);
            DataSet results = data.ExecuteQuery(query);
            if(results.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
}
}
