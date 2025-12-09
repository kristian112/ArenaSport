using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ArenaSportProj.Models;

namespace ArenaSportProj.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminUsersController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // LIST
    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    // DETAILS
    public async Task<IActionResult> Details(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();
        return View(user);
    }

    // EDIT (GET)
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();
        return View(user);
    }

    // EDIT (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(string id, ApplicationUser updated)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        user.Email = updated.Email;
        user.UserName = updated.Email; // ако искаш да са равни
        user.PhoneNumber = updated.PhoneNumber;

        await _userManager.UpdateAsync(user);

        return RedirectToAction("Index");
    }

    // DELETE
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        await _userManager.DeleteAsync(user);

        return RedirectToAction("Index");
    }
}
