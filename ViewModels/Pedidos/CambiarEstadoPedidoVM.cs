using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels.Pedidos;

public class CambiarEstadoPedidoVM
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int IdCliente { get; set; }

    [Required]
    public int IdCadete { get; set; }

    [Required]
    [StringLength(45)]
    [Display(Name = "Pedido")]
    public string Descripcion { get; set; }

    [Required]
    [Display(Name = "Estado")]
    public int IdEstado { get; set; }

    [Display(Name = "Cliente")]
    public string? Cliente { get; set; }

    public SelectList? ListaDeEstadosVM { get; set; }         // Sirve para mostrar el nombre
}