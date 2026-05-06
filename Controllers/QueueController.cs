using Microsoft.AspNetCore.Mvc;
using queue_management.Models;
using queue_management.Services;

namespace queue_management.Controllers;

[ApiController]
[Route("api/queue")]
public class QueueController : ControllerBase
{
    private readonly TurnService _turnService;

    public QueueController(TurnService turnService)
    {
        _turnService = turnService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var turns = (_turnService.GetQueue().Data ?? Enumerable.Empty<Turn>()).ToList();
        var current = turns.FirstOrDefault(t => t.Status == TurnStatus.InService);
        var waiting = turns
            .Where(t => t.Status == TurnStatus.Pending || t.Status == TurnStatus.Waiting)
            .OrderBy(t => t.Status == TurnStatus.Waiting ? 0 : 1)
            .ThenBy(t => t.CreatedAt)
            .Select(t => new
            {
                t.TicketCode,
                Status = t.Status.ToString(),
                User = t.User == null ? null : new
                {
                    t.User.FullName,
                    t.User.DocumentNumber
                }
            })
            .ToList();

        return Ok(new
        {
            Current = current == null ? null : new
            {
                current.TicketCode,
                Status = current.Status.ToString(),
                User = current.User == null ? null : new
                {
                    current.User.FullName,
                    current.User.DocumentNumber
                }
            },
            Waiting = waiting,
            TotalWaiting = waiting.Count
        });
    }
}
