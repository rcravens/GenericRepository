using System.IO;
using System.Reflection;

namespace NHibernateImplTests
{
    public static class Helpers
    {
        private const string _relPath = @"DataModel\Database\db.sdf";
        private const string _connectionStringTmpl = @"Data Source={0}";

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

        private static Assembly _resourceAssembly;
        public static Assembly ResourceAssembly
        {
            get
            {
                if(_resourceAssembly==null)
                {
                    _resourceAssembly = Assembly.GetExecutingAssembly();
                }
                return _resourceAssembly;
            }
        }
    }
}