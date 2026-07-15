using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public class TurmaProfile : Profile
{
    public TurmaProfile()
    {
        CreateMap<ListarTurmasDto, ListarTurmasViewModel>();
        CreateMap<CadastrarTurmaViewModel, CadastrarTurmaDto>()
            .ForMember(dest => dest.CursoId, opt => opt.MapFrom(src => src.CursoId!.Value))
            .ForMember(dest => dest.InstrutorId, opt => opt.MapFrom(src => src.InstrutorId!.Value));
        CreateMap<EditarTurmaViewModel, EditarTurmaDto>()
            .ForMember(dest => dest.CursoId, opt => opt.MapFrom(src => src.CursoId!.Value))
            .ForMember(dest => dest.InstrutorId, opt => opt.MapFrom(src => src.InstrutorId!.Value));
        CreateMap<DetalhesTurmaDto, EditarTurmaViewModel>();
        CreateMap<DetalhesTurmaDto, ExcluirTurmaViewModel>();
        CreateMap<ExcluirTurmaViewModel, ExcluirTurmaDto>();

        CreateMap<OpcaoInstrutorDto, OpcaoInstrutorViewModel>();
        CreateMap<OpcaoCursoDto, OpcaoCursoViewModel>();
    }
}
