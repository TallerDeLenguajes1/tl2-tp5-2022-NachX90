namespace CadeteriaMVC.Models;

public class Cliente : Persona
{
    private string? referencia;

    public string? Referencia { get => referencia; set => referencia = value; }

    public Cliente()
    {
    }
}
