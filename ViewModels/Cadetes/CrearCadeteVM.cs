using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels.Cadetes;

public class CrearCadeteVM
{
    [Required]
    public int IDRol { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El domicilio es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Domicilio")]
    public string Domicilio { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Range(1100000000, 3894999999, ErrorMessage = "El teléfono debe tener 10 dígitos, sin 0 ni 15.")]
    [Display(Name = "Teléfono")]
    public long Telefono { get; set; }

    [Required(ErrorMessage = "El usuario es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Usuario")]
    public string Usuario { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(45)]
    [Display(Name = "Contraseña")]
    public string Contrasena { get; set; }
}