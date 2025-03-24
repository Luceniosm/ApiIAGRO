using IAGRO.Application.Interfaces;
using IAGRO.Application.Services;
using IAGRO.Domain.DBO;
using IAGRO.Domain.Dtos;
using IAGRO.Domain.Interfaces;
using IAGRO.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Load Context Books
var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DBO", "books.json");
builder.Services.AddSingleton(new BookContext(jsonFilePath));

//Services
builder.Services.AddScoped<IBookAppService, BookAppService>();
//Repositories
builder.Services.AddTransient<IBookRepository, BookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/books", async ([FromServices]IBookAppService _bookAppService, [FromBody]BookFilterRequest filterRequest) =>
{
    var result = await _bookAppService.GetBooksAsync(filterRequest);
    return result.Success? 
        Results.Ok(result) :
        Results.BadRequest(result.Erros);
});

app.MapGet("/books/calculateShipping/{id}", async ([FromServices] IBookAppService _bookAppService, [FromRoute] int id) =>
{
    var result = await _bookAppService.CalculateShipping(id);
    return result.Success? 
        Results.Ok(result) :
        Results.NotFound(result.Erros);
});

app.Run();

