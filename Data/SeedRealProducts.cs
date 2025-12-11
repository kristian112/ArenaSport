using ArenaSportProj.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace ArenaSportProj.Data;

public static class RealProductSeed
{
    public static async Task Seed(IServiceProvider services)
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

        if (!ctx.Products.Any())
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
                    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442263/kaska-boks-adidas-response-247x247_e6jryn.jpg"
                },
                new Product {
                    Name = "ММА шорти Venum Gladiator",
                    Brand = "Venum",
                    Price = 89,
                    Description = "Еластични, удобни и устойчиви на разкъсване.",
                    CategoryId = cats.First(c => c.Name == "ММА").Id,
                    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442321/e9d18b7c84544a20f6429e0e1b380c4a5b631793_hzqa83.jpg"
                },
                new Product {
                    Name = "Муа Тай шорти Top King",
                    Brand = "Top King",
                    Price = 79,
                    Description = "Класически Тайландски дизайн с отлична свобода на движение.",
                    CategoryId = cats.First(c => c.Name == "Муа Тай").Id,
                    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442440/storm_1_king_4_tugiw6.webp"
                },
                new Product {
                    Name = "BJJ Gi Tatami Nova",
                    Brand = "Tatami",
                    Price = 159,
                    Description = "Лек и удобен GI за BJJ тренировки и състезания.",
                    CategoryId = cats.First(c => c.Name == "Бразилско Джу-Джицу").Id,
                    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442560/137049-WH-nova-pls-bjj-gi-1_mgvwpc.jpg"
                },
                new Product {
                    Name = "Протектор за уста Shock Doctor",
                    Brand = "Shock Doctor",
                    Price = 29,
                    Description = "Двуслоен протектор за оптимална защита.",
                    CategoryId = cats.First(c => c.Name == "ММА").Id,
                    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442600/protektor-za-usta-gel-max-s-ili-bez-vkus-shock-doctor-image_6214c4bbc67d3_800x800_cpbglv.png"
                },

                new Product {
                    Name="Бинтове Venum 4м", Brand="Venum", Price=19,
                    Description="Еластични бинтове за бокс 4 метра.",
                    CategoryId=cats.First(c=>c.Name=="Бокс").Id,
                    ImageUrl="https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442666/Venum-Kontact-Boxing-Handwraps-black-red-jkfitness_20_2_-900x900_ipzoqk.jpg"
                },
                new Product {
                    Name="ММА лапи Fairtex", Brand="Fairtex", Price=109,
                    Description="Компактни лапи за скорост и точност.",
                    CategoryId=cats.First(c=>c.Name=="ММА").Id,
                    ImageUrl="https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442703/Fairtex-Kickpads-Muay-Thai-Schlagpolster-FXB-KP_1600x_ba8nty.jpg"
                },
                new Product {
                    Name="Кикбокс боксови обувки Adidas", Brand="Adidas", Price=129,
                    Description="Леки обувки за бокс и кикбокс.",
                    CategoryId=cats.First(c=>c.Name=="Кикбокс").Id,
                    ImageUrl="https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442748/1550_klao01.jpg"
                },
                new Product {
                    Name="Муа Тай лапи Twins", Brand="Twins", Price=189,
                    Description="Професионални лапи, произведени в Тайланд.",
                    CategoryId=cats.First(c=>c.Name=="Муа Тай").Id,
                    ImageUrl="https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442807/res_1b49b357db7d7fbe6ff89d3cd58682fd_qcfe74.jpg"
                },
                new Product {
                    Name="BJJ колан Tatami Black", Brand="Tatami", Price=29,
                    Description="BJJ колан от памук – черен.",
                    CategoryId=cats.First(c=>c.Name=="Бразилско Джу-Джицу").Id,
                    ImageUrl="https://bjjup.bg/cdn/shop/files/fuji-bjj-belt-5.jpg?v=1739478557"
                },
                new Product {
                    Name="Главен протектор TopTen", Brand="TopTen", Price=149,
                    Description="Лек и проветлив протектор за бокс.",
                    CategoryId=cats.First(c=>c.Name=="Бокс").Id,
                    ImageUrl="https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442876/top-ten-kaska-competition-fight-blue_ipdsuh.jpg"
                },
                new Product {
                    Name="ММА шорти Bad Boy", Brand="Bad Boy", Price=69,
                    Description="Класически MMA шорти, удобни и леки.",
                    CategoryId=cats.First(c=>c.Name=="ММА").Id,
                    ImageUrl="https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442907/mma-sorti-legacy-ii-bad-boy-image_5d9661a475929_600x600_u6wupv.jpg"
                },
                new Product {
                    Name="Кикбокс каска RDX", Brand="RDX", Price=89,
                    Description="Универсална каска за ударни спортове.",
                    CategoryId=cats.First(c=>c.Name=="Кикбокс").Id,
                    ImageUrl="https://res.cloudinary.com/daiw6lh6i/image/upload/v1765442953/res_7b7aa36ca32bb6a1e9494ceb47f76f78_jtstrh.jpg"
                },
                new Product {
                    Name="Муа Тай шорти Yokkao", Brand="Yokkao", Price=109,
                    Description="Премиум шорти, ръчно изработени.",
                    CategoryId=cats.First(c=>c.Name=="Муа Тай").Id,
                    ImageUrl="https://res.cloudinary.com/daiw6lh6i/image/upload/v1765443777/yokkao-havajski-oranzhevi-mma-shorti-tybs-136-2-0_piqoij.jpg"
                },
                new Product {
                    Name="BJJ Rashguard Venum", Brand="Venum", Price=79,
                    Description="Стегнат рашгард за grappling и BJJ.",
                    CategoryId=cats.First(c=>c.Name=="Бразилско Джу-Джицу").Id,
                    ImageUrl="https://res.cloudinary.com/daiw6lh6i/image/upload/v1765443832/venum-venum-bjjrashguard-nogi-20-l-s-black-white-v_fgs828.jpg"
                },
                new Product {
                    Name = "Муа Тай ръкавици Yokkao Matrix",
                    Brand = "Yokkao",
                    Price = 189,
                    Description = "Премиум ръкавици, ръчно изработени в Тайланд. Отлична защита и комфорт за Муа Тай тренировките.",
                    CategoryId = cats.First(c => c.Name == "Муа Тай").Id,
                    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765443892/8859386461323_40_brwca7.jpg"
                },
                new Product {
                    Name = "Боксови лапи RDX Curved Focus Pads",
                    Brand = "RDX",
                    Price = 79,
                    Description = "Издръжливи извити лапи за тренировки по бокс и кикбокс. Отлична амортизация и комфортна дръжка.",
                    CategoryId = cats.First(c => c.Name == "Бокс").Id,
                    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765443957/_________________65d0780b56524_shfbfo.jpg"
                },
                new Product
{
    Name = "Шин за крака Fairtex SP3",
    Description = "Професионални предпазни шини за крака, изработени от издръжлива синтетична кожа. Подходящи за муай тай и кикбокс тренировки.",
    Brand = "Fairtex",
    Price = 139.00m,
    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765444165/sp3_black_03_3-550x550_xksbhp.jpg",
    CategoryId = 4 // Кикбокс / Муай Тай
},
                new Product
{
    Name = "Ръкавици Venum Challenger 3.0 Black",
    Description = "Популярните Venum Challenger 3.0 предлагат отлична защита, комфорт и вентилация за боксови и кикбокс тренировки.",
    Brand = "Venum",
    Price = 119.00m,
    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765444246/29592_1_qu7dfl.jpg",
    CategoryId = 1 // Бокс
}
,
                new Product
{
    Name = "MMA ръкавици Bad Boy Legacy",
    Description = "Професионални MMA ръкавици от серията Legacy на Bad Boy. Отлична защита за спаринг и граплинг.",
    Brand = "Bad Boy",
    Price = 79.00m,
    ImageUrl = "https://res.cloudinary.com/daiw6lh6i/image/upload/v1765444307/mma-rukavici-legasy-cherni-bad-boy-online-sportstore_mnn87v.jpg",
    CategoryId = 3 // MMA
}

            };


            ctx.Products.AddRange(list);
            ctx.SaveChanges();
        }
    }
}
