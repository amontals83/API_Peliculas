using API_Peliculas.Data;
using API_Peliculas.PeliculasMapper;
using API_Peliculas.Repositorio;
using API_Peliculas.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//4º PASO - CONFIGURAMOS LA CONEXION A SQL SERVER Y AGREGAMOS REPOSITORIOS Y AUTOMAPPER
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

//AGREGAMOS LOS REPOSITORIOS
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
//23º PASO
builder.Services.AddScoped<IPeliculaRepositorio, PeliculaRepositorio>();

//AGREGAMOS EL AUTOMAPPER
builder.Services.AddAutoMapper(typeof(PeliculasMapper));

// Add services to the container.

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
