using Microsoft.AspNetCore.Mvc.Rendering;
namespace CadeteriaMVC.ViewModels.Pedidos;

public class PedidoVM
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    //public int IdCliente { get; set; }
    //public int? IdCadete { get; set; }
    //public int IdEstado { get; set; }
    public string Cliente { get; set; }                    // Sirve para mostrar el nombre en los listados de pedidos.
    public string Cadete { get; set; }                     // Sirve para mostrar el nombre en los listados de pedidos.
    public string Estado { get; set; }                     // Sirve para mostrar el nombre en los listados de pedidos.
    //public SelectList? ListaDeClientes { get; set; }        // Sirve para mostrar el nombre en la creacion/edicion de pedidos.
    //public SelectList? ListaDeCadetes { get; set; }         // Sirve para mostrar el nombre en la creacion/edicion de pedidos.
    //public SelectList? ListaDeEstados { get; set; }         // Sirve para mostrar el nombre en la creacion/edicion de pedidos.
}