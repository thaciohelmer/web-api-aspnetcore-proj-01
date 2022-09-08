using APITarefas2.Data;
using Dapper.Contrib.Extensions;
using System.Runtime.CompilerServices;
using static APITarefas2.Data.TarefaContext;

namespace APITarefas2.Endpoints;

public static class TarefasEndpoints
{
    public static void MapTarefasEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => $"Bem Vindo a API Tarefas - {DateTime.Now}");

        app.MapGet("/tarefas", async (GetConnection connection) =>
        {
            using var con = await connection();

            var tarefas = con.GetAll<Tarefa>().ToList();

            if (tarefas is null) return Results.NotFound("Nenhuma tarefa encontrada.");

            return Results.Ok(tarefas);
        });

        app.MapGet("/tarefas/{id}", async(GetConnection connection, int id) =>
        {
            using var con = await connection();

            var tarefa = con.Get<Tarefa>(id);

            if (tarefa is null) return Results.NotFound("Tarefa não encontrada");

            return Results.Ok(tarefa);
        });

        app.MapPost("/tarefas", async (GetConnection connection, Tarefa tarefa) =>
        {
            using var con = await connection();

            var novaTarefa = con.Insert(tarefa);

            return Results.Created($"/tarefas/{novaTarefa}", tarefa);
        });

        app.MapPut("/tarefas/{id}", async (GetConnection connection, int id, Tarefa tarefa) => 
        {
            using var con = await connection();

            if (id != tarefa.Id) return Results.BadRequest("Erro ao tentar alterar a tarefa.");

            var alteraTarefa = con.Update(tarefa);

            return Results.Ok($"A tarefa {tarefa.Atividade} foi atualizada com sucesso !");
        });

        app.MapDelete("/tarefas/{id}", async (GetConnection connection, int id) =>
        {
            using var con = await connection();

            var deleteTarefa = con.Get<Tarefa>(id);

            if (deleteTarefa is null) return Results.NotFound("Tarefa não encontrada");

            con.Delete(deleteTarefa);

            return Results.Ok($"A tarefa {deleteTarefa.Atividade} foi excluída com sucesso !");
        });
    }
}
