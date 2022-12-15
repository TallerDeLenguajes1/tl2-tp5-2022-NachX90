using AutoMapper;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels.Cadetes;
using CadeteriaMVC.ViewModels.Clientes;
using CadeteriaMVC.ViewModels.Pedidos;
using CadeteriaMVC.ViewModels.Estados;
using CadeteriaMVC.ViewModels.Inicio;

public class PerfilDeMapeo : Profile
{
    public PerfilDeMapeo()
    {
        CreateMap<Cadete, CadeteVM>().ReverseMap();
        CreateMap<Cadete, CrearCadeteVM>().ReverseMap();
        CreateMap<Cadete, EditarCadeteVM>().ReverseMap();
        CreateMap<Cliente, ClienteVM>().ReverseMap();
        CreateMap<Cliente, CrearClienteVM>().ReverseMap();
        CreateMap<Cliente, EditarClienteVM>().ReverseMap();
        CreateMap<Pedido, PedidoVM>().ReverseMap();
        CreateMap<Pedido, CrearPedidoVM>().ReverseMap();
        CreateMap<Pedido, EditarPedidoVM>().ReverseMap();
        CreateMap<Estado, EstadoVM>().ReverseMap();
        CreateMap<Usuario, IngresarVM>().ReverseMap();
    }
}