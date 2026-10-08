using Microsoft.AspNetCore.Authentication.Cookies;
using YouLearnGeometry.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register MongoDB and Neo4j services
builder.Services.AddSingleton<IMongoDbService, MongoDbService>();
builder.Services.AddSingleton<INeo4jService, Neo4jService>();
builder.Services.AddScoped<IDataSeeder, DataSeeder>();

// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

var app = builder.Build();

// Auto seed data on application startup
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    try
    {
        Console.WriteLine("Đang tiến hành seed dữ liệu mẫu vào MongoDB Atlas & Neo4j Aura...");
        await seeder.SeedAllAsync();
        Console.WriteLine("✔ Seed dữ liệu mẫu thành công!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ Lỗi khi seed dữ liệu: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
