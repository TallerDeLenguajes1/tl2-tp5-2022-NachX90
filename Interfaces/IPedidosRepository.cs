using CadeteriaMVC.Models;

namespace CadeteriaMVC.Interfaces;

public interface IPedidosRepository : IDBRepository<Pedido>
{
    List<Pedido> ObtenerPorCadete(int Id);
    List<Pedido> ObtenerPorCliente(int Id);
}
