using DigitalPortalAcademy;
using DigitalPortalAcademy.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Регистрация вашего сервиса в DI-контейнере

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AdministratorService>();
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120); // Время жизни сессии
    options.Cookie.HttpOnly = true;                // Защита от JavaScript-доступа
    options.Cookie.IsEssential = true;             // Обязательно для работы
});

builder.Services.AddDbContext<AcademyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}/{id?}");
app.Run();