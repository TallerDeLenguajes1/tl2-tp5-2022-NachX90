using CadeteriaMVC.Models;

namespace CadeteriaMVC.Repositories
{
    public interface IPedidosRepository
    {
        void Crear(Pedido Pedido);
        void Editar(Pedido Pedido);
        void Eliminar(int Id);
        Pedido Obtener(int Id);
        List<Pedido> ObtenerTodos();
        List<Pedido> ObtenerPorCadete(int Id);
        List<Pedido> ObtenerPorCliente(int Id);
    }
}
