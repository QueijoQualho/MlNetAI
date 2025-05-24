using Checkpoint_5_6.App.Services;
using Checkpoint_5_6.App.Services.Mappers;
using Checkpoint_5_6.Infra.Interfaces;
using Microsoft.ML;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<MLContext>();
builder.Services.AddAutoMapper(typeof(ModelMappingProfile));
builder.Services.AddScoped<IDataLoader, CsvDataLoader>();
builder.Services.AddScoped<IModelTrainer, ModelTrainer>();
builder.Services.AddScoped<IModelPredictor, ModelPredictor>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
