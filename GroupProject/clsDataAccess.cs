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
   
    public class clsDataAccess
    {
        private const string _connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=../../../Data/DB.accdb";



        /// <summary>
        /// Executes the specified SQL query and returns the result in a dataset.
        /// </summary>
        /// <param name="stmt">The SQL query to execute.</param>
        /// <returns>The result of the query in a dataset.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while executing the query.</exception>
        public DataSet ExecuteQuery(string stmt)
        {
            try
            {
                DataSet ds = new DataSet();
                using OleDbConnection conn = new OleDbConnection(_connectionString);
                using OleDbDataAdapter adapter = new OleDbDataAdapter();
                conn.Open();
                adapter.SelectCommand = new OleDbCommand(stmt, conn);
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(ds);
                conn.Close();
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
        /// Executes a non-query SQL statement on the database and returns the number of rows affected.
        /// </summary>
        /// <param name="stmt">The SQL statement to execute.</param>
        /// <returns>The number of rows affected by the executed SQL statement.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while executing the SQL statement.</exception>
        public int ExecuteNonQuery(string stmt)
        {
            //executes non query
            try
            {
                using OleDbConnection conn = new OleDbConnection(_connectionString);
                using OleDbCommand cmd = new OleDbCommand(stmt, conn);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
                return rowsAffected;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show(e.StackTrace);
                throw;
            }
        }
    }
}
