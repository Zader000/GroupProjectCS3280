using System;

namespace GroupProject.Search
{
    /// <summary>
    /// Responsible for creating sql statements for the SearchLogic class to use.
    /// </summary>
    public static class SearchSql
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
