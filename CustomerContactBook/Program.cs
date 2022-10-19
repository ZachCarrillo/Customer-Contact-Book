using Microsoft.EntityFrameworkCore;
using CustomerContactBook.Models;
using CustomerContactBook.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ContactBookContext>(opt =>
               opt.UseInMemoryDatabase("ContactBook"));
builder.Services.AddScoped<CustomersService>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<MembersService>();
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
