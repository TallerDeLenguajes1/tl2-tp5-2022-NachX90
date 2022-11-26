using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels;

public class PedidoViewModel
{
    public uint Nro { get; set; }

    [Required(ErrorMessage = "La observación es obligatoria")]
    [StringLength(45)]
    [Display(Name = "Observaciones")]
    public string Obs { get; set; }

    [Required(ErrorMessage = "El cliente es obligatorio")]
    [Display(Name = "Cliente")]
    public uint IdCliente { get; set; }

    [Display(Name = "Cadete a cargo")]
    public uint IdCadete { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio")]
    [Display(Name = "Estado")]
    public uint IdEstado { get; set; }

    public string? Estado { get; set; } // IMPORTANTE: debe ser nulleable sino no pasará la validación xq por defecto tendrá NULL
}