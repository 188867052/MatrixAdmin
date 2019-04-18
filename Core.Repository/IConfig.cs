using System.Data;
using System.Data.SqlClient;
using Core.Repository.Dependency;

namespace Core.Repository
{
	public interface IConfig : IDependency
	{
		bool IsAtCompany { get; }
		string Url { get; }

		string XmlPath { get; }


		DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters);


		DataSet ExecuteDataSet(string sql, params SqlParameter[] parameter);
	}
}