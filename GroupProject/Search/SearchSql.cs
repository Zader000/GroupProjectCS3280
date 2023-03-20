using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GroupProject.Data;

namespace GroupProject.Search
{
    /// <summary>
    /// Handles the communication of the database and the GUI. Does so by interpreting the user input, formatting the SQL,
    /// and returning the query results back.
    /// </summary>
    public static class SearchSql
    {
        /// <summary>
        /// Queries the database Invoice table for invoices based on the field(column) and value
        /// </summary>
        /// <param name="field">Column to be filtered on</param>
        /// <param name="value">Value in the where clause</param>
        /// <returns>IAsyncEnumerable&lt;Invoice&gt;</returns>
        /// <exception cref="Exception">
        /// Throws an exception if the field is ID or InvoiceNumber and the value is not an integer
        /// </exception>
        public static async IAsyncEnumerable<Invoice> SearchInvoices(string field, string value)
        {
            string valStr = field switch
            {
                "ID" or "InvoiceNumber" when !int.TryParse(value, out int intVal) => throw new Exception(
                    $"The value for {field} must be a number"),
                "ID" or "InvoiceNumber" => value,
                "InvoiceDate" => $"#{value}#",
                _ => $"'{value}'"
            };
            
            string stmt = $"select * from InvoiceTest where {field} = {valStr};";

            clsDataAccess da = new clsDataAccess();
            DataSet results = await da.ExecuteQuery(stmt);

            foreach (DataRow row in results.Tables[0].Rows)
            {
                yield return new Invoice(int.Parse(row["ID"].ToString()),
                    int.Parse(row["InvoiceNumber"].ToString()), row["CustomerName"].ToString(),
                    row["InvoiceDate"].ToString(), double.Parse(row["InvoiceAmount"].ToString()));
            }
        }
    }
}
