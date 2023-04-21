using GroupProject.Data;
using System;
using System.Collections.Generic;
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
            // TODO: Implement code to load items from the database into _items
            
        }

        public void AddItem(string ItemDesc, string Cost, string ItemCode)
        {
            // TODO: Implement code to add a new item to the database and _items
            //items.Add(newItem);
            /*ItemDesc = components.txtDesc.Text;
            Cost = components.txtCost.Text;
            ItemCode = components.txtCode.Text;*/
            data.ExecuteQuery(clsItemsSQL.insertDesc(ItemDesc, Cost, ItemCode));
            
        }



        public void UpdateItem(string ItemDesc, string Cost, string ItemCode)
        {
            // TODO: Implement code to update an item in the database and _items
            /*int index = items.IndexOf(itemToEdit);

            if(index != -1)
            {
                items[index] = updatedItem;
            }
            else
            {
                throw new ArgumentException("Item does not exist in list.");
            }*/
            data.ExecuteQuery(clsItemsSQL.updateDesc(ItemCode, ItemDesc, Cost));

        }

        public void DeleteItem(string ItemCode)
        {
            // TODO: Implement code to delete an item from the database and _items
            /*bool success = items.Remove(itemToDelete);

            if (!success)
            {
                throw new ArgumentException("Item does not exist in list.");
            }*/
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
