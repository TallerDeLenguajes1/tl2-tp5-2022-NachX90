namespace CadeteriaMVC.Models;

public abstract class Persona
{
    private static uint contador = 0;
    private uint id;
    private string? nombre;
    private string? direccion;
    private ulong telefono;

    public static uint Contador { get => contador; set => contador = value; }
    public uint Id { get => id; set => id = value; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Direccion { get => direccion; set => direccion = value; }
    public ulong Telefono { get => telefono; set => telefono = value; }

    public Persona(string Nombre, string Direccion, ulong Telefono)
    {
        contador++;
        id = contador;
        nombre = Nombre;
        direccion = Direccion;
        telefono = Telefono;
    }
}