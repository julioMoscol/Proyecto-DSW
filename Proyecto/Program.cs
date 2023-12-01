using Microsoft.AspNetCore.Authentication.Cookies;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IProducto, ProductoSQl>();
builder.Services.AddSingleton<IProveedor, ProveedorSQL>();
builder.Services.AddSingleton<ITrabajador, TrabajadorSQL>();
builder.Services.AddSingleton<ICliente, ClienteSQL>();
builder.Services.AddSingleton<ITipoBaja, TipoBajaSQL>();
builder.Services.AddSingleton<IArea, AreaSQL>();
builder.Services.AddSingleton<ICargo, CargoSQL>();
builder.Services.AddSingleton<IBaja, BajaSQL>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => {
    option.LoginPath = "/Usuario/login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();
