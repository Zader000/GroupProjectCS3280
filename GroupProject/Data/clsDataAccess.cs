using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows;
using System.Data;

namespace GroupProject.Data
{
   
    internal class clsDataAccess
    {
        private const string _connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../Data/DB.accdb";

        public clsDataAccess() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public async Task<DataSet> ExecuteQuery(string stmt)
        {
            try
            {
                DataSet ds = new DataSet();
                await using OleDbConnection conn = new OleDbConnection(_connectionString);
                using OleDbDataAdapter adapter = new OleDbDataAdapter();
                await conn.OpenAsync();
                adapter.SelectCommand = new OleDbCommand(stmt, conn);
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(ds);
                await conn.CloseAsync();
                return ds;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show(e.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int ExecuteNonQuery(string stmt)
        {
            throw new NotImplementedException();
        }
    }
}
