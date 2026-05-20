using Microsoft.EntityFrameworkCore;
using MvcLab1.Data;
using MvcLab1.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Регистрация контекста БД
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information)
           .EnableSensitiveDataLogging());

// Регистрация EF репозиториев
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<IWorkoutRepository, EfWorkoutRepository>();

var app = builder.Build();

// ========== ИНИЦИАЛИЗАЦИЯ БАЗЫ ДАННЫХ ==========
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // Создаёт БД, если её нет
    SeedData.Initialize(dbContext);     // Добавляет тестовые данные
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "about", pattern: "about-us", defaults: new { controller = "Home", action = "Privacy" });
app.MapControllerRoute(name: "userProfile", pattern: "user/{username}/{action=Profile}", defaults: new { controller = "Demo" });
app.MapControllerRoute(name: "product", pattern: "product/{id:int}", defaults: new { controller = "Demo", action = "ProductDetails" });

app.Run();