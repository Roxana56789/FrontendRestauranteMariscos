using FrontendRestauranteMarisco.WebApp.Service;
using FrontendRestauranteMarisco.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ApiService>(client =>
{
    // Define la URL base de la API que vamos a consumir
    client.BaseAddress = new Uri("https://localhost:7053/api/"); // API base (puerto seg�n pc)
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PlatilloService>();


// Configuraci�n de la autenticaci�n de la aplicaci�n usando cookies
builder.Services.AddAuthentication("AuthCookie")
    .AddCookie("AuthCookie", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Despu�s de 60 minutos, el usuario tendr� que iniciar sesi�n nuevamente
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

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
