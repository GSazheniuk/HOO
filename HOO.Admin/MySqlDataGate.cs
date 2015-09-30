using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOO.Admin
{
	public partial class GlobalParameters
	{
		public static MySqlTransaction MySqlTransaction { get; set; }
	}

	public class MySqlDataGate
	{
		#region Private fields
		private MySqlConnection _con;
		private MySqlTransaction _trn;
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
				this._con = new MySqlConnection(value);
			}
		}
		public MySqlConnection Connection
		{
			get { return this._con; }
		}
		#endregion

		#region Constructors
		public MySqlDataGate()
		{
		}

		public MySqlDataGate(string connectionString)
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
		public MySqlTransaction BeginTransaction()
		{
			if (GlobalParameters.Transaction == null)
			{
				GlobalParameters.MySqlTransaction = this._con.BeginTransaction();
			}
			return GlobalParameters.MySqlTransaction;
		}

		public DataSet GetDataSet(string sqlStatement)
		{
			MySqlCommand sqlCmd = new MySqlCommand(sqlStatement);
			return this.GetDataSet(sqlCmd);
		}

		public DataSet GetDataSet(MySqlCommand sqlStatement)
		{
			DataSet res = null;
			if (Connect())
			{
				res = new DataSet();
				sqlStatement.Connection = this._con;
				sqlStatement.Transaction = GlobalParameters.MySqlTransaction;
				MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlStatement);
				dataAdapter.Fill(res);
			}
			return res;
		}

		public object ExecuteScalar(string sqlStatement)
		{
			if (Connect())
			{
				MySqlCommand sqlCmd = new MySqlCommand(sqlStatement, this._con);
				sqlCmd.Transaction = GlobalParameters.MySqlTransaction;
				return sqlCmd.ExecuteScalar();
			}
			return null;
		}

		public object ExecuteScalar(MySqlCommand sqlCmd)
		{
			if (Connect())
			{
				sqlCmd.Transaction = GlobalParameters.MySqlTransaction;
				return sqlCmd.ExecuteScalar();
			}
			return null;
		}

		public void Execute(string sqlStatement)
		{
			if (Connect())
			{
				MySqlCommand sqlCmd = new MySqlCommand(sqlStatement, this._con);
				sqlCmd.Transaction = GlobalParameters.MySqlTransaction;
				sqlCmd.ExecuteNonQuery();
			}
		}

		public void Execute(MySqlCommand sqlCmd)
		{
			if (Connect())
			{
				sqlCmd.Transaction = GlobalParameters.MySqlTransaction;
				sqlCmd.ExecuteNonQuery();
			}
		}

		public MySqlCommand ExecuteCommand(MySqlCommand sqlCmd)
		{
			if (Connect())
			{
				sqlCmd.Connection = this._con;
				sqlCmd.Transaction = GlobalParameters.MySqlTransaction;
				sqlCmd.ExecuteNonQuery();
			}
			return sqlCmd;
		}
		#endregion
	}
}
