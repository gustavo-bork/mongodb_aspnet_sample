using Carter;
using Microsoft.Extensions.Options;
using MongoDB_Test;
using MongoDB_Test.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SchoolDatabaseSettings>(builder.Configuration.GetSection(nameof(SchoolDatabaseSettings)));
builder.Services.AddSingleton<ISchoolDatabaseSettings>(provider => provider.GetRequiredService<IOptions<SchoolDatabaseSettings>>().Value);

builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<CourseService>();

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
