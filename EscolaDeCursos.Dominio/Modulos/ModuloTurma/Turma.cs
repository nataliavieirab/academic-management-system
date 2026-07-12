using EscolaDeCursos.Dominio.Compartilhado;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
// using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;

namespace EscolaDeCursos.Dominio.Modulos.ModuloTurma;


public class Turma : EntidadeBase<Turma>
{
    public string Nome { get; set; } = string.Empty;
    // public Curso? Curso { get; set; } = null;
    public Instrutor? Instrutor { get; set; } = null;
    public List<Aluno> Alunos { get; set; } = new List<Aluno>();
    public int CapacidadeMaxima { get; set; }
    public DateOnly DataInicio { get; set; }
    public DateOnly DataTermino { get; set; }

    public Turma()
    {
    }

    public Turma(string nome, Instrutor? instrutor, int capacidadeMaxima, DateOnly dataInicio, DateOnly dataTermino)
    {
        Nome = nome;
        // Curso = curso;
        Instrutor = instrutor;
        CapacidadeMaxima = capacidadeMaxima;
        DataInicio = dataInicio;
        DataTermino = dataTermino;
        Alunos = new List<Aluno>();
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        ValidarNome(erros);
        // ValidarCurso(erros);
        ValidarInstrutor(erros);
        ValidarCapacidadeMaxima(erros);
        ValidarDataInicio(erros);
        ValidarDataTermino(erros);

        return erros;
    }

    private void ValidarNome(List<string> erros)
    {
        if (Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");
    }

    // private void ValidarCurso(List<string> erros)
    // {
    //     if (Curso == null)
    //         erros.Add("Toda turma deve possuir um curso.");
    // }

    private void ValidarInstrutor(List<string> erros)
    {
        if (Instrutor == null)
            erros.Add("Toda turma deve possuir um instrutor.");
    }

    private void ValidarCapacidadeMaxima(List<string> erros)
    {
        if (CapacidadeMaxima <= 0 || CapacidadeMaxima > 30)
            erros.Add("O campo \"Capacidade Máxima\" deve conter entre 1 e 30 alunos.");
    }

    private void ValidarDataInicio(List<string> erros)
    {
        if (DataInicio < DateOnly.FromDateTime(DateTime.Today))
            erros.Add("O campo \"Data de Início\" não pode ser anterior à data atual.");
    }

    private void ValidarDataTermino(List<string> erros)
    {
        if (DataTermino <= DataInicio)
            erros.Add("O campo \"Data de Término\" deve ser posterior à de início.");
    }

    public override void Atualizar(Turma entidadeAtualizada)
    {
        Turma turmaAtualizada = entidadeAtualizada;

        Nome = turmaAtualizada.Nome;
        // Curso = turmaAtualizada.Curso;
        Instrutor = turmaAtualizada.Instrutor;
        CapacidadeMaxima = turmaAtualizada.CapacidadeMaxima;
        DataInicio = turmaAtualizada.DataInicio;
        DataTermino = turmaAtualizada.DataTermino;
    }
}
