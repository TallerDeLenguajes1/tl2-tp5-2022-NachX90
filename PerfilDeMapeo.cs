using AutoMapper;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels.Inicio;
using CadeteriaMVC.ViewModels.Cadetes;
using CadeteriaMVC.ViewModels.Clientes;
using CadeteriaMVC.ViewModels.Pedidos;
using CadeteriaMVC.ViewModels.Estados;

public class PerfilDeMapeo : Profile
{
    public PerfilDeMapeo()
    {
        // InicioController
        CreateMap<Empleado, EmpleadoVM>().ReverseMap();
        CreateMap<Empleado, IngresarVM>().ReverseMap();

        // CadetesController
        CreateMap<Empleado, CadeteVM>().ReverseMap();
        CreateMap<Empleado, CrearCadeteVM>().ReverseMap();
        CreateMap<Empleado, EditarCadeteVM>().ReverseMap();

        // ClientesController
        CreateMap<Cliente, ClienteVM>().ReverseMap();
        CreateMap<Cliente, CrearClienteVM>().ReverseMap();
        CreateMap<Cliente, EditarClienteVM>().ReverseMap();

        // PedidosController
        CreateMap<Pedido, PedidoVM>().ReverseMap();
        CreateMap<Pedido, CrearPedidoVM>().ReverseMap();
        CreateMap<Pedido, EditarPedidoVM>().ReverseMap();
        CreateMap<Pedido, CambiarEstadoPedidoVM>().ReverseMap();

        // EstadosController
        CreateMap<Estado, EstadoVM>().ReverseMap();
        CreateMap<Estado, CrearEstadoVM>().ReverseMap();
        CreateMap<Estado, EditarEstadoVM>().ReverseMap();

        // UsuariosController
        //CreateMap<Usuario, IngresarVM>().ReverseMap();
    }
}