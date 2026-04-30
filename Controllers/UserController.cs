using Microsoft.AspNetCore.Mvc;
using queue_management.Models;
using queue_management.Services;

namespace queue_management.Controllers;

public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    public IActionResult Index()
    {
        var response = _userService.GetAllUsers();
        return View(response.Data);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Store(User user)
    {
        var result = _userService.SaveUser(user);
        TempData["message"] = result.Message;

        if (result.Success)
            return RedirectToAction("Index");

        return RedirectToAction("Create");
    }
    
    public IActionResult Edit(int id)
    {
        var result = _userService.GetUserById(id);

        if (!result.Success)
        {
            TempData["message"] = result.Message;
            return RedirectToAction("Index");
        }

        return View(result.Data);
    }
    
    [HttpPost]
    public IActionResult Update(User user)
    {
        var result = _userService.UpdateUser(user);
        TempData["message"] = result.Message;

        if (!result.Success)
            return RedirectToAction("Edit", new { id = user.Id });

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Destroy(User user)
    {
        var result = _userService.DeleteUser(user);
        TempData["message"] = result.Message;
        return RedirectToAction("Index");
    }
}