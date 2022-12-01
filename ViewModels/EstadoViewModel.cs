using System.ComponentModel.DataAnnotations;
namespace CadeteriaMVC.ViewModels;

public class EstadoViewModel
{
    public uint Id { get; set; }
    public string EstadoPedido { get; set; }
    public string Descripcion { get; set; }
}