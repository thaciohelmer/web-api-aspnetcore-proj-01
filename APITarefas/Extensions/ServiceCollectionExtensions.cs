using System.Data.SqlClient;
using static APITarefas2.Data.TarefaContext;

namespace APITarefas2.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<GetConnection>(sp => async() =>
        {
            var connection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();
            return connection;
        });
        return builder;
    }
}
