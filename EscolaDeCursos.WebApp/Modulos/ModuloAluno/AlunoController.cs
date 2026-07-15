using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using EscolaDeCursos.WebApp.Modulos.ModuloTurma;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno;

public class AlunoController(ServicoAluno servicoAluno, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar(SituacaoAluno? situacao = null, Guid? cursoId = null)
    {
        List<OpcaoCursoDto> cursosDto = servicoAluno.SelecionarCursos();
        List<OpcaoCursoViewModel> cursos = mapeador.Map<List<OpcaoCursoViewModel>>(cursosDto);

        List<ListarAlunosDto> dtos = servicoAluno.Selecionar(situacao, cursoId);

        ListarAlunosPaginaViewModel pagina = new()
        {
            Situacao = situacao,
            CursoId = cursoId,
            Cursos = cursos,
            Alunos = mapeador.Map<List<ListarAlunosViewModel>>(dtos)
        };

        return View(pagina);
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

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesAlunoDto> resultado = servicoAluno.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarAlunoViewModel editarVm = mapeador.Map<EditarAlunoViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarAlunoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarAlunoDto dto = mapeador.Map<EditarAlunoDto>(editarVm);

        Result resultado = servicoAluno.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }


    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesAlunoDto> resultado = servicoAluno.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirAlunoViewModel excluirVm = mapeador.Map<ExcluirAlunoViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirAlunoViewModel excluirVm)
    {
        Result resultado = servicoAluno.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}