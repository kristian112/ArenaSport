using ArenaSportProj.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace ArenaSportProj.Data;

public static class RealProductSeed
{
    public static void Seed(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!ctx.Categories.Any())
        {
            ctx.Categories.AddRange(
                new Category { Name = "Бокс" },
                new Category { Name = "Кикбокс" },
                new Category { Name = "ММА" },
                new Category { Name = "Муа Тай" },
                new Category { Name = "Бразилско Джу-Джицу" }
            );
            ctx.SaveChanges();
        }

        if (false)
        {
            var cats = ctx.Categories.ToList();

            var list = new List<Product>
            {

                new Product {
                    Name = "Каска за бокс Adidas",
                    Brand = "Adidas",
                    Price = 99,
                    Description = "Каска за тренировки, подходяща за спаринг.",
                    CategoryId = cats.First(c => c.Name == "Бокс").Id,
                    ImageUrl = "https://m.media-amazon.com/images/I/61QwIYOJqHL._AC_SL1500_.jpg"
                },
                new Product {
                    Name = "ММА шорти Venum Gladiator",
                    Brand = "Venum",
                    Price = 89,
                    Description = "Еластични, удобни и устойчиви на разкъсване.",
                    CategoryId = cats.First(c => c.Name == "ММА").Id,
                    ImageUrl = "https://cdn.shopify.com/s/files/1/0556/8349/7184/products/venum-gladiator-3-0-fightshorts-black-red-1_1024x1024.jpg"
                },
                new Product {
                    Name = "Муа Тай шорти Top King",
                    Brand = "Top King",
                    Price = 79,
                    Description = "Класически Тайландски дизайн с отлична свобода на движение.",
                    CategoryId = cats.First(c => c.Name == "Муа Тай").Id,
                    ImageUrl = "https://www.muaythai-shop.com/media/catalog/product/cache/1/image/600x/040ec09b1e35df139433887a97daa66f/t/k/tks-enc_tks-enc_z1_1.jpg"
                },
                new Product {
                    Name = "BJJ Gi Tatami Nova",
                    Brand = "Tatami",
                    Price = 159,
                    Description = "Лек и удобен GI за BJJ тренировки и състезания.",
                    CategoryId = cats.First(c => c.Name == "Бразилско Джу-Джицу").Id,
                    ImageUrl = "https://www.fightstoreireland.com/wp-content/uploads/2020/02/TATAMI-NOVA-ABSOLUTE-BLACK-7.jpg"
                },
                new Product {
                    Name = "Протектор за уста Shock Doctor",
                    Brand = "Shock Doctor",
                    Price = 29,
                    Description = "Двуслоен протектор за оптимална защита.",
                    CategoryId = cats.First(c => c.Name == "ММА").Id,
                    ImageUrl = "https://mmawarehouse.wpenginepowered.com/wp-content/uploads/2018/02/shock-doctor-mouthguard-gel-max.jpg"
                },

                // 🔥 добавям още 12, за да станат общо 20
                new Product {
                    Name="Бинтове Venum 4м", Brand="Venum", Price=19,
                    Description="Еластични бинтове за бокс 4 метра.",
                    CategoryId=cats.First(c=>c.Name=="Бокс").Id,
                    ImageUrl="https://cdn.shopify.com/s/files/1/0556/8349/7184/products/venum-handwraps-black_1024x1024.jpg"
                },
                new Product {
                    Name="ММА лапи Fairtex", Brand="Fairtex", Price=109,
                    Description="Компактни лапи за скорост и точност.",
                    CategoryId=cats.First(c=>c.Name=="ММА").Id,
                    ImageUrl="https://muaythaiaddict.com/wp-content/uploads/2020/09/fairtex-focus-mitts.jpg"
                },
                new Product {
                    Name="Кикбокс боксови обувки Adidas", Brand="Adidas", Price=129,
                    Description="Леки обувки за бокс и кикбокс.",
                    CategoryId=cats.First(c=>c.Name=="Кикбокс").Id,
                    ImageUrl="https://m.media-amazon.com/images/I/61fH4UpDZJL._AC_SL1500_.jpg"
                },
                new Product {
                    Name="Муа Тай лапи Twins", Brand="Twins", Price=189,
                    Description="Професионални лапи, произведени в Тайланд.",
                    CategoryId=cats.First(c=>c.Name=="Муа Тай").Id,
                    ImageUrl="https://www.muaythaifactory.com/images/Twins%20KPL10%20kick%20pads%20black.jpg"
                },
                new Product {
                    Name="BJJ колан Tatami Black", Brand="Tatami", Price=29,
                    Description="BJJ колан от памук – черен.",
                    CategoryId=cats.First(c=>c.Name=="Бразилско Джу-Джицу").Id,
                    ImageUrl="https://budoshop.nl/wp-content/uploads/2021/07/bjj-belt-tatami-black.jpg"
                },
                new Product {
                    Name="Главен протектор TopTen", Brand="TopTen", Price=149,
                    Description="Лек и проветлив протектор за бокс.",
                    CategoryId=cats.First(c=>c.Name=="Бокс").Id,
                    ImageUrl="https://www.toptenstore.com/media/image/product/1744/md/top-ten-head-guard.jpg"
                },
                new Product {
                    Name="ММА шорти Bad Boy", Brand="Bad Boy", Price=69,
                    Description="Класически MMA шорти, удобни и леки.",
                    CategoryId=cats.First(c=>c.Name=="ММА").Id,
                    ImageUrl="https://m.media-amazon.com/images/I/61Jt9Hn7Q4L._AC_SL1500_.jpg"
                },
                new Product {
                    Name="Кикбокс каска RDX", Brand="RDX", Price=89,
                    Description="Универсална каска за ударни спортове.",
                    CategoryId=cats.First(c=>c.Name=="Кикбокс").Id,
                    ImageUrl="https://m.media-amazon.com/images/I/71qevBK33ML._AC_UL1500_.jpg"
                },
                new Product {
                    Name="Муа Тай шорти Yokkao", Brand="Yokkao", Price=109,
                    Description="Премиум шорти, ръчно изработени.",
                    CategoryId=cats.First(c=>c.Name=="Муа Тай").Id,
                    ImageUrl="https://yokkao.com/cdn/shop/products/SHORTS280_800x.jpg"
                },
                new Product {
                    Name="BJJ Rashguard Venum", Brand="Venum", Price=79,
                    Description="Стегнат рашгард за grappling и BJJ.",
                    CategoryId=cats.First(c=>c.Name=="Бразилско Джу-Джицу").Id,
                    ImageUrl="https://cdn.shopify.com/s/files/1/0556/8349/7184/products/venum-rashguard-phantom-1_1024x1024.jpg"
                },
                new Product {
                    Name = "Муа Тай ръкавици Yokkao Matrix",
                    Brand = "Yokkao",
                    Price = 189,
                    Description = "Премиум ръкавици, ръчно изработени в Тайланд. Отлична защита и комфорт за Муа Тай тренировките.",
                    CategoryId = cats.First(c => c.Name == "Муа Тай").Id,
                    ImageUrl = "https://yokkao.com/cdn/shop/products/Yokkao-Matrix-Black-Gloves-1_800x.jpg"
                },
                new Product {
                    Name = "Боксови лапи RDX Curved Focus Pads",
                    Brand = "RDX",
                    Price = 79,
                    Description = "Издръжливи извити лапи за тренировки по бокс и кикбокс. Отлична амортизация и комфортна дръжка.",
                    CategoryId = cats.First(c => c.Name == "Бокс").Id,
                    ImageUrl = "https://m.media-amazon.com/images/I/71Xj5ZNVYQL._AC_SL1500_.jpg"
                }
            };


            ctx.Products.AddRange(list);
            ctx.SaveChanges();
        }
    }
}
