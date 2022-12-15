using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels.Pedidos;

public class CrearPedidoVM
{
    [Required(ErrorMessage = "El pedido es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Pedido")]
    public string Obs { get; set; }

    [Required(ErrorMessage = "El cliente es obligatorio")]
    [Display(Name = "Cliente")]
    public uint IdCliente { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio")]
    [Display(Name = "Estado")]
    public uint IdEstado { get; set; }

    public SelectList? ListaDeClientesVM { get; set; }        // Sirve para mostrar el nombre en la creacion de pedidos. IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL
}