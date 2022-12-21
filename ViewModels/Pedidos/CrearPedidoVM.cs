using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels.Pedidos;

public class CrearPedidoVM
{
    [Required(ErrorMessage = "El pedido es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Pedido")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El cliente es obligatorio")]
    [Display(Name = "Cliente")]
    public int IdCliente { get; set; }

    public int IdEstado { get; set; }

    public SelectList? ListaDeClientesVM { get; set; }
}