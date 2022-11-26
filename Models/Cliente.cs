namespace CadeteriaMVC.Models;

public class Cliente
{
    private uint id;
    private string? nombre;
    private string? direccion;
    private string? referenciaDeDireccion;
    private ulong telefono;

    public uint Id { get => id; set => id = value; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Direccion { get => direccion; set => direccion = value; }
    public string? ReferenciaDeDireccion { get => referenciaDeDireccion; set => referenciaDeDireccion = value; }
    public ulong Telefono { get => telefono; set => telefono = value; }

    public Cliente()
    {

    }

    public Cliente(uint Id, string Nombre, string Direccion, string ReferenciaDeDireccion, ulong Telefono)
    {
        id = Id;
        nombre = Nombre;
        direccion = Direccion;
        referenciaDeDireccion= ReferenciaDeDireccion;
        telefono = Telefono;
    }
}
