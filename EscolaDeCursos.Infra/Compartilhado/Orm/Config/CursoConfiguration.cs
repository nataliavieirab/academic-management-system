using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Config;

public sealed class CursoConfiguration : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("TBCurso");

        builder.HasKey(c => c.Id)
            .HasName("PK_TBCurso");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Descricao)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Nivel)
            .IsRequired();

        builder.Property(c => c.CargaHoraria)
            .IsRequired();

        builder.HasIndex(c => c.Titulo)
            .IsUnique()
            .HasDatabaseName("UQ_TBCurso_Titulo");

        builder.HasOne(c => c.Categoria)
            .WithMany()
            .HasForeignKey(c => c.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Modulos)
            .WithOne(m => m.Curso)
            .HasForeignKey(m => m.CursoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
