using System.Runtime.InteropServices;

namespace CadeteriaMVC.Interfaces;

public interface IDBRepository<T>
{
    void Alta(T Objeto, [Optional] int IdUsuario);
    void BajaLogica(int Id, [Optional] int IdUsuario);
    void Modificacion(T Objeto, [Optional] int IdUsuario);
    int ObtenerID(T Objeto);
    T ObtenerPorID(int Id, [Optional] int IdUsuario);
    List<T> ObtenerTodos([Optional] int IdUsuario);
}
