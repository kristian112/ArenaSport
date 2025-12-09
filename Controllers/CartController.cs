using ArenaSportProj.Data;
using ArenaSportProj.Models;
using ArenaSportProj.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArenaSportProj.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ApplicationDbContext _ctx;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
    {
        _ctx = ctx;
        _userManager = userManager;
    }

    // GET: /Cart
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User)!;
        var items = await _ctx.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        ViewBag.Total = items.Sum(i => i.Product.Price * i.Quantity);
        return View(items);
    }

    public async Task<IActionResult> Add(int productId)
    {
        var userId = _userManager.GetUserId(User)!;
        var existing = await _ctx.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        if (existing != null) existing.Quantity++;
        else _ctx.CartItems.Add(new CartItem { UserId = userId, ProductId = productId, Quantity = 1 });

        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int productId)
    {
        var userId = _userManager.GetUserId(User)!;
        var item = await _ctx.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        if (item != null)
        {
            _ctx.CartItems.Remove(item);
            await _ctx.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }


    // GET
    public IActionResult Checkout() => View();

    // POST
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutViewModel vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var userId = _userManager.GetUserId(User)!;
        var cartItems = await _ctx.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        var total = cartItems.Sum(i => i.Product.Price * i.Quantity);

        var order = new Order
        {
            UserId = userId,
            Address = vm.Address,
            PhoneNumber = vm.PhoneNumber,
            TotalPrice = total,
            PaymentMethod = vm.PaymentMethod
        };

        foreach (var i in cartItems)
        {
            order.Items.Add(new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.Product.Price
            });
        }

        _ctx.Orders.Add(order);
        _ctx.CartItems.RemoveRange(cartItems);
        await _ctx.SaveChangesAsync();

        return RedirectToAction("Success");
    }

    // GET
    [AllowAnonymous]
    public IActionResult Success() => View();


}
