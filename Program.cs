using API_Peliculas.Data;
using API_Peliculas.PeliculasMapper;
using API_Peliculas.Repositorio;
using API_Peliculas.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//4º PASO - CONFIGURAMOS LA CONEXION A SQL SERVER Y AGREGAMOS REPOSITORIOS Y AUTOMAPPER -> siguiente paso es un txt
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

//AGREGAMOS LOS REPOSITORIOS
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
builder.Services.AddScoped<IPeliculaRepositorio, PeliculaRepositorio>(); //24º PASO
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>(); //38º PASO

//AGREGAMOS EL AUTOMAPPER
builder.Services.AddAutoMapper(typeof(PeliculasMapper));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SOPORTE PARA CORS
//Se pueden habilitar: 1-Un dominnio, 2-multiples dominios, 3- cualquier dominio (cuidado la seguridad)
//Usamos de ejemplo el dominio http://localhost:3223, se debe cambiar por el correcto
//Se usa (*) para todos los dominios.
builder.Services.AddCors(p => p.AddPolicy("PolicyCors", build =>
{
    //CON ESTO HACEMOS QUE SOLO LOS QUE ESTEN EN ESE DOMINIO PODRAN CONSUMIR LA API
    //build.WithOrigins("http://localhost:3223").AllowAnyMethod().AllowAnyHeader(); //EJEMPLO 1
    //build.WithOrigins("http://localhost:3223", "http://localhost:5445").AllowAnyMethod().AllowAnyHeader(); //EJEMPLO 2
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); //EJEMPLO 3
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//SOPORTE PARA CORS
app.UseCors("PolicyCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
