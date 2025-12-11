using ArenaSportProj.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArenaSportProj.Controllers;

[AllowAnonymous]
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _ctx;
    public ProductsController(ApplicationDbContext ctx) => _ctx = ctx;

    public async Task<IActionResult> Index(
        string search,
        string? category,
        string? brand,
        decimal? minPrice,
        decimal? maxPrice,
        string? sort)
    {
        var query = _ctx.Products
            .Include(p => p.Category)
            .AsQueryable();

        // 🔍 SEARCH
        if (!string.IsNullOrWhiteSpace(search))
        {
            string lower = search.ToLower();
            query = query.Where(p =>
                p.Name.ToLower().Contains(lower) ||
                p.Brand.ToLower().Contains(lower));
        }

        // 🏷 CATEGORY FILTER
        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category.Name == category);

        // 🏭 BRAND FILTER
        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(p => p.Brand == brand);

        // 💵 PRICE FILTERS
        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice);

        // ↕ SORTING
        sort = sort?.ToLower();
        query = sort switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "name_asc" => query.OrderBy(p => p.Name),
            "name_desc" => query.OrderByDescending(p => p.Name),
            _ => query.OrderBy(p => p.Id)
        };
        // ГРУПИРАНЕ ПО МАРКА
        var brandCounts = await _ctx.Products
            .GroupBy(p => p.Brand)
            .Select(g => new
            {
                Brand = g.Key,
                Count = g.Count()
            })
            .OrderBy(g => g.Brand)
            .ToListAsync();

        ViewBag.BrandCounts = brandCounts;


        // VIEWBAGS
        ViewBag.Categories = await _ctx.Categories.Select(c => c.Name).ToListAsync();
        ViewBag.Brands = await _ctx.Products.Select(p => p.Brand).Distinct().ToListAsync();
        ViewBag.CurrentCategory = category;
        ViewBag.CurrentBrand = brand;
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.Sort = sort;
        ViewBag.Search = search;

        var items = await query.ToListAsync();
        return View(items);
    }


    public async Task<IActionResult> Details(int id)
    {
        var p = await _ctx.Products.Include(i => i.Category).FirstOrDefaultAsync(x => x.Id == id);
        return p == null ? NotFound() : View(p);
    }
}
