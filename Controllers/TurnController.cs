using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using queue_management.Hubs;
using queue_management.Models;
using queue_management.Services;

namespace queue_management.Controllers;

public class TurnController : Controller
{
    private readonly TurnService _turnService;
    private readonly UserService _userService;

    // ← NUEVO: IHubContext<QueueHub> permite que el CONTROLADOR le hable
    // a los clientes de SignalR desde fuera del Hub (en un controller MVC).
    // Es la forma estándar de notificar desde código de servidor.
    private readonly IHubContext<QueueHub> _hub;

    public TurnController(
        TurnService turnService,
        UserService userService,
        IHubContext<QueueHub> hub)
    {
        _turnService = turnService;
        _userService = userService;
        _hub = hub;
    }

    public IActionResult Kiosk() => View();

    [HttpPost]
    public async Task<IActionResult> RequestTurn(string fullName, string documentNumber)
    {
        var userResult = _userService.GetUserByDocument(documentNumber);

        if (!userResult.Success)
        {
            var newUser = _userService.SaveUser(new User { FullName = fullName, DocumentNumber = documentNumber });
            if (!newUser.Success)
            {
                TempData["message"] = newUser.Message;
                return RedirectToAction("Kiosk");
            }
            userResult = newUser;
        }

        var result = _turnService.SaveTurn(userResult.Data!.Id);
        TempData["message"] = result.Message;

        if (result.Success)
        {
            // ← NUEVO: notifica a todos los grupos que la cola cambió.
            // Clients.All envía a TODOS los conectados al hub.
            // "QueueUpdated" es el nombre del evento que escucha el JS cliente.
            await _hub.Clients.All.SendAsync("QueueUpdated");

            TempData["ticket"] = result.Data!.TicketCode;
            return RedirectToAction("Ticket");
        }

        return RedirectToAction("Kiosk");
    }

    public IActionResult Ticket() => View();

    public IActionResult WaitingRoom()
    {
        var response = _turnService.GetQueue();
        return View(response.Data);
    }

    public IActionResult Advisor()
    {
        ViewBag.Pending   = _turnService.GetTurnsByStatus(TurnStatus.Pending).Data;
        ViewBag.Waiting   = _turnService.GetTurnsByStatus(TurnStatus.Waiting).Data;
        ViewBag.InService = _turnService.GetTurnsByStatus(TurnStatus.InService).Data;
        ViewBag.Finished  = _turnService.GetTurnsByStatus(TurnStatus.Finished).Data;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CallNext()
    {
        var result = _turnService.CallNext();
        TempData["message"] = result.Message;

        // ← NUEVO: notifica a todos (advisor + waitingRoom) que la cola cambió
        await _hub.Clients.All.SendAsync("QueueUpdated");

        return RedirectToAction("Advisor");
    }

    [HttpPost]
    public async Task<IActionResult> Finish(int id, string? comment)
    {
        var result = _turnService.FinishTurn(id, comment);
        TempData["message"] = result.Message;
        await _hub.Clients.All.SendAsync("QueueUpdated");
        return RedirectToAction("Advisor");
    }

    [HttpPost]
    public async Task<IActionResult> MoveToWaiting(int id)
    {
        var result = _turnService.MoveToWaiting(id);
        TempData["message"] = result.Message;
        await _hub.Clients.All.SendAsync("QueueUpdated");
        return RedirectToAction("Advisor");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCurrentUser(int turnId, int userId, string fullName, string documentNumber)
    {
        var result = _userService.UpdateUser(new User
        {
            Id = userId,
            FullName = fullName,
            DocumentNumber = documentNumber
        });
        TempData["message"] = result.Message;
        await _hub.Clients.All.SendAsync("QueueUpdated");
        return RedirectToAction("Advisor");
    }

    [HttpPost]
    public async Task<IActionResult> MarkAbsent(int id, string? comment)
    {
        var result = _turnService.MarkAbsent(id, comment ?? "Usuario ausente");
        TempData["message"] = result.Message;
        await _hub.Clients.All.SendAsync("QueueUpdated");
        return RedirectToAction("Advisor");
    }
}
