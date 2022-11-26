namespace CadeteriaMVC.Models;

public class Cadete
{
    private uint id;
    private string? nombre;
    private string? direccion;
    private ulong telefono;

    public uint Id { get => id; set => id = value; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Direccion { get => direccion; set => direccion = value; }
    public ulong Telefono { get => telefono; set => telefono = value; }

    public Cadete()
    {

    }

    public Cadete(uint Id, string Nombre, string Direccion, ulong Telefono)
    {
        id = Id;
        nombre = Nombre;
        direccion = Direccion;
        telefono = Telefono;
    }
}
