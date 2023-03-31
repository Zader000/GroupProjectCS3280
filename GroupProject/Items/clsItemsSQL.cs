using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Items
{
    public static class clsItemsSQL
    {
        public static string getItems()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }

        public static string getInvoiceNum(string ItemCode)
        {
            return $"SELECT distinct(InvoiceNum) FROM LineItems WHERE ItemCode = '{ItemCode}'";
        }

        public static string updateDesc(string ItemDesc, string Cost, string ItemCode)
        {
            return $"UPDATE ItemDesc SET ItemDesc = {ItemDesc}, COST = {Cost} WHERE ItemCode = '{ItemCode}'";
        }

        public static string insertDesc(string ItemCode, string ItemDesc, string Cost)
        {
            return $"INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) VALUES('{ItemCode}', '{ItemDesc}', {Cost})";
        }

        public static string deleteDesc(string ItemCode)
        {
            return $"DELETE FROM ItemDesc Where ItemCode = '{ItemCode}'";
        }
    }
}
