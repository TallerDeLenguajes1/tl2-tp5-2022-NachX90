namespace CadeteriaMVC.Models;

public class Empleado : Persona
{
    private string usuario;
    private string contrasena;
    private int idrol;
    private string rol;

    public string Usuario { get => usuario; set => usuario = value; }
    public string Contrasena { get => contrasena; set => contrasena = value; }
    public int IDRol { get => idrol; set => idrol = value; }
    public string Rol { get => rol; set => rol = value; }

    public Empleado()
    {
    }
}
