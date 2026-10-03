using PokemonServer.Clients;
using PokemonServer.Services;

var builder = WebApplication.CreateBuilder(args);

var applicationHost = builder.Configuration["Application:Host"];
var applicationPort = builder.Configuration["Application:Port"];

builder.WebHost.UseUrls(
    $"http://{applicationHost}:{applicationPort}"
);

// Servicios
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddHttpClient<PokemonApiClient>(client =>
{
    var pokemonApiBaseUrl = builder.Configuration["PokemonApi:BaseUrl"];
    client.BaseAddress = new Uri(pokemonApiBaseUrl!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
