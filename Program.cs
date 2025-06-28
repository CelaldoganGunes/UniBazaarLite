

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton(typeof(IRepository<Event>), typeof(InMemoryRepository<Event>));
builder.Services.AddSingleton(typeof(IRepository<ClassifiedItem>), typeof(InMemoryRepository<ClassifiedItem>));
builder.Services.AddSingleton<IUserContextService, FakeUserContextService>();
builder.Services.AddScoped(typeof(ValidateEntityExistsFilter<>));
builder.Services.AddControllersWithViews();

var app = builder.Build();


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


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.UseStatusCodePagesWithRedirects("/Error");
app.Run();

