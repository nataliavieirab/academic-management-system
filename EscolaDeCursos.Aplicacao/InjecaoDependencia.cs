using EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;
using EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace EscolaDeCursos.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration
    )
    {
        services.AddScoped<ServicoAluno>();
        services.AddScoped<ServicoInstrutor>();
        services.AddScoped<ServicoTurma>();
    }
}
