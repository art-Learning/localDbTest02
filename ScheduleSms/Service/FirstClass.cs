using Dapper;
using ScheduleSms.Database;

namespace ScheduleSms.Service
{
    internal class FirstClass
    {
        private readonly SecondClass _secondClass;
        private IDatabaseConnectionFactory DatabaseConnectionFactory { get; }

        public FirstClass(IDatabaseConnectionFactory connectionFactory)
        {
            _secondClass = new SecondClass(connectionFactory);
            DatabaseConnectionFactory = connectionFactory;
        }

        public bool DoSomething()
        {
            var sqlCmd = GetSqlCmd();
            bool result;
            using (var sql = DatabaseConnectionFactory.Create())
            {
                result = sql.QuerySingle<bool>(sqlCmd);
            }
            _secondClass.DoSomething();

            return result;
        }

        private static string GetSqlCmd()
        {
            return "select top 1 SMS_ENABLE_FG from Test";
        }
    }
}