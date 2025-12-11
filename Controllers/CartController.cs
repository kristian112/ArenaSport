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
        var productExists = await _ctx.Products.AnyAsync(p => p.Id == productId);
        if (!productExists)
        {
            TempData["Error"] = "Избраният продукт не съществува.";
            return RedirectToAction("Index", "Products");
        }

        var userId = _userManager.GetUserId(User)!;

        var existing = await _ctx.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        if (existing != null)
            existing.Quantity++;
        else
            _ctx.CartItems.Add(new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            });

        await _ctx.SaveChangesAsync();
        return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var item = await _ctx.CartItems.FindAsync(id);

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

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int itemId, int quantity)
    {
        var item = await _ctx.CartItems.Include(c => c.Product).FirstOrDefaultAsync(c => c.Id == itemId);

        if (item == null)
            return NotFound();

        if (quantity < 1) quantity = 1;
        if (quantity > 10) quantity = 10;

        item.Quantity = quantity;
        await _ctx.SaveChangesAsync();

        return RedirectToAction("Index");
    }



    // GET
    [AllowAnonymous]
    public IActionResult Success() => View();


}
