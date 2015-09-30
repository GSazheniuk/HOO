using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Admin
{
    public partial class GlobalParameters
    {
        public static SqlTransaction Transaction { get; set; }
    }

    public class DataGate
    {
        #region Private fields
        private SqlConnection _con;
        private SqlTransaction _trn;
        #endregion

        #region Public properties
        public string ConnectionString
        {
            get
            {
                return _con.ConnectionString;
            }
            set
            {
                this._con = new SqlConnection(value);
            }
        }
        public SqlConnection Connection
        {
            get { return this._con; }
        }
        #endregion

        #region Constructors
        public DataGate()
        {
        }

        public DataGate(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        #endregion

        #region Private methods
        private bool IsConnect()
        {
            if (this._con != null)
            {
                return this._con.State == System.Data.ConnectionState.Open;
            }
            else
            {
                return false;
            }
        }

        private bool Connect()
        {
            if (!IsConnect())
            {
                this._con.Open();
            }
            return IsConnect();
        }

        private bool Close()
        {
            if (IsConnect())
            {
                this._con.Close();
            }
            return !IsConnect();
        }
        #endregion

        #region Public methods
        public SqlTransaction BeginTransaction()
        {
            if (GlobalParameters.Transaction == null)
            {
                GlobalParameters.Transaction = this._con.BeginTransaction();
            }
            return GlobalParameters.Transaction;
        }

        public DataSet GetDataSet(string sqlStatement)
        {
            SqlCommand sqlCmd = new SqlCommand(sqlStatement);
            return this.GetDataSet(sqlCmd);
        }

        public DataSet GetDataSet(SqlCommand sqlStatement)
        {
            DataSet res = null;
            if (Connect())
            {
                res = new DataSet();
                sqlStatement.Connection = this._con;
                sqlStatement.Transaction = GlobalParameters.Transaction;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlStatement);
                dataAdapter.Fill(res);
            }
            return res;
        }

        public object ExecuteScalar(string sqlStatement)
        {
            if (Connect())
            {
                SqlCommand sqlCmd = new SqlCommand(sqlStatement, this._con);
                sqlCmd.Transaction = GlobalParameters.Transaction;
                return sqlCmd.ExecuteScalar();
            }
            return null;
        }

        public object ExecuteScalar(SqlCommand sqlCmd)
        {
            if (Connect())
            {
                sqlCmd.Transaction = GlobalParameters.Transaction;
                return sqlCmd.ExecuteScalar();
            }
            return null;
        }

        public void Execute(string sqlStatement)
        {
            if (Connect())
            {
                SqlCommand sqlCmd = new SqlCommand(sqlStatement, this._con);
                sqlCmd.Transaction = GlobalParameters.Transaction;
                sqlCmd.ExecuteNonQuery();
            }
        }

        public void Execute(SqlCommand sqlCmd)
        {
            if (Connect())
            {
                sqlCmd.Transaction = GlobalParameters.Transaction;
                sqlCmd.ExecuteNonQuery();
            }
        }

        public SqlCommand ExecuteCommand(SqlCommand sqlCmd)
        {
            if (Connect())
            {
                sqlCmd.Connection = this._con;
                sqlCmd.Transaction = GlobalParameters.Transaction;
                sqlCmd.ExecuteNonQuery();
            }
            return sqlCmd;
        }
        #endregion
    }
}
