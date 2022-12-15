namespace CadeteriaMVC.Models;

public class Usuario
{
    private uint id;
    private string? nombre;
    private string? nickname;
    private string? contrasena;
    private uint idRol;
    private uint idCadete;

    public uint Id { get => id; set => id = value; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Nickname { get => nickname; set => nickname = value; }
    public string? Contrasena { get => contrasena; set => contrasena = value; }
    public uint IdRol { get => idRol; set => idRol = value; }
    public uint IdCadete { get => idCadete; set => idCadete = value; }
    
    public Usuario()
    {

    }

    public Usuario(string Nickname, string Contrasena)
    {
        nickname = Nickname;
        contrasena = Contrasena;
    }

    public Usuario(uint Id, string Nombre, string Nickname, string Contrasena, uint IdRol)
    {
        id = Id;
        nombre = Nombre;
        nickname = Nickname;
        contrasena = Contrasena;
        idRol = IdRol;
    }
}
