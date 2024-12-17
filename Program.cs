var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер DI
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Конфигурация HTTP-конвейера
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
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
