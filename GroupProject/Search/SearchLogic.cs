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
        /// Executes a sql query that is determined by the given parameters. If a parameter is null it will not be 
        /// included in the where clause.
        /// </summary>
        /// <param name="invNumber"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static IList<Invoice> GetInvoices(string? invNumber, string? amount, string? date)
        {
            if (invNumber != null && amount != null && date != null)
                return ParseResults(new clsDataAccess().ExecuteQuery(SearchSql.GetInvoiceByInvoiceNumberAmountDateQuery(invNumber, amount, date)));
            if (invNumber != null && amount != null && date == null)
                return ParseResults(
                    new clsDataAccess().ExecuteQuery(SearchSql.GetInvoiceByNumberAndAmountQuery(invNumber, amount)));
            if (invNumber != null && amount == null && date == null)
                return ParseResults(
                    new clsDataAccess().ExecuteQuery(SearchSql.GetInvoiceByInvoiceNumberQuery(invNumber)));
            if (invNumber == null && amount != null && date != null)
                return ParseResults(
                    new clsDataAccess().ExecuteQuery(SearchSql.GetInvoiceByDateAndAmountQuery(amount, date)));
            if (invNumber == null && amount == null && date != null)
                return ParseResults(new clsDataAccess().ExecuteQuery(SearchSql.GetInvoiceByDateQuery(date)));
            if (invNumber == null && amount != null && date == null)
                return ParseResults(new clsDataAccess().ExecuteQuery(SearchSql.GetInvoiceByAmountQuery(amount)));
            if (invNumber != null && amount == null && date != null)
                return ParseResults(
                    new clsDataAccess().ExecuteQuery(SearchSql.GetInvoiceByDateAndNumberQuery(invNumber, date)));
            return ParseResults(new clsDataAccess().ExecuteQuery(SearchSql.GetAllInvoicesQuery()));
        }

        /// <summary>
        /// Takes a DataSet that has invoice information and parses it it into Invoice Objects and returns a list of them
        /// </summary>
        /// <param name="results">IList&lt;Invoice&gt; from the rows of the DataSet</param>
        /// <returns></returns>
        private static IList<Invoice> ParseResults(DataSet results)
        {
            IList<Invoice> invoices = new List<Invoice>();
            foreach (DataRow row in results.Tables[0].Rows)
            {
                invoices.Add(new Invoice(int.Parse(row["ID"].ToString()),
                    int.Parse(row["InvoiceNumber"].ToString()), row["CustomerName"].ToString(),
                    row["InvoiceDate"].ToString(), double.Parse(row["InvoiceAmount"].ToString())));
            }
            return invoices;
        }
    }
}
