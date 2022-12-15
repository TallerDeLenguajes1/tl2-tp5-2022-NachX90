using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels.Pedidos;

public class EditarPedidoVM
{
    [Required]
    public uint Nro { get; set; }

    [Required]
    public uint IdCliente { get; set; }

    [Required(ErrorMessage = "El pedido es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Pedido")]
    public string Obs { get; set; }

    [Required(ErrorMessage = "El cliente es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Cliente")]
    public string Cliente { get; set; }

    [Display(Name = "Cadete a cargo")]
    public uint? IdCadete { get; set; }                     // IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL

    [Required(ErrorMessage = "El estado es obligatorio")]
    [Display(Name = "Estado")]
    public uint IdEstado { get; set; }

    public SelectList? ListaDeCadetesVM { get; set; }         // Sirve para mostrar el nombre en la edicion de pedidos. IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL

    public SelectList? ListaDeEstadosVM { get; set; }         // Sirve para mostrar el nombre en la edicion de pedidos. IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL
}