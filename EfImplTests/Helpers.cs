using System.IO;
using System.Reflection;

namespace EfImplTests
{
    public static class Helpers
    {
        private const string _connectionStringTmpl = "metadata=res://*/db2.csdl|res://*/db2.ssdl|res://*/db2.msl;provider=System.Data.SqlServerCe.3.5;provider connection string='Data Source={0}'";
        private const string _relPath = @"DataModel\Database\db2.sdf";

        private static string _connectionString;
        public static string ConnectionString
        {
            get
            {
                if(string.IsNullOrEmpty(_connectionString))
                {

                    string curPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string rootPath = Path.Combine(curPath, "../../../");
                    rootPath = Path.GetFullPath(rootPath);
                    string dbPath = Path.Combine(rootPath, _relPath);
                    dbPath = Path.GetFullPath(dbPath);

                    _connectionString = string.Format(_connectionStringTmpl, dbPath);
                }
                return _connectionString;
            }
        }
    }
}