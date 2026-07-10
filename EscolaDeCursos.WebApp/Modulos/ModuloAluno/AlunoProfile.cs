using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;
namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno;

public class AlunoProfile : Profile
{
    public AlunoProfile()
    {
        CreateMap<ListarAlunosDto, ListarAlunosViewModel>();
        CreateMap<CadastrarAlunoViewModel, CadastrarAlunoDto>();
        CreateMap<EditarAlunoViewModel, EditarAlunoDto>();
        CreateMap<DetalhesAlunoDto, EditarAlunoViewModel>();
        CreateMap<DetalhesAlunoDto, ExcluirAlunoViewModel>();
    }
}