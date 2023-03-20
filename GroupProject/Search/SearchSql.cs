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
    public static class SearchSql
    {
        public static async IAsyncEnumerable<Invoice> SearchInvoices(string field, string value)
        {
            string valStr;
            
            if (field is "ID" or "InvoiceNumber")
            {
                if (!int.TryParse(value, out int intVal))
                    throw new Exception($"The value for {field} must be a number");
                valStr = value;
            }
            else if (field == "InvoiceDate")
            {
                valStr = $"#{value}#";
            }
            else
            {
                valStr = $"'{value}'";
            }
            string stmt = $"select * from InvoiceTest where {field} = {valStr};";

            clsDataAccess da = new clsDataAccess();
            DataSet results = await da.ExecuteQuery(stmt);

            foreach (DataRow row in results.Tables[0].Rows)
            {
                yield return new Invoice(int.Parse(row["ID"].ToString()),
                    int.Parse(row["InvoiceNumber"].ToString()), row["CustomerName"].ToString(),
                    row["InvoiceDate"].ToString(), new SqlMoney(double.Parse(row["InvoiceAmount"].ToString())));
            }
            
            // Simulate load time
            await Task.Delay(1000);
        }
    }
}
