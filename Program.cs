var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton(typeof(IRepository<Event>), typeof(InMemoryRepository<Event>));
builder.Services.AddSingleton(typeof(IRepository<ClassifiedItem>), typeof(InMemoryRepository<ClassifiedItem>));

var app = builder.Build();


var eventRepo = app.Services.GetRequiredService<IRepository<Event>>();
eventRepo.Add(new Event { Title = "Hackathon", Description = "Coding contest 2!", Date = DateTime.Today.AddDays(3) });
eventRepo.Add(new Event { Title = "Career Fair", Description = "Meet companies.", Date = DateTime.Today.AddDays(7) });

/*
using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetRequiredService<IRepository<Event>>();

    repo.Add(new Event
    {
        Title = "Hackathon",
        Description = "A 24-hour coding marathon!",
        Date = DateTime.Today.AddDays(2)
    });

    repo.Add(new Event
    {
        Title = "Job Fair",
        Description = "Meet tech companies on campus.",
        Date = DateTime.Today.AddDays(5)
    });
}*/


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

app.Run();

