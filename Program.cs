using CadeteriaMVC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));                                // Dependencia del AutoMapper
builder.Services.AddSingleton<IConexionBDRepository, ConexionBDRepository>();   // Dependencia de la conexion SQLite
builder.Services.AddTransient<ICadetesRepository, CadetesRepository>();         // Dependencia del repositorio de cadetes
builder.Services.AddTransient<IClientesRepository, ClientesRepository>();       // Dependencia del repositorio de clientes
builder.Services.AddTransient<IPedidosRepository, PedidosRepository>();         // Dependencia del repositorio de pedidos
builder.Services.AddTransient<IEstadosRepository, EstadosRepository>();         // Dependencia del repositorio de estados

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
