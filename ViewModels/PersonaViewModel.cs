using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels;

public abstract class PersonaViewModel
{
    public uint Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [StringLength(45)]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Range(1100000000, 9999999999, ErrorMessage = "El teléfono debe tener 10 dígitos, sin 0 ni 15.")]
    [Display(Name = "Teléfono")]
    public ulong Telefono { get; set; }
}