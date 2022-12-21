using CadeteriaMVC.Models;

namespace CadeteriaMVC.Interfaces;

public interface IEmpleadosRepository : IDBRepository<Empleado>
{
    Empleado Verificar(Empleado Objeto);
}
