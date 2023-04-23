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
        /// <summary>
        /// Updates the invoice based on totalcost and invoice number
        /// </summary>
        /// <returns></returns>
        /*public static string UpdatebyInvoiceNum(int cost, int InvNum) 
        {
            return $"UPDATE Invoices SET TotalCost = {cost} WHERE InvoiceNum = {InvNum};";
        }*/


        /// <summary>
        /// Updates the invoice based on totalcost and invoice number
        /// </summary>
        /// <returns></returns>
        public static string UpdateInvoiceAmountStatement(int invoiceNumber, double invoiceAmount)
        {
            return $"UPDATE Invoices SET InvoiceAmount = {invoiceAmount} where InvoiceNumber = {invoiceNumber}";
        }


        /// <summary>
        /// Makes an insert into the lineitems based on num, lineitem and itemcode
        /// </summary>
        /// <returns></returns>
        public static string InsertLineItems(int num, int LineItem, string ItemCode) 
        {
            return $"INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values({num}, {LineItem}, {ItemCode});";
        }

        public static string InsertInvoices(string Date, double Cost)
        {
            return $"INSERT INTO Invoices(InvoiceDate, TotalCost) Values({Date}, {Cost});";
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
