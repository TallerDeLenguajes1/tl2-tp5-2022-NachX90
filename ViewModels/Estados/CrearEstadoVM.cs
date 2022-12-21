using System.ComponentModel.DataAnnotations;

namespace CadeteriaMVC.ViewModels.Estados;

public class CrearEstadoVM
{
    [Required(ErrorMessage = "El estado es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Estado")]
    public string NombreEstado { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria")]
    [StringLength(90)]
    [Display(Name = "Descripcion")]
    public string Descripcion { get; set; }
}