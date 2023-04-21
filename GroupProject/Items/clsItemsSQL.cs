using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Items
{
    public class clsItemsSQL
    {
        private string sConnectionString;
            
        //todo remove this already exists in clsDataAccess
        public clsItemsSQL()
        {
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\DB.accdb";
        }
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
