using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Config;

public sealed class InstrutorConfiguration : IEntityTypeConfiguration<Instrutor>
{
    public void Configure(EntityTypeBuilder<Instrutor> builder)
    {
        builder.ToTable("TBInstrutor");

        builder.HasKey(i => i.Id)
            .HasName("PK_TBInstrutor");

        builder.Property(i => i.Id)
            .ValueGeneratedNever();

        builder.Property(i => i.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.Cpf)
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(i => i.Telefone)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(i => i.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(i => i.Cpf)
            .IsUnique()
            .HasDatabaseName("UQ_TBInstrutor_Cpf");

        builder.HasIndex(i => i.Email)
            .IsUnique()
            .HasDatabaseName("UQ_TBInstrutor_Email");
    }

}