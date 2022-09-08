
using System.ComponentModel.DataAnnotations.Schema;

namespace APITarefas2.Data;

[Table("Tarefas")]
public record Tarefa(int Id, string Atividade, string Status);
