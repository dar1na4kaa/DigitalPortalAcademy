using DigitalPortalAcademy;
using DigitalPortalAcademy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ����������� ������ ������� � DI-����������

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<RegistrationService>();
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120); // ����� ����� ������
    options.Cookie.HttpOnly = true;                // ������ �� JavaScript-�������
    options.Cookie.IsEssential = true;             // ����������� ��� ������
});
builder.Services.AddDbContext<AcademyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSession(); // �������� ������
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
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Administrator}/{action=Index}/{id?}");
app.Run();