namespace CadeteriaMVC.Models;

public class Rol
{
    private uint id;
    private string rol;
    private string descripcion;

    public uint Id { get => id; set => id = value; }
    public string NombreRol { get => rol; set => rol = value; }
    public string DescripcionRol { get => descripcion; set => descripcion = value; }

    public Rol()
    {

    }

    public Rol(uint Id, string NombreRol, string DescripcionRol)
    {
        id = Id;
        rol = NombreRol;
        descripcion = DescripcionRol;
    }
}
