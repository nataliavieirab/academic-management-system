using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor;

public class InstrutorController(ServicoInstrutor servicoInstrutor, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarInstrutoresDto> dtos = servicoInstrutor.SelecionarTodos();
        List<ListarInstrutoresViewModel> listarVms = mapeador.Map<List<ListarInstrutoresViewModel>>(dtos);

        return View(listarVms);
    }
}