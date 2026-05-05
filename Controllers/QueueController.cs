using Microsoft.AspNetCore.Mvc;
using queue_management.Models;
using queue_management.Services;

namespace queue_management.Controllers;

/// <summary>
/// QueueController — Parte 4: Sala de Espera (consultas de cola)
/// Responsabilidad: Proveer la información de la cola en tiempo real
/// para alimentar la pantalla de sala de espera y otros consumidores.
///
/// Endpoints:
///   GET /api/queue                  → Cola activa (Waiting + InService), orden FIFO
///   GET /api/queue/waiting          → Solo turnos en espera
///   GET /api/queue/status/{status}  → Turnos filtrados por estado
///   GET /api/queue/summary          → Resumen de contadores por estado
/// </summary>
[ApiController]
[Route("api/queue")]
public class QueueController : ControllerBase
{
    private readonly TurnService _turnService;

    public QueueController(TurnService turnService)
    {
        _turnService = turnService;
    }

    /// <summary>
    /// Retorna la cola activa: turnos en estado Waiting e InService,
    /// ordenados por CreatedAt (FIFO). Este es el endpoint principal
    /// que consume la pantalla de sala de espera.
    ///
    /// Respuesta de ejemplo:
    /// {
    ///   "current": { "ticketCode": "A-003", "user": { "fullName": "Ana Torres" }, ... },
    ///   "waiting": [ { "ticketCode": "A-004", ... }, ... ],
    ///   "totalWaiting": 4
    /// }
    /// </summary>
    [HttpGet]
    public IActionResult GetActiveQueue()
    {
        var result = _turnService.GetQueue();

        if (!result.Success)
            return StatusCode(500, new { result.Success, result.Message });

        var all      = result.Data!.ToList();
        var current  = all.FirstOrDefault(t => t.Status == TurnStatus.InService);
        var waiting  = all.Where(t => t.Status == TurnStatus.Waiting).ToList();

        return Ok(new
        {
            Success = true,
            Current      = current  == null ? null : MapTurnToDto(current),
            Waiting      = waiting.Select(MapTurnToDto),
            TotalWaiting = waiting.Count
        });
    }

    /// <summary>
    /// Retorna únicamente los turnos en estado Waiting (en espera),
    /// ordenados por tiempo de llegada. Útil para listas parciales
    /// en la sala de espera cuando no se necesita el turno actual.
    /// </summary>
    [HttpGet("waiting")]
    public IActionResult GetWaiting()
    {
        var result = _turnService.GetTurnsByStatus(TurnStatus.Waiting);
        return Ok(new
        {
            Success = true,
            Turns   = result.Data!.Select(MapTurnToDto),
            Total   = result.Data!.Count()
        });
    }

    /// <summary>
    /// Retorna turnos filtrados por estado.
    /// Estados válidos: Pending, Waiting, InService, Finished
    ///
    /// Ejemplo: GET /api/queue/status/Finished
    /// Retorna el historial de turnos atendidos (útil para estadísticas).
    /// </summary>
    [HttpGet("status/{status}")]
    public IActionResult GetByStatus(string status)
    {
        if (!Enum.TryParse<TurnStatus>(status, ignoreCase: true, out var parsed))
            return BadRequest(new
            {
                Success = false,
                Message = $"Estado '{status}' no válido. Use: Pending, Waiting, InService, Finished."
            });

        var result = _turnService.GetTurnsByStatus(parsed);

        return Ok(new
        {
            Success     = true,
            Status      = parsed.ToString(),
            Turns       = result.Data!.Select(MapTurnToDto),
            Total       = result.Data!.Count()
        });
    }

    /// <summary>
    /// Resumen de contadores por cada estado del sistema.
    /// Ideal para los badges/estadísticas del panel del asesor
    /// sin necesidad de cargar todos los datos de cada turno.
    ///
    /// Respuesta de ejemplo:
    /// { "pending": 2, "waiting": 5, "inService": 1, "finished": 12 }
    /// </summary>
    [HttpGet("summary")]
    public IActionResult GetSummary()
    {
        var pending   = _turnService.GetTurnsByStatus(TurnStatus.Pending).Data!;
        var waiting   = _turnService.GetTurnsByStatus(TurnStatus.Waiting).Data!;
        var inService = _turnService.GetTurnsByStatus(TurnStatus.InService).Data!;
        var finished  = _turnService.GetTurnsByStatus(TurnStatus.Finished).Data!;

        return Ok(new
        {
            Success   = true,
            Pending   = pending.Count(),
            Waiting   = waiting.Count(),
            InService = inService.Count(),
            Finished  = finished.Count(),
            Total     = pending.Count() + waiting.Count() + inService.Count() + finished.Count()
        });
    }

    // ─── Helpers ──────────────────────────────────────────────────────────────

    private static object MapTurnToDto(Turn t) => new
    {
        t.Id,
        t.TicketCode,
        Status    = t.Status.ToString(),
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
