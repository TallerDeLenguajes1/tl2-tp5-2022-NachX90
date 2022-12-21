namespace CadeteriaMVC.Models;

public abstract class Persona
{
    private int id;
    private string nombre;
    private string domicilio;
    private long telefono;

    public int Id { get => id; set => id = value; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Domicilio { get => domicilio; set => domicilio = value; }
    public long Telefono { get => telefono; set => telefono = value; }

    public Persona()
    {
    }
}
