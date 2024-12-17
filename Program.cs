var builder = WebApplication.CreateBuilder(args);

// ���������� �������� � ��������� DI
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ������������ HTTP-���������
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
