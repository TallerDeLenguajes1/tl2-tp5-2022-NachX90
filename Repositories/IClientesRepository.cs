using CadeteriaMVC.Models;

namespace CadeteriaMVC.Repositories
{
    public interface IClientesRepository
    {
        void Crear(Cliente Cliente);
        void Editar(Cliente Cliente);
        void Eliminar(int Id);
        Cliente Obtener(int Id);
        List<Cliente> ObtenerTodos();
    }
}
