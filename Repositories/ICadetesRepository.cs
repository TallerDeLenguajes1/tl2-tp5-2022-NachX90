using CadeteriaMVC.Models;

namespace CadeteriaMVC.Repositories
{
    public interface ICadetesRepository
    {
        void Crear(Cadete Cadete);
        void Editar(Cadete Cadete);
        void Eliminar(int Id);
        Cadete Obtener(int Id);
        List<Cadete> ObtenerTodos();
    }
}
