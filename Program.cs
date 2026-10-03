using PokemonServer.Clients;
using PokemonServer.Services;
using PokemonServer.Initializers;

var builder = WebApplication.CreateBuilder(args);

var applicationHost = builder.Configuration["Application:Host"];
var applicationPort = builder.Configuration["Application:Port"];

builder.WebHost.UseUrls(
    $"http://{applicationHost}:{applicationPort}"
);

// Servicios
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<PokemonService>();
builder.Services.AddScoped<ExcelService>();
builder.Services.AddHttpClient<PokemonApiClient>(client =>
{
    var pokemonApiBaseUrl = builder.Configuration["PokemonApi:BaseUrl"];
    client.BaseAddress = new Uri(pokemonApiBaseUrl!);
});
builder.Services.AddMemoryCache();
builder.Services.AddScoped<PokemonInitializer>();

var app = builder.Build();

//Cargas iniciales
using (var scope = app.Services.CreateScope())
{
    var pokemonInitializer = scope.ServiceProvider
        .GetRequiredService<PokemonInitializer>();

    //Inicialización de la memoria caché y almacenamiento de Pokemons 
    await pokemonInitializer.InitializeAsync();
}

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
