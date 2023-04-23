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
        
        public static string GetInvoiceByIdQuery(int id)
        {
            return $"select * from Invoice where ID = {id};";
        }

        public static string UpdateInvoiceAmountStatement(int invoiceNumber, double invoiceAmount)
        {
            return "update Invoices set InvoiceAmount = {invoiceAmount} where InvoiceNumber = {invoiceNumber}";
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