using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArenaSportProj.Data;
using ArenaSportProj.Models;

namespace ArenaSportProj.Controllers;

[Authorize(Roles = "Admin")]
public class AdminProductsController : Controller
{
    private readonly ApplicationDbContext _ctx;
    public AdminProductsController(ApplicationDbContext ctx) => _ctx = ctx;

    public async Task<IActionResult> Index()
    {
        ViewBag.Categories = await _ctx.Categories.ToListAsync();
        var products = await _ctx.Products.Include(p => p.Category).ToListAsync();
        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _ctx.Categories.ToList();
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(Product p)
    {

        _ctx.Products.Add(p);
        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.Categories = await _ctx.Categories.ToListAsync();
        var product = await _ctx.Products.FindAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product p)
    {
        var db = await _ctx.Products.FindAsync(id);
        if (db == null) return NotFound();

        db.Name = p.Name;
        db.Brand = p.Brand;
        db.Price = p.Price;
        db.CategoryId = p.CategoryId;
        db.Description = p.Description;

        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var p = await _ctx.Products.FindAsync(id);
        if (p == null) return NotFound();

        _ctx.Products.Remove(p);
        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Details(int id)
    {
        var product = await _ctx.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();
        return View(product);
    }

}
