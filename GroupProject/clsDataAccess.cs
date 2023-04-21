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
        /// 
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
        public DataSet ExecuteQuery(string stmt)
        {
            //_items = new ItemsWindow();
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
        /// 
        /// </summary>
        /// <param name="stmt"></param>
        /// <returns></returns>
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
