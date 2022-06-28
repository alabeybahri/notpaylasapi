//using Project.Filters;
//using Project.Middleware;
using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Filters;
using Project.Repositories;
using Project.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<INoteRepo, NoteRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Token",
//        policy => policy.Requirements.Add(new CustomAuthorization()));
//});

//builder.Services.AddControllers(config =>
//{
//    config.Filters.Add(new CustomAuthorization());
//});

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connString));

var app = builder.Build();
app.UseCors("corsapp");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<TokenMiddleware>();
app.MapControllers();

app.Run();
