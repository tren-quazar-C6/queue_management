using Microsoft.AspNetCore.Mvc;
using queue_management.Models;
using queue_management.Services;

namespace queue_management.Controllers;

public class TurnController : Controller
{
    private readonly TurnService _turnService;
    private readonly UserService _userService;

    public TurnController(TurnService turnService, UserService userService)
    {
        _turnService = turnService;
        _userService = userService;
    }
    
    public IActionResult Kiosk()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult RequestTurn(string documentNumber, string fullName)
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
        ViewBag.Waiting   = _turnService.GetTurnsByStatus(TurnStatus.Waiting).Data;
        ViewBag.InService = _turnService.GetTurnsByStatus(TurnStatus.InService).Data;
        ViewBag.Finished  = _turnService.GetTurnsByStatus(TurnStatus.Finished).Data;

        return View();
    }
    
    [HttpPost]
    public IActionResult CallNext()
    {
        var result = _turnService.CallNext();
        TempData["message"] = result.Message;
        return RedirectToAction("Advisor");
    }

    [HttpPost]
    public IActionResult MoveToWaiting(int id)
    {
        var result = _turnService.MoveToWaiting(id);
        TempData["message"] = result.Message;
        return RedirectToAction("Advisor");
    }
    
    [HttpPost]
    public IActionResult Finish(int id, string? comment)
    {
        var result = _turnService.FinishTurn(id, comment);
        TempData["message"] = result.Message;
        return RedirectToAction("Advisor");
    }

    [HttpPost]
    public IActionResult MarkAbsent(int id, string? comment)
    {
        var result = _turnService.MarkAbsent(id, comment);
        TempData["message"] = result.Message;
        return RedirectToAction("Advisor");
    }

    [HttpPost]
    public IActionResult UpdateCurrentUser(int turnId, int userId, string fullName, string documentNumber)
    {
        _ = turnId;
        var result = _userService.UpdateUser(new User
        {
            Id = userId,
            FullName = fullName,
            DocumentNumber = documentNumber
        });
        TempData["message"] = result.Message;
        return RedirectToAction("Advisor");
    }
}
