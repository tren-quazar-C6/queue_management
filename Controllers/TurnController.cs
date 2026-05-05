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
    private readonly IHubContext<QueueHub> _hub;

    public TurnController(TurnService turnService, UserService userService, IHubContext<QueueHub> hub)
    {
        _turnService = turnService;
        _userService = userService;
        _hub = hub;
    }

    private void PrintReceipt(string fullName, string documentNumber, string ticketCode)
    {
        var content = "==================\n" +
                      "      HELLO       \n" +
                      "==================\n" +
                      $"Name:  {fullName}\n" +
                      $"Doc #: {documentNumber}\n" +
                      $"Ticket:{ticketCode}\n" +
                      "==================\n" +
                      " Please wait for  \n     your turn    \n" +
                      "==================\n" +
                      "\n\n\n";

        var tempFile = "/tmp/receipt.txt";
        System.IO.File.WriteAllText(tempFile, content);

        var process = new System.Diagnostics.Process
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "lp",
                Arguments = $"-d Printer_USB_Printer_Port {tempFile}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            }
        };

        process.Start();
        process.WaitForExit();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Kiosk()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RequestTurn(string documentNumber, string fullName)
    {
        var userResult = _userService.GetUserByDocument(documentNumber);

        if (!userResult.Success)
        {
            var createResult = _userService.SaveUser(new User
            {
                DocumentNumber = documentNumber,
                FullName = fullName
            });

            if (!createResult.Success)
            {
                TempData["message"] = createResult.Message;
                return RedirectToAction("Kiosk");
            }

            userResult = createResult;
        }
    
        var result = _turnService.SaveTurn(userResult.Data!.Id);
        TempData["message"] = result.Message;

        if (result.Success)
        {
            TempData["ticket"] = result.Data!.TicketCode;
        
            
            PrintReceipt(fullName, documentNumber, result.Data!.TicketCode);
        
            await _hub.Clients.All.SendAsync("QueueUpdated");
            return RedirectToAction("Ticket");
        }

        return RedirectToAction("Kiosk");
    }
    
    public IActionResult Ticket()
    {
        return View();
    }
    
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
    public async Task<IActionResult> Finish(int id, string? comment)
    {
        var result = _turnService.FinishTurn(id, comment);
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

    [HttpPost]
    public async Task<IActionResult> UpdateCurrentUser(int turnId, int userId, string fullName, string documentNumber)
    {
        _ = turnId;
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
}