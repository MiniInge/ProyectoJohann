using Microsoft.EntityFrameworkCore;
using cine.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext con Supabase
builder.Services.AddDbContext<CineDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Soporte de fechas/horas para PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CineDbContext>();

    try
    {
        string script = dbContext.Database.GenerateCreateScript();
        dbContext.Database.ExecuteSqlRaw(script);
    }
    catch
    {
    }

    if (!dbContext.Funciones.Any())
    {
        var pelicula = dbContext.Peliculas.FirstOrDefault() ?? new Pelicula
        {
            Titulo = "Avatar: El Camino del Agua",
            Sinopsis = "Jake Sully vive con su nueva familia en el planeta Pandora.",
            DuracionMinutos = 192,
            PosterUrl = "https://via.placeholder.com/150"
        };

        var sala = dbContext.Salas.FirstOrDefault() ?? new Sala
        {
            Nombre = "Sala 1 - IMAX 3D",
            TotalFilas = 6,
            AsientosPorFila = 8
        };

        if (pelicula.Id == 0) dbContext.Peliculas.Add(pelicula);
        if (sala.Id == 0) dbContext.Salas.Add(sala);
        dbContext.SaveChanges();

        var funcion = new Funcion
        {
            PeliculaId = pelicula.Id,
            SalaId = sala.Id,
            FechaHora = DateTime.UtcNow.AddHours(3),
            PrecioEntrada = 75.00m
        };

        dbContext.Funciones.Add(funcion);
        dbContext.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<cine.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();