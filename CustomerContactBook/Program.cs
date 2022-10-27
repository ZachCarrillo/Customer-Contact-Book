using Microsoft.EntityFrameworkCore;
using CustomerContactBook.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CustomerContext>(opt =>
               opt.UseInMemoryDatabase("CustomerList"));
builder.Services.AddDbContext<CustomerGroupContext>(opt =>
               opt.UseInMemoryDatabase("GroupList"));
builder.Services.AddDbContext<GroupMemberContext>(opt =>
               opt.UseInMemoryDatabase("GroupMemberList"));
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


app.MapControllers();

app.Run();
