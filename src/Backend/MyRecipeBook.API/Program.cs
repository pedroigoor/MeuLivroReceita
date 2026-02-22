using MyRecipeBook.API.Config;
using MyRecipeBook.Application;
using MyRecipeBook.Infrastructe;
using MyRecipeBook.Infrastructe.Extensions;
using MyRecipeBook.Infrastructe.Migrations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructe(builder.Configuration);

builder.Services.AddApplication();
builder.Services.AddApi();





builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.RegisterMiddlewares();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    if (builder.Configuration.IsUnitTestEnviroment())
    {
        return;
    }

    var connetionString = builder.Configuration.ConnectionString();
    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    DataBaseMigration.Migrate(connetionString, serviceScope.ServiceProvider);
}

public partial class Program {

    protected Program() { }
}
