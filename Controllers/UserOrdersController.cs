using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArenaSportProj.Data;
using ArenaSportProj.Models;

namespace ArenaSportProj.Controllers;

[Authorize(Roles = "User,Admin")]
public class UserOrdersController : Controller
{
    private readonly ApplicationDbContext _ctx;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserOrdersController(ApplicationDbContext ctx, UserManager<ApplicationUser> um)
    {
        _ctx = ctx;
        _userManager = um;
    }

    // GET: /UserOrders
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User)!;
        var orders = await _ctx.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedOn)
            .ToListAsync();

        return View(orders);
    }
}
