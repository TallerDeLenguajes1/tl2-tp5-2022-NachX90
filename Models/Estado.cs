namespace CadeteriaMVC.Models;

public class Estado
{
    private int id;
    private string estado;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public string NombreEstado { get => estado; set => estado = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public Estado()
    {
    }
}
