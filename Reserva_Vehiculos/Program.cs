using Microsoft.AspNetCore.Authentication.Cookies;
using Reserva_Vehiculos.Controllers;
using Microsoft.AspNetCore.Http;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<UserController>(); // es de vital importancia registar el controlador 
builder.Services.AddHttpContextAccessor(); // Aquí agregamos el servicio IHttpContextAccessor correctamente

builder.Services.AddSession(
    option=>{
        option.IdleTimeout = TimeSpan.FromSeconds(60);
    }
);

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.AccessDeniedPath = "/Usuario/IniciarSesion";
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


