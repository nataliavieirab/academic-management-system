using EscolaDeCursos.Dominio.Modulos.ModuloCurso.ModuloModulo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Config;

public sealed class ModuloConfiguration : IEntityTypeConfiguration<Modulo>
{
    public void Configure(EntityTypeBuilder<Modulo> builder)
    {
        builder.ToTable("TBModulo");

        builder.HasKey(m => m.Id)
            .HasName("PK_TBModulo");

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.Property(m => m.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Descricao)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Ordem)
            .IsRequired();

        builder.Property(m => m.Duracao)
            .IsRequired();

        builder.HasIndex(m => new { m.CursoId, m.Ordem })
            .IsUnique()
            .HasDatabaseName("UQ_TBModulo_Curso_Ordem");
    }
}
