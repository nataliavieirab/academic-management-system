using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Config;

public sealed class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("TBTurma");

        builder.HasKey(t => t.Id)
            .HasName("PK_TBTurma");

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        builder.Property(t => t.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.CapacidadeMaxima)
            .IsRequired();

        builder.Property(t => t.DataInicio)
            .IsRequired();

        builder.Property(t => t.DataTermino)
            .IsRequired();


        // builder.HasOne(t => t.Curso)
        //     .WithMany()
        //     .HasForeignKey("CursoId")
        //     .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(t => t.Instrutor)
            .WithMany()
            .HasForeignKey("InstrutorId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Alunos)
            .WithMany()
            .UsingEntity(j => j.ToTable("TBTurmaAluno"));
    }
}
