using BrunusBurguer.Context;
using BrunusBurguer.Models;
using BrunusBurguer.Repositories;
using BrunusBurguer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//--Implementacao padrao do IDistributedCache (session)
builder.Services.AddMemoryCache();
builder.Services.AddSession();
//--

//--Implementacao do HttpContext
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp)); //registrando o serviço e já criando um carrinho -- Scoped (uma instancia a cada request, se dois clientes solicitarem um carrinho, cada um terá uma instancia diferente pois sao requests diferentes)


//-- Conexao com o banco e contexto para entity
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => //Registrando context 
            options.UseSqlServer(connectionString));
//--

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); //Session

app.UseAuthorization();

app.MapControllerRoute(
    name: "categoriaFiltro",
    pattern: "Lanche/{action}/{categoria?}",
    defaults: new { Controller = "Lanche", action = "List" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
