using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Core.Repository
{
	public class Config : IConfig
	{
		public string Url
		{
			get
			{
				return this.IsAtCompany ? "http://hcheng1:90" : "http://localhost:90";
			}
		}

		public string XmlPath
		{
			get
			{
				return this.IsAtCompany ? CompanyXmlPath : HomeXmlPath;
			}
		}

		public bool IsAtCompany
		{
			get
			{
				return File.Exists(CompanyXmlPath);
			}
		}

		private IList<string> DataBaseList
		{
			get
			{
				return this.IsAtCompany ? new List<string> { "TF_100000", "TF_Master_Dev" } : new List<string> { "AspNetCore", "master" };
			}
		}

		private string ConnectionStringFormat
		{
			get
			{
				return this.IsAtCompany ? "Data Source=HCHENG1;Initial Catalog={0};Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True" :
				 "Data Source=.;Initial Catalog={0};Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True";
			}
		}

		private string CompanyXmlPath
		{
			get
			{
				return @"C:\SVN\Framework\Branches\Staging\CodeSmithTemplates\BOLTemplates\AT_BOL_Map.xml";
			}
		}

		private string HomeXmlPath
		{
			get
			{
				return @"C:\Users\54215\source\repos\WebApi\WebApi\AT_BOL_Map.xml";
			}
		}

		public DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
		{
			IList<DataSet> dataSetList = ExecuteDataSetList(sql, parameters);
			DataTable dataTable = null;
			foreach (DataSet dataSet in dataSetList)
			{
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					if (dataTable == null)
					{
						dataTable = dataSet.Tables[0].Clone();
					}
					foreach (DataRow dataRow in dataSet.Tables[0].Rows)
					{
						dataTable.ImportRow(dataRow);
					}
				}
			}

			return dataTable;
		}

		public DataSet ExecuteDataSet(string sql, params SqlParameter[] parameter)
		{
			DataSet ds = new DataSet();
			try
			{
				string connectionString = string.Format(ConnectionStringFormat, DataBaseList[0]);
				SqlConnection sqlConnection = new SqlConnection(connectionString);
				SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
				SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCommand);
				sqlAdp.Fill(ds);
			}
			catch
			{
				string connectionString = string.Format(ConnectionStringFormat, DataBaseList[1]);
				SqlConnection sqlConnection = new SqlConnection(connectionString);
				SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
				SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCommand);
				sqlAdp.Fill(ds);
			}

			return ds;
		}

		private IList<DataSet> ExecuteDataSetList(string sql, params SqlParameter[] parameter)
		{
			IList<DataSet> dataSetList = new List<DataSet>();
			foreach (string dataBase in DataBaseList)
			{
				string connectionString = string.Format(ConnectionStringFormat, dataBase);
				using (SqlConnection sqlConnection = new SqlConnection(connectionString))
				{
					SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
					SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCommand);
					DataSet ds = new DataSet();
					sqlAdp.Fill(ds);
					dataSetList.Add(ds);
				}
			}
			return dataSetList;
		}
	}
}
