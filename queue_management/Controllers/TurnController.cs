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
    public IActionResult RequestTurn(string documentNumber)
    {
        var userResult = _userService.GetUserByDocument(documentNumber);

        if (!userResult.Success)
        {
            TempData["message"] = "User not found. Please register first";
            return RedirectToAction("Kiosk");
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
    public IActionResult Finish(int id, string? comment)
    {
        var result = _turnService.FinishTurn(id, comment);
        TempData["message"] = result.Message;
        return RedirectToAction("Advisor");
    }
}