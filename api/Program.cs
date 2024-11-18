using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Ajoutez votre DbContext avant de construire l'application
builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ConnectionDB"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConnectionDB"))
    ));

// Ajoutez les services au conteneur
builder.Services.AddControllers();
// Configurez Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    try
    {
        // Tester la connexion à la base de données
        if (await context.Database.CanConnectAsync())
        {
            Console.WriteLine("Connexion à la base de données réussie !");
        }
        else
        {
            Console.WriteLine("Impossible de se connecter à la base de données.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de la connexion à la base de données: {ex.Message}");
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
