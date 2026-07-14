using EscolaDeCursos.Dominio.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;

namespace EscolaDeCursos.Dominio.Modulos.ModuloMatricula;

public class Matricula : EntidadeBase<Matricula>
{
    public Guid AlunoId { get; set; }
    public Aluno? Aluno { get; set; }

    public Guid TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public DateOnly Data { get; set; }
    public SituacaoAluno Situacao { get; set; }

    public Matricula()
    {
    }

    public Matricula(Aluno aluno, Turma turma, DateOnly data, SituacaoAluno situacao)
    {
        Aluno = aluno;
        AlunoId = aluno.Id;
        Turma = turma;
        TurmaId = turma.Id;
        Data = data;
        Situacao = situacao;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (Aluno is null && AlunoId == Guid.Empty)
            erros.Add("O campo \"Aluno\" é obrigatório.");

        if (Turma is null && TurmaId == Guid.Empty)
            erros.Add("O campo \"Turma\" é obrigatório.");

        if (!Enum.IsDefined(Situacao))
            erros.Add("O campo \"Situação\" é inválido.");

        return erros;
    }

    public override void Atualizar(Matricula entidadeAtualizada)
    {
        Situacao = entidadeAtualizada.Situacao;
    }
}
