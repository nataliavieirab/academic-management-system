using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloCurso.ModuloModulo;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using EscolaDeCursos.Infra.Compartilhado.Logging;
using EscolaDeCursos.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using EscolaDeCursos.Infra.Modulos.ModuloCurso.ModuloModulo;
using EscolaDeCursos.Infra.Modulos.ModuloCurso;
using EscolaDeCursos.Infra.Modulos.ModuloCategoria;
using EscolaDeCursos.Infra.Modulos.ModuloAluno;
using EscolaDeCursos.Infra.Modulos.ModuloInstrutor;
using EscolaDeCursos.Infra.Modulos.ModuloMatricula;
using EscolaDeCursos.Infra.Modulos.ModuloTurma;
using Microsoft.AspNetCore.Identity;
namespace EscolaDeCursos.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(
    this IServiceCollection services,
    IConfiguration configuration,
    ILoggingBuilder logging
    )
    {
        // Injeta logs do Serilog
        Log.Logger = SerilogFactory.Create(configuration);

        logging.ClearProviders();

        services.AddSerilog(Log.Logger);

        // Injeta o DbContext do EF
        services.AddDbContext<EscolaDeCursosDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("SqlServerEF");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
        $"A connection string \"SqlServerEF\" não foi encontrada."
        );
            }

            options.UseSqlServer(connectionString, opt =>
    {
        opt.EnableRetryOnFailure(3);
    });
        });

        // Configuração do Usuário no Identity
        services.AddIdentityCore<IdentityUser<Guid>>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<IdentityRole<Guid>>() // Configuração de Cargos/Papéis no Identity
        .AddEntityFrameworkStores<EscolaDeCursosDbContext>() // Integração com EntityFramework
        .AddSignInManager() // Lida com Login, Logout, Registros
        .AddDefaultTokenProviders(); // Lida com geração de Tokens para troca de senhas


        // Injeta os repositórios
        services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
        services.AddScoped<IRepositorioCurso, RepositorioCurso>();
        services.AddScoped<IRepositorioModulo, RepositorioModulo>();

        services.AddScoped<IRepositorioAluno, RepositorioAluno>();
        services.AddScoped<IRepositorioInstrutor, RepositorioInstrutor>();
        services.AddScoped<IRepositorioTurma, RepositorioTurma>();
        services.AddScoped<IRepositorioMatricula, RepositorioMatricula>();

    }
}
