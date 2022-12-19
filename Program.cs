using ApiInventoryControl.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventoryDataContext>();

var app = builder.Build();
app.MapControllers();

app.Run();
