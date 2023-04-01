﻿using System;
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

        public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
        {
            try
            {
                DataSet ds = new DataSet();
                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {
                        conn.Open();

                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        adapter.Fill(ds);
                    }
                }

                iRetVal = ds.Tables[0].Rows.Count;

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + "->" + ex.Message);
            }
        }
    }
}
