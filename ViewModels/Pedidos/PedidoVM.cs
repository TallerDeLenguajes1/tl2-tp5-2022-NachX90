using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels.Pedidos;

public class PedidoVM
{
    public uint Nro { get; set; }

    [Required(ErrorMessage = "El pedido es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Pedido")]
    public string Obs { get; set; }

    [Required(ErrorMessage = "El cliente es obligatorio")]
    [Display(Name = "Cliente")]
    public uint IdCliente { get; set; }

    [Display(Name = "Cadete a cargo")]
    public uint? IdCadete { get; set; }                     // IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL

    [Required(ErrorMessage = "El estado es obligatorio")]
    [Display(Name = "Estado")]
    public uint IdEstado { get; set; }

    public string? Cliente { get; set; }                    // Sirve para mostrar el nombre en los listados de pedidos. IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL

    public string? Cadete { get; set; }                     // Sirve para mostrar el nombre en los listados de pedidos. IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL

    public string? Estado { get; set; }                     // Sirve para mostrar el nombre en los listados de pedidos. IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL

    public SelectList? ListaDeClientes { get; set; }        // Sirve para mostrar el nombre en la creacion/edicion de pedidos. IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL

    public SelectList? ListaDeCadetes { get; set; }         // Sirve para mostrar el nombre en la edicion de pedidos. IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL

    public SelectList? ListaDeEstados { get; set; }         // Sirve para mostrar el nombre en la creacion/edicion de pedidos. IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL
}