using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
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

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarAlunoViewModel cadastrarVm = new CadastrarAlunoViewModel(
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarAlunoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarAlunoDto dto = mapeador.Map<CadastrarAlunoDto>(cadastrarVm);

        Result resultado = servicoAluno.Cadastrar(dto);


        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }
}