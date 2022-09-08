using System.Data;

namespace APITarefas2.Data;

public class TarefaContext
{
    public delegate Task<IDbConnection> GetConnection();
}
