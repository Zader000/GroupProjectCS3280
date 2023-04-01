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
        public void LoadItemsFromDatabase()
        {
            // TODO: Implement code to load items from the database into _items
            
        }

        public void AddItem(Item newItem)
        {
            // TODO: Implement code to add a new item to the database and _items
        }

        public void UpdateItem(Item updatedItem)
        {
            // TODO: Implement code to update an item in the database and _items
        }

        public void DeleteItem(Item itemToDelete)
        {
            // TODO: Implement code to delete an item from the database and _items
        }

        public bool IsItemInUseOnInvoice(Item item)
        {
            // TODO: Implement code to check if an item is currently in use on an invoice
            // Return true if the item is in use, false otherwise
            return false;
        }
}
}
