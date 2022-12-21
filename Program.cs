using CadeteriaMVC.Interfaces;
using CadeteriaMVC.Models;
using CadeteriaMVC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));                                    // Dependencia del AutoMapper
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();         // Dependencia para acceder a los datos de las sesiones
builder.Services.AddSingleton<IDBConnectionRepository, ConexionBDRepository>();     // Dependencia del repositorio de conexión a la DB
builder.Services.AddTransient<IDBRepository<Empleado>, EmpleadosRepository>();      // Dependencia del repositorio de empleados
builder.Services.AddTransient<IDBRepository<Empleado>, CadetesRepository>();        // Dependencia del repositorio de cadetes
builder.Services.AddTransient<IDBRepository<Cliente>, ClientesRepository>();        // Dependencia del repositorio de clientes
builder.Services.AddTransient<IPedidosRepository, PedidosRepository>();             // Dependencia del repositorio de pedidos
builder.Services.AddTransient<IDBRepository<Estado>, EstadosRepository>();          // Dependencia del repositorio de estados
builder.Services.AddTransient<IEmpleadosRepository, UsuariosRepository>();          // Dependencia del repositorio de usuarios

builder.Services.AddDistributedMemoryCache();                                       // Para almacenar la sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Inicio/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();                                                                   // Para usar las sesiones

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();
