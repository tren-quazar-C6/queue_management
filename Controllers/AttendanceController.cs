using Microsoft.AspNetCore.Mvc;
using queue_management.Models;
using queue_management.Responses;
using queue_management.Services;

namespace queue_management.Controllers;

/// <summary>
/// AttendanceController — Parte 4: Flujo de Atención
/// Responsabilidad: Gestionar el ciclo de vida del turno desde que es llamado
/// hasta que se finaliza. Expone endpoints JSON (API REST) para ser consumidos
/// por el panel del asesor u otros clientes.
///
/// Endpoints:
///   POST /api/attendance/call-next       → Llama al siguiente turno en cola (FIFO)
///   POST /api/attendance/finish/{id}     → Finaliza el turno en atención
///   POST /api/attendance/absent/{id}     → Marca el turno como ausente
///   GET  /api/attendance/current         → Retorna el turno actualmente en atención
/// </summary>
[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly TurnService _turnService;

    public AttendanceController(TurnService turnService)
    {
        _turnService = turnService;
    }

    /// <summary>
    /// Llama al siguiente turno en espera (orden FIFO por CreatedAt).
    /// Regla: Solo puede haber un turno "InService" a la vez.
    /// Si ya hay uno en atención, retorna error 409.
    /// </summary>
    [HttpPost("call-next")]
    public IActionResult CallNext()
    {
        var result = _turnService.CallNext();

        if (!result.Success)
            return Conflict(new { result.Success, result.Message });

        return Ok(new
        {
            result.Success,
            result.Message,
            turn = MapTurnToDto(result.Data!)
        });
    }

    /// <summary>
    /// Finaliza el turno actualmente en atención.
    /// Permite registrar un comentario sobre la atención prestada.
    /// Solo se puede finalizar un turno con estado "InService".
    /// </summary>
    [HttpPost("finish/{id:int}")]
    public IActionResult Finish(int id, [FromBody] FinishTurnRequest request)
    {
        var result = _turnService.FinishTurn(id, request.Comment);

        if (!result.Success)
            return BadRequest(new { result.Success, result.Message });

        return Ok(new
        {
            result.Success,
            result.Message,
            turn = MapTurnToDto(result.Data!)
        });
    }

    /// <summary>
    /// Marca el turno como ausente (usuario no se presentó).
    /// El turno pasa a estado Finished con comentario de ausencia.
    /// Permite al asesor avanzar sin bloquear la cola.
    /// </summary>
    [HttpPost("absent/{id:int}")]
    public IActionResult MarkAbsent(int id, [FromBody] AbsentTurnRequest? request)
    {
        var comment = request?.Comment ?? "Usuario ausente";
        var result = _turnService.MarkAbsent(id, comment);

        if (!result.Success)
            return BadRequest(new { result.Success, result.Message });

        return Ok(new
        {
            result.Success,
            result.Message,
            turn = MapTurnToDto(result.Data!)
        });
    }

    /// <summary>
    /// Retorna el turno que está actualmente siendo atendido.
    /// Útil para sincronizar la pantalla de sala de espera y el panel del asesor.
    /// Retorna 404 si no hay ningún turno en atención en este momento.
    /// </summary>
    [HttpGet("current")]
    public IActionResult GetCurrentTurn()
    {
        var result = _turnService.GetTurnsByStatus(TurnStatus.InService);

        var current = result.Data?.FirstOrDefault();

        if (current == null)
            return NotFound(new { Success = false, Message = "No hay turno en atención en este momento." });

        return Ok(new
        {
            Success = true,
            turn = MapTurnToDto(current)
        });
    }

    // ─── Helpers ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Proyecta un Turn a un DTO plano para las respuestas JSON.
    /// Evita ciclos de serialización y expone solo los campos necesarios.
    /// </summary>
    private static object MapTurnToDto(Turn t) => new
    {
        t.Id,
        t.TicketCode,
        Status       = t.Status.ToString(),
        t.CreatedAt,
        t.Comment,
        User = t.User == null ? null : new
        {
            t.User.Id,
            t.User.FullName,
            t.User.DocumentNumber
        }
    };
}

// ─── Request DTOs ─────────────────────────────────────────────────────────────

public record FinishTurnRequest(string? Comment);
public record AbsentTurnRequest(string? Comment);
