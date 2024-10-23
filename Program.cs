using MappingByAutoMapper;
using MappingByAutoMapper.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddAutoMapper(typeof(Mapper));
builder.Services.AddDbContext<DataContext>(x =>
    x.UseNpgsql(builder.Configuration["ConnectionString"]));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
