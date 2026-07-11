using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public class TurmaController(ServicoTurma servicoTurma, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarTurmasDto> dtos = servicoTurma.SelecionarTodos();
        List<ListarTurmasViewModel> listarVms = mapeador.Map<List<ListarTurmasViewModel>>(dtos);

        return View(listarVms);
    }

}
