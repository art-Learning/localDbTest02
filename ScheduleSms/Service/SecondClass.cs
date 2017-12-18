using Dapper;
using ScheduleSms.Database;

namespace ScheduleSms.Service
{
    internal class SecondClass
    {
        private IDatabaseConnectionFactory DatabaseConnectionFactory { get; }

        public SecondClass(IDatabaseConnectionFactory connectionFactory)
        {
            DatabaseConnectionFactory = connectionFactory;
        }

        public void DoSomething()
        {
            const string sqlCmd = " Update Test set SMS_ENABLE_FG='0'";
            using (var sql = DatabaseConnectionFactory.Create())
            {
                sql.Execute(sqlCmd);
            }
        }
    }
}