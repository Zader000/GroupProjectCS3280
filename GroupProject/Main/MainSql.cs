using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    /// <summary>
    /// Responsible for creating sql statements for the MainLogic class to use.
    /// </summary>
    public static class MainSql
    {
        public static string GetInvoiceByInvoiceNumberQuery(string invNumber)
        {
            return $"select * from Invoice where InvoiceNumber = {invNumber};";
        }

        public static string GetInvoiceByDateQuery(string date)
        {
            return
                $"select * from Invoice where InvoiceDate = #{date}#;";
        }

        public static string GetInvoiceByAmountQuery(string amount)
        {
            return
                $"select * from Invoice where InvoiceAmount = {amount};";
        }

        public static string GetInvoiceByDateAndAmountQuery(string amount, string dateStr)
        {
            return $"select * from Invoice where InvoiceAmount = {amount} and InvoiceDate = #{dateStr}#;";
        }

        public static string GetInvoiceByDateAndNumberQuery(string invNumber, string date)
        {
            return
                $"select * from Invoice where InvoiceNumber = {invNumber} and InvoiceDate = #{date}#;";
        }

        public static string GetInvoiceByNumberAndAmountQuery(string invNumber, string amount)
        {
            return
                $"select * from Invoice where InvoiceNumber = {invNumber} and InvoiceAmount = {amount};";
        }

        public static string GetInvoiceByInvoiceNumberAmountDateQuery(string invNumber, string amount, string date)
        {
            return
                $"select * from Invoice where InvoiceNumber = {invNumber} and InvoiceAmount = {amount} and InvoiceDate = #{date}#;";
        }

        public static string GetAllInvoicesQuery()
        {
            return "select * from Invoice;";
        }
    }
}
//Examples of SQL statements needed for future use
/*
Main Window
- UPDATE Invoices SET TotalCost = 1200 WHERE InvoiceNum = 123
- INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) Values (123, 1, 'AA')
- INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#4/13/2018#, 100)
- SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = 123
- select ItemCode, ItemDesc, Cost from ItemDesc
- SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = 5000
- DELETE FROM LineItems WHERE InvoiceNum = 5000
 */