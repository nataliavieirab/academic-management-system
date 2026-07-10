using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;
namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor;

public class InstrutorProfile : Profile
{
    public InstrutorProfile()
    {
        CreateMap<ListarInstrutoresDto, ListarInstrutoresViewModel>();
        CreateMap<CadastrarInstrutorViewModel, CadastrarInstrutorDto>();
        CreateMap<EditarInstrutorViewModel, EditarInstrutorDto>();
        CreateMap<DetalhesInstrutorDto, EditarInstrutorViewModel>();
        CreateMap<DetalhesInstrutorDto, ExcluirInstrutorViewModel>();
    }
}