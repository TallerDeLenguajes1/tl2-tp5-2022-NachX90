using AutoMapper;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels;

public class PerfilDeMapeo : Profile
{
    public PerfilDeMapeo()
    {
        CreateMap<Cadete, CadeteViewModel>().ReverseMap();
        CreateMap<Cliente, ClienteViewModel>().ReverseMap();
        CreateMap<Pedido, PedidoViewModel>().ReverseMap();
        CreateMap<Estado, EstadoViewModel>().ReverseMap();
    }
}