using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloCategoria;
using EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso;

public class CursoController(
    ServicoCurso servicoCurso,
    ServicoCategoria servicoCategoria,
    IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarCursosDto> dtos = servicoCurso.SelecionarTodos();
        List<ListarCursosViewModel> listarVms = mapeador.Map<List<ListarCursosViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCursoViewModel cadastrarVm = new(string.Empty, string.Empty, Guid.Empty, 0, 0)
        {
            CategoriasDisponiveis = ObterCategoriasDisponiveis()
        };

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCursoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            cadastrarVm.CategoriasDisponiveis = ObterCategoriasDisponiveis();

            return View(cadastrarVm);
        }

        CadastrarCursoDto dto = mapeador.Map<CadastrarCursoDto>(cadastrarVm);

        Result resultado = servicoCurso.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            cadastrarVm.CategoriasDisponiveis = ObterCategoriasDisponiveis();

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    private List<SelectListItem> ObterCategoriasDisponiveis()
    {
        return servicoCategoria
            .SelecionarTodos()
            .Select(c => new SelectListItem(c.Titulo, c.Id.ToString()))
            .ToList();
    }
}
