namespace CadeteriaMVC.Models;

public class Cliente : Persona
{
    private string? referenciaDeDireccion;

    public string? ReferenciaDeDireccion { get => referenciaDeDireccion; set => referenciaDeDireccion = value; }

    public Cliente(string Nombre, string Direccion, ulong Telefono, string ReferenciaDeDireccion) : base(Nombre, Direccion, Telefono)
    {
        referenciaDeDireccion = ReferenciaDeDireccion;
    }
}
