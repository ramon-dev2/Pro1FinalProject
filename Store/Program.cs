using DataAccess.DependencyInjection;
using BusinessLogic.DependencyInjection;
using BusinessLogic.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddRinLogger();
builder.Services.AddRin();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(ProductProfile), typeof(CustomerProfile), typeof(StatusProfile), typeof(OrderProfile));

// Configure Repositories
builder.Services.AddRepository(builder.Configuration);

// Configure Services
builder.Services.AddService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseRin();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseRinDiagnosticsHandler();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
