using System.Reflection;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso.ModuloModulo;
using Microsoft.EntityFrameworkCore;
namespace EscolaDeCursos.Infra.Compartilhado.Orm;

public sealed class EscolaDeCursosDbContext(
    DbContextOptions<EscolaDeCursosDbContext> options) : DbContext(options)
{
    public DbSet<Curso> Cursos => Set<Curso>();
    // public DbSet<Instrutor> Instrutores => Set<Instrutor>();
    public DbSet<Modulo> ModulosDoCurso => Set<Modulo>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    // public DbSet<Turma> Turmas => Set<Turma>();
    // public DbSet<Matricula> Matriculas => Set<Matricula>();
    // public DbSet<Aluno> Alunos => Set<Aluno>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assembly assembly = typeof(EscolaDeCursosDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
