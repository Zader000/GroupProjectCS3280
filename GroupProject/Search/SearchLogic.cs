using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupProject.Data;

namespace GroupProject.Search
{
    /// <summary>
    /// Handles the communication of the database and the GUI. Does so by interpreting the user input, and returning the query results back.
    /// </summary>
    public static class SearchLogic
    {
        /// <summary>
        /// Queries the database Invoice table for invoices based on the field(column) and value
        /// if both parameters are null, the method returns all invoices.
        /// </summary>
        /// <param name="field">Column to be filtered on</param>
        /// <param name="value">Value in the where clause</param>
        /// <returns>IAsyncEnumerable&lt;Invoice&gt;</returns>
        /// <exception cref="ArgumentNullException">
        /// Throws argument null exception if one but not the other parameter is null
        /// Both can be null to get all invoices though
        /// </exception>
        public static IEnumerable<Invoice> SearchInvoices(string? field, string? value)
        {
            if ((field == null && value != null) || (value == null && field != null))
                throw new ArgumentNullException("Both params need to be null or not null");
            
            string stmt = 
                field != null && value != null 
                    ? SearchSql.GetSearchInvoicesQuery(field, value)
                    : SearchSql.GetAllInvoicesQuery();
            clsDataAccess da = new clsDataAccess();
            DataSet results = da.ExecuteQuery(stmt);

            foreach (DataRow row in results.Tables[0].Rows)
            {
                yield return new Invoice(int.Parse(row["ID"].ToString()),
                    int.Parse(row["InvoiceNumber"].ToString()), row["CustomerName"].ToString(),
                    row["InvoiceDate"].ToString(), double.Parse(row["InvoiceAmount"].ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IList<Invoice> GetAllInvoices()
        {
            return SearchInvoices(null, null).ToList();
        }
    }
}
