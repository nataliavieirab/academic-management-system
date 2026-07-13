using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloCurso.ModuloModulo;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.ModuloModulo;

public class ModuloProfile : Profile
{
    public ModuloProfile()
    {
        CreateMap<ListarModulosDto, ListarModulosViewModel>();
        CreateMap<CadastrarModuloViewModel, CadastrarModuloDto>();
        CreateMap<EditarModuloViewModel, EditarModuloDto>();
        CreateMap<DetalhesModuloDto, EditarModuloViewModel>();
        CreateMap<DetalhesModuloDto, ExcluirModuloViewModel>();
    }
}
