using System.ComponentModel.DataAnnotations;

namespace CadeteriaMVC.ViewModels.Pedidos;

public class ListarPorClienteVM
{
    [Required]
    public string NombreCliente { get; set; }
    public List<PedidoVM>? ListaDePedidosVM { get; set; }       // Puede ser null porque puede devolver una lista vacía
}