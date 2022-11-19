using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels;

public class PedidoViewModel
{
    public uint Nro { get; set; }

    [Required(ErrorMessage = "La observación es obligatoria")]
    [StringLength(45)]
    [Display(Name = "Observaciones")]
    public string? Obs { get; set; }

    //[Required(ErrorMessage = "El cliente es obligatorio")]
    [Display(Name = "Cliente")]
    //public ClienteViewModel? ClienteViewModel { get; set; }
    public string? Cliente { get; set; }

    //[Required(ErrorMessage = "El estado es obligatorio")]
    [Display(Name = "Estado")]
    public EstadoViewModel EstadoViewModel { get; set; }

    //[Required(ErrorMessage = "El cadete es obligatorio")]
    [Display(Name = "Cadete a cargo")]
    public uint IdCadete { get; set; }
}