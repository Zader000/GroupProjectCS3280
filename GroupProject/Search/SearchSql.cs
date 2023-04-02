using System;

namespace GroupProject.Search
{
    /// <summary>
    /// Responsible for creating sql statements for the SearchLogic class to use.
    /// </summary>
    public static class SearchSql
    {
        
        /// <summary>
        /// SQL statement to get invoices by invoice number
        /// </summary>
        /// <param name="invNumber">Invoice Number</param>
        /// <returns>SQL String</returns>
        public static string GetInvoiceByInvoiceNumberQuery(string invNumber)
        {
            return $"select * from Invoice where InvoiceNumber = {invNumber};";
        }
        
        /// <summary>
        /// SQL Statement to get invoices by date
        /// </summary>
        /// <param name="date">Invoice Date</param>
        /// <returns>SQL String</returns>
        public static string GetInvoiceByDateQuery(string date)
        {
            return
                $"select * from Invoice where InvoiceDate = #{date}#;";
        }

        /// <summary>
        /// SQL statement to get invoices by amount
        /// </summary>
        /// <param name="amount">Invoice Amount</param>
        /// <returns>SQL String</returns>
        public static string GetInvoiceByAmountQuery(string amount)
        {
            return
                $"select * from Invoice where InvoiceAmount = {amount};";
        }

        /// <summary>
        /// SQL statement to get invoices by amount and date
        /// </summary>
        /// <param name="amount">Invoice Amount</param>
        /// <param name="dateStr">Invoice Date</param>
        /// <returns>SQL String</returns>
        public static string GetInvoiceByDateAndAmountQuery(string amount, string dateStr)
        {
            return $"select * from Invoice where InvoiceAmount = {amount} and InvoiceDate = #{dateStr}#;";
        }

        /// <summary>
        /// SQL statement to get an invoice by invoice number and date
        /// </summary>
        /// <param name="invNumber">Invoice Number</param>
        /// <param name="date">Invoice Date</param>
        /// <returns>SQL String</returns>
        public static string GetInvoiceByDateAndNumberQuery(string invNumber, string date)
        {
            return
                $"select * from Invoice where InvoiceNumber = {invNumber} and InvoiceDate = #{date}#;";
        }

        /// <summary>
        /// SQL statement to get invoices by invoice number and amount
        /// </summary>
        /// <param name="invNumber">Invoice Number</param>
        /// <param name="amount">Invoice Amount</param>
        /// <returns>SQL String</returns>
        public static string GetInvoiceByNumberAndAmountQuery(string invNumber, string amount)
        {
            return
                $"select * from Invoice where InvoiceNumber = {invNumber} and InvoiceAmount = {amount};";
        }
        
        /// <summary>
        /// SQL statement to get an invoice by invoice number, amount, and date
        /// </summary>
        /// <param name="invNumber">Invoice Number</param>
        /// <param name="amount">Invoice AMount</param>
        /// <param name="date">Invoice Date</param>
        /// <returns>SQL String</returns>
        public static string GetInvoiceByInvoiceNumberAmountDateQuery(string invNumber, string amount, string date)
        {
            return
                $"select * from Invoice where InvoiceNumber = {invNumber} and InvoiceAmount = {amount} and InvoiceDate = #{date}#;";
        }
        
        /// <summary>
        /// SQL statement to get all invoices
        /// </summary>
        /// <returns>SQL String</returns>
        public static string GetAllInvoicesQuery()
        {
            return "select * from Invoice;";
        }
    }
}
