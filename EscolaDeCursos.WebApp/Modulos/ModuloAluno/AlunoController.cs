using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;
using Microsoft.AspNetCore.Mvc;
namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno;

public class AlunoController(ServicoAluno servicoAluno, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarAlunosDto> dtos = servicoAluno.SelecionarTodos();
        List<ListarAlunosViewModel> listarVms = mapeador.Map<List<ListarAlunosViewModel>>(dtos);

        return View(listarVms);
    }
}