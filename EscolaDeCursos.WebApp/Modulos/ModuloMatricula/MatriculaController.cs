using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula;

public class MatriculaController(
    ServicoMatricula servicoMatricula,
    ServicoTurma servicoTurma,
    IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar(Guid turmaId, SituacaoAluno? situacao = null)
    {
        Result<DetalhesTurmaDto> resultadoTurma = servicoTurma.SelecionarPorId(turmaId);

        if (resultadoTurma.IsFailed)
        {
            TempData["MensagemErro"] = "Turma não encontrada.";
            return RedirectToAction("Listar", "Turma");
        }

        List<ListarMatriculasDto> dtos = servicoMatricula.SelecionarPorTurma(turmaId, situacao);

        ListarMatriculasPaginaViewModel pagina = new()
        {
            TurmaId = turmaId,
            TurmaNome = resultadoTurma.Value.Nome,
            Situacao = situacao,
            Matriculas = mapeador.Map<List<ListarMatriculasViewModel>>(dtos)
        };

        return View(pagina);
    }

    [HttpGet]
    public ActionResult Cadastrar(Guid id)
    {
        Result<DetalhesTurmaDto> resultadoTurma = servicoTurma.SelecionarPorId(id);

        if (resultadoTurma.IsFailed)
        {
            TempData["MensagemErro"] = "Turma não encontrada.";
            return RedirectToAction("Listar", "Turma");
        }

        CadastrarMatriculaViewModel cadastrarVm = new(
            null,
            id,
            resultadoTurma.Value.Nome,
            SelecionarAlunos()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMatriculaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            return View(cadastrarVm with
            {
                Alunos = SelecionarAlunos(),
                TurmaNome = ObterNomeTurma(cadastrarVm.TurmaId)
            });
        }

        CadastrarMatriculaDto dto = mapeador.Map<CadastrarMatriculaDto>(cadastrarVm);
        Result resultado = servicoMatricula.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with
            {
                Alunos = SelecionarAlunos(),
                TurmaNome = ObterNomeTurma(cadastrarVm.TurmaId)
            });
        }

        TempData["MensagemSucesso"] = "Matrícula realizada com sucesso.";
        return RedirectToAction(nameof(Listar), new { turmaId = cadastrarVm.TurmaId });
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesMatriculaDto> resultado = servicoMatricula.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = "Matrícula não encontrada.";
            return RedirectToAction("Listar", "Turma");
        }

        EditarMatriculaViewModel editarVm = mapeador.Map<EditarMatriculaViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarMatriculaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarMatriculaDto dto = mapeador.Map<EditarMatriculaDto>(editarVm);
        Result resultado = servicoMatricula.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(editarVm);
        }

        TempData["MensagemSucesso"] = "Situação da matrícula atualizada com sucesso.";
        return RedirectToAction(nameof(Listar), new { turmaId = editarVm.TurmaId });
    }

    private List<OpcaoAlunoViewModel> SelecionarAlunos()
    {
        try
        {
            List<OpcaoAlunoDto> dtos = servicoMatricula.SelecionarAlunos();
            return mapeador.Map<List<OpcaoAlunoViewModel>>(dtos);
        }
        catch
        {
            return new List<OpcaoAlunoViewModel>();
        }
    }

    private string ObterNomeTurma(Guid turmaId)
    {
        Result<DetalhesTurmaDto> resultado = servicoTurma.SelecionarPorId(turmaId);
        return resultado.IsSuccess ? resultado.Value.Nome : string.Empty;
    }
}
