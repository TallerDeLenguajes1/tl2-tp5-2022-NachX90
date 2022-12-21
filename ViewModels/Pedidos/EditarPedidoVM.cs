using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels.Pedidos;

public class EditarPedidoVM
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "El pedido es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Pedido")]
    public string Descripcion { get; set; }

    [Required]
    public int IdCliente { get; set; }

    [Display(Name = "Cadete a cargo")]
    public int? IdCadete { get; set; }                      // IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL

    [Required]
    [Display(Name = "Estado")]
    public int IdEstado { get; set; }

    [Display(Name = "Cliente")]
    public string Cliente { get; set; }

    public SelectList? ListaDeCadetesVM { get; set; }        // Sirve para mostrar el nombre

    public SelectList? ListaDeEstadosVM { get; set; }        // Sirve para mostrar el nombre
}