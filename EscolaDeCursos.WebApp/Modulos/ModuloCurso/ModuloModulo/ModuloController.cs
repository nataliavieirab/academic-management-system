using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;
using EscolaDeCursos.Aplicacao.Modulos.ModuloCurso.ModuloModulo;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.ModuloModulo;

public class ModuloController(
    ServicoModulo servicoModulo,
    ServicoCurso servicoCurso,
    IMapper mapeador) : Controller
{

    [HttpGet]
    public ActionResult Listar(Guid cursoId)
    {
        List<ListarModulosDto> dtos = servicoModulo.SelecionarPorCurso(cursoId);
        List<ListarModulosViewModel> vms = mapeador.Map<List<ListarModulosViewModel>>(dtos);

        ViewBag.CursoId = cursoId;
        ViewBag.CursoTitulo = servicoCurso.SelecionarPorId(cursoId).ValueOrDefault?.Titulo;

        return View(vms);
    }

    [HttpGet]
    public ActionResult Cadastrar(Guid id)
    {
        Result<DetalhesCursoDto> resultado = servicoCurso.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar), new { cursoId = id });
        }

        CadastrarModuloViewModel cadastrarVm = new(string.Empty, string.Empty, 0, 0, id);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarModuloViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarModuloDto dto = mapeador.Map<CadastrarModuloDto>(cadastrarVm);

        Result resultado = servicoModulo.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar), new { cursoId = cadastrarVm.CursoId });
    }
}
