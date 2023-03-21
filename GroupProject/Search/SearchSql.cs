using System;

namespace GroupProject.Search
{
    /// <summary>
    /// Responsible for creating sql statements for the SearchLogic class to use.
    /// </summary>
    public static class SearchSql
    {
        
        /// <summary>
        /// Creates the sql statement for the Invoice table
        /// </summary>
        /// <param name="field">Column Name</param>
        /// <param name="value">Value to be compared</param>
        /// <returns>SQL Statement string</returns>
        /// <exception cref="Exception">
        /// Throws an exception if the field is InvoiceAmount or InvoiceNumber and the value is not a number
        /// </exception>
        public static string GetSearchInvoicesQuery(string field, string value)
        {
            string valStr = field switch
            {
                "InvoiceNumber" when !int.TryParse(value, out int intVal) => throw new Exception(
                    $"The value for {field} must be a number"),
                "InvoiceAmount" when !double.TryParse(value, out double dblVal) => throw new Exception(
                $"The value for {field} must be a number"),
                "InvoiceAmount" or "InvoiceNumber" => value,
                "InvoiceDate" => $"#{value}#",
                _ => $"'{value}'"
            };
            return $"select * from InvoiceTest where {field} = {valStr};";
        }

        public static string GetAllInvoicesQuery()
        {
            return "select * from InvoiceTest;";
        }
    }
}
