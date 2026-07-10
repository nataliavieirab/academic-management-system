using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Config;

public sealed class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("TBAluno");

        builder.HasKey(a => a.Id)
            .HasName("PK_TBAluno");

        builder.Property(a => a.Id)
            .ValueGeneratedNever();

        builder.Property(a => a.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Cpf)
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(a => a.Telefone)
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(a => a.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Endereco)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(a => a.Cpf)
            .IsUnique()
            .HasDatabaseName("UQ_TBAluno_Cpf");

        builder.HasIndex(a => a.Email)
            .IsUnique()
            .HasDatabaseName("UQ_TBAluno_Email");
    }

}