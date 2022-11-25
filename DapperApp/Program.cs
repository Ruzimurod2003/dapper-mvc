using DapperApp.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string? connectionString = builder.Configuration.GetConnectionString("PeopleConnection");

builder.Services.AddTransient<IUserRepository, UserRepository>(provider => new UserRepository(connectionString));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
