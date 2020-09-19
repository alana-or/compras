using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Compras.API.Repository.DbContexts
{
    public static class DbContextExtensions
    {
        public async static Task ExecuteProcNonQueryAsync(this DbContext context,
            string storedProcedureName,
            int timeOut = 30,
            params (string name, object value)[] parameters)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandTimeout = timeOut;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;

                foreach (var (name, value) in parameters)
                {
                    var p = command.CreateParameter();
                    p.ParameterName = name;
                    p.Value = value;
                    command.Parameters.Add(p);
                }

                command.Connection.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public static async Task SaveChangesWithIdentityAsync(this DbContext context,
            string schema,
            string table)
        {
            context.Database.OpenConnection();
            try
            {
                var scriptOn = $"SET IDENTITY_INSERT [{schema}].[{table}] ON";
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
                context.Database.ExecuteSqlRaw(scriptOn);
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
                await context.SaveChangesAsync();
                var scriptOff = $"SET IDENTITY_INSERT [{schema}].[{table}] OFF";
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
                context.Database.ExecuteSqlRaw(scriptOff);
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        public static async Task TruncateTable(this DbContext context,
           string schema,
           string table)
        {
            context.Database.OpenConnection();
            try
            {
                var scriptOn = $"TRUNCATE TABLE [{schema}].[{table}]";
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
                context.Database.ExecuteSqlRaw(scriptOn);
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
                await context.SaveChangesAsync();
            }
            finally
            {
                context.Database.CloseConnection();
            }
        }
    }
}
