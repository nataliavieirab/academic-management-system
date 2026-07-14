using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Config;

public sealed class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("TBMatricula");

        builder.HasKey(m => m.Id)
            .HasName("PK_TBMatricula");

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.Data)
            .IsRequired();

        builder.Property(m => m.Situacao)
            .IsRequired();

        builder.HasOne(m => m.Aluno)
            .WithMany()
            .HasForeignKey(m => m.AlunoId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(m => m.Turma)
            .WithMany()
            .HasForeignKey(m => m.TurmaId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(m => new { m.AlunoId, m.TurmaId })
            .IsUnique()
            .HasDatabaseName("UQ_TBMatricula_Aluno_Turma");
    }
}
