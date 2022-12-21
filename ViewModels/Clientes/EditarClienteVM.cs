using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels.Clientes;

public class EditarClienteVM
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El domicilio es obligatorio")]
    [StringLength(45)]
    [Display(Name = "Domicilio")]
    public string Domicilio { get; set; }

    [StringLength(45)]
    [Display(Name = "Referencia de domicilio")]
    public string? Referencia { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Range(1100000000, 3894999999, ErrorMessage = "El teléfono debe tener 10 dígitos, sin 0 ni 15.")]
    [Display(Name = "Teléfono")]
    public long Telefono { get; set; }
}