using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArenaSportProj.Data;
using ArenaSportProj.Models;

namespace ArenaSportProj.Controllers;

[Authorize(Roles = "Admin")]
public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _ctx;
    public CategoriesController(ApplicationDbContext ctx) => _ctx = ctx;

    public async Task<IActionResult> Index()
    {
        var cats = await _ctx.Categories.ToListAsync();
        return View(cats);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Category c)
    {
        _ctx.Categories.Add(c);
        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var c = await _ctx.Categories.FindAsync(id);
        if (c == null) return NotFound();
        return View(c);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Category c)
    {
        var db = await _ctx.Categories.FindAsync(id);
        if (db == null) return NotFound();
        db.Name = c.Name;
        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var c = await _ctx.Categories.FindAsync(id);
        if (c == null) return NotFound();
        _ctx.Categories.Remove(c);
        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
