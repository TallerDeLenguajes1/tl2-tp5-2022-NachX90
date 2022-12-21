using CadeteriaMVC.Models;
using System.Runtime.InteropServices;

namespace CadeteriaMVC.Interfaces;

public interface IPedidosRepository : IDBRepository<Pedido>
{
    List<Pedido> ObtenerPorCadete(int Id, [Optional] int IdUsuario);
    List<Pedido> ObtenerPorCliente(int Id, [Optional] int IdUsuario);
}
