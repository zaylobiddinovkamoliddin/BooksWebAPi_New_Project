using AutoMapper;
using BooksWepAPiDtos;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repasitories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cors = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(cors,
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ICategoryInterface, CategoryRepasitory>();
builder.Services.AddTransient<IBookInterface, BookRepasitory>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

#region
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
