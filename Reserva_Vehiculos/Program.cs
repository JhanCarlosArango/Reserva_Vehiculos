using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Reserva_Vehiculos.Controllers;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<UserController>(); // Registrar el controlador
builder.Services.AddHttpContextAccessor(); // Registrar IHttpContextAccessor

// Aquí agregamos el servicio IActionContextAccessor correctamente
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddSession(
    options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(5);
    }
);

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.AccessDeniedPath = "/Home/Privacy";
        //options.Cookie.Name = "MyAppAuthCookie";
    });

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
app.UseSession();

app.UseRouting();

// Agrega el middleware de autenticación antes del middleware de autorización
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Peticion}/{action=peticion}/{id?}");

app.Run();


