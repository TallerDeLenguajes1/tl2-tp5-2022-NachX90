using System.ComponentModel.DataAnnotations;

namespace CadeteriaMVC.ViewModels.Inicio;

public class IngresarVM
{
    [Required(ErrorMessage = "El usuario es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Usuario")]
    public string Usuario { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    [StringLength(45)]
    [Display(Name = "Contraseña")]
    public string Contrasena { get; set; }

    public List<EmpleadoVM> ListaDeEmpleadosVM { get; set; }
}