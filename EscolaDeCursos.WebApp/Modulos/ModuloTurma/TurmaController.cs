using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
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

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarTurmaViewModel cadastrarVm = new CadastrarTurmaViewModel(
            string.Empty,
            Guid.Empty,
            0,
            DateOnly.FromDateTime(DateTime.Today),
            DateOnly.FromDateTime(DateTime.Today.AddMonths(1)),
            SelecionarInstrutor()
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarTurmaViewModel cadastrarVm)
    {

        // if (!ModelState.IsValid)
        //     return View(cadastrarVm with { Curso = SelecionarCurso() });
        if (!ModelState.IsValid)
            return View(cadastrarVm with { Instrutores = SelecionarInstrutor() });

        CadastrarTurmaDto dto = mapeador.Map<CadastrarTurmaDto>(cadastrarVm);
        Result resultado = servicoTurma.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm with { Instrutores = SelecionarInstrutor() });
        }

        return RedirectToAction(nameof(Listar));
    }


    // private List<OpcaoCursoViewModel> SelecionarCurso()
    // {
    //     List<OpcaoCursoDto> dtos = servicoTurma.SelecionarCurso();

    //     return mapeador.Map<List<OpcaoCursoViewModel>>(dtos);
    // }

    private List<OpcaoInstrutorViewModel> SelecionarInstrutor()
    {
        try
        {
            List<OpcaoInstrutorDto> dtos = servicoTurma.SelecionarInstrutor();
            return mapeador.Map<List<OpcaoInstrutorViewModel>>(dtos);
        }
        catch
        {
            return new List<OpcaoInstrutorViewModel>();
        }
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesTurmaDto> resultado = servicoTurma.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = "Turma não encontrada.";
            return RedirectToAction(nameof(Listar));
        }

        DetalhesTurmaDto dto = resultado.Value;
        EditarTurmaViewModel editarVm = new EditarTurmaViewModel(
            dto.Id,
            dto.Nome,
            dto.InstrutorId,
            dto.CapacidadeMaxima,
            dto.DataInicio,
            dto.DataTermino,
            SelecionarInstrutor()
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarTurmaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm with { Instrutores = SelecionarInstrutor() });

        EditarTurmaDto dto = mapeador.Map<EditarTurmaDto>(editarVm);
        Result resultado = servicoTurma.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(editarVm with { Instrutores = SelecionarInstrutor() });
        }

        TempData["MensagemSucesso"] = "Turma atualizada com sucesso.";
        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesTurmaDto> resultado = servicoTurma.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = "Turma não encontrada.";
            return RedirectToAction(nameof(Listar));
        }

        DetalhesTurmaDto dto = resultado.Value;
        ExcluirTurmaViewModel excluirVm = mapeador.Map<ExcluirTurmaViewModel>(dto);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirTurmaViewModel excluirVm)
    {
        ExcluirTurmaDto dto = mapeador.Map<ExcluirTurmaDto>(excluirVm);
        Result resultado = servicoTurma.Excluir(dto.Id);

        if (resultado.IsFailed)
        {
            TempData["MensagemErro"] = resultado.Errors.First().Message;
            return RedirectToAction(nameof(Listar));
        }

        TempData["MensagemSucesso"] = "Turma excluída com sucesso.";
        return RedirectToAction(nameof(Listar));
    }

}
