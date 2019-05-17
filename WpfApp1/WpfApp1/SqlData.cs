using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    public class SqlData
    {
        SqlDataAdapter sqlProductsDataAdapter;
        SqlDataAdapter sqlUnitsDataAdapter;
        SqlDataAdapter sqlDataProvidersAdapter;
        public SqlData(SqlConnection sqlConnection)
        {
            sqlDataProvidersAdapter = new SqlDataAdapter("select * from c_malsatanlar", sqlConnection);
            sqlProductsDataAdapter = new SqlDataAdapter("select ad,id from c_mallar where is_active!=1", sqlConnection);
            sqlUnitsDataAdapter = new SqlDataAdapter("select * from c_units", sqlConnection);


        }
        public DataTable GetProductsDataTable()
        {
            DataTable dataTable = new DataTable();
            sqlProductsDataAdapter.Fill(dataTable: dataTable);
            return dataTable;
        }
        public DataTable GetUnitsDataTable()
        {
            DataTable dataTable = new DataTable();
            sqlUnitsDataAdapter.Fill(dataTable: dataTable);
            return dataTable;
        }
        public DataTable GetProvidersDataTable()
        {
            DataTable dataTable = new DataTable();
            sqlDataProvidersAdapter.Fill(dataTable: dataTable);
            return dataTable;
        }
    }
}
