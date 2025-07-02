var builder = WebApplication.CreateBuilder(args);

// Yapımcı: Üçümüz (Celaldoğan Güneş, Burak Kılıç, Hüseyin Kaplan)
// Bu dosya uygulamanın giriş noktası. Dependency Injection container ayarları,
// örnek veri ekleme ve pipeline konfigürasyonları burada yapılır.

// DI container'a servisleri ekliyoruz
builder.Services.AddSingleton(typeof(IRepository<Event>), typeof(InMemoryRepository<Event>));
builder.Services.AddSingleton(typeof(IRepository<ClassifiedItem>), typeof(InMemoryRepository<ClassifiedItem>));
builder.Services.AddSingleton<IUserContextService, FakeUserContextService>();
builder.Services.AddScoped(typeof(ValidateEntityExistsFilter<>));

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<LogActivityFilter>(); // MVC için global log filter
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.ConfigureFilter(new LogActivityFilter()); // Razor Pages için global log filter
});

var app = builder.Build();

// Örnek verileri uygulama başladığında belleğe ekliyoruz
var eventRepo = app.Services.GetRequiredService<IRepository<Event>>();
eventRepo.Add(new Event { Title = "Hackathon", Description = "Coding contest 2!", Date = DateTime.Today.AddDays(3) });
eventRepo.Add(new Event { Title = "Career Fair", Description = "Meet companies.", Date = DateTime.Today.AddDays(7) });

var itemRepo = app.Services.GetRequiredService<IRepository<ClassifiedItem>>();
itemRepo.Add(new ClassifiedItem
{
    Title = "Used Laptop",
    Description = "i5, 8GB RAM, good condition.",
    Price = 3500m,
    Seller = "Ahmet"
});
itemRepo.Add(new ClassifiedItem
{
    Title = "Dorm Furniture Set",
    Description = "Bed, desk, chair included.",
    Price = 1200m,
    Seller = "Zeynep"
});

// HTTP pipeline ayarları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.UseStatusCodePagesWithRedirects("/Error"); // ör: 404 için /Error'a yönlendir

app.Run();
