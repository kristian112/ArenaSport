using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArenaSportProj.Data;
using ArenaSportProj.Models;

namespace ArenaSportProj.Controllers;

[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _ctx;
    public OrdersController(ApplicationDbContext ctx) => _ctx = ctx;

    public async Task<IActionResult> Index()
    {
        var orders = await _ctx.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ToListAsync();

        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _ctx.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();
        return View(order);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Order o)
    {
        _ctx.Orders.Add(o);
        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var order = await _ctx.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();

        // Ако имаш OrderItems, първо ги чистим за да няма FK errors
        if (order.Items.Any())
        {
            _ctx.OrderItems.RemoveRange(order.Items);
        }

        _ctx.Orders.Remove(order);
        await _ctx.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
