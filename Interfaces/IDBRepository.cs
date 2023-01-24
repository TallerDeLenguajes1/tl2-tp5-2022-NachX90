namespace CadeteriaMVC.Interfaces;

public interface IDBRepository<T>
{
    void Alta(T Objeto);
    void BajaLogica(int Id);
    void Modificacion(T Objeto);
    int ObtenerID(T Objeto);
    T ObtenerPorID(int Id);
    List<T> ObtenerTodos();
}
