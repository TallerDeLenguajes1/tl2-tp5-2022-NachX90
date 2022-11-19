namespace CadeteriaMVC.Models;

public class Pedido
{
    private static uint contador = 0;
    private uint nro;
    private readonly string? obs;
    //private readonly Cliente cliente;
    private readonly string? cliente;
    private Estado estado;
    private uint idCadete;

    public static uint Contador { get => contador; set => contador = value; }
    public uint Nro { get => nro; set => nro = value; }
    public string? Obs => obs;
    //public Cliente Cliente => cliente;
    public string? Cliente => cliente;
    public Estado Estado { get => estado; set => estado = value; }
    public uint IdCadete { get => idCadete; set => idCadete = value; }

    //public Pedido(string? Obs, Cliente Cliente)
    public Pedido(string? Obs, string? Cliente)
    {
        contador++;
        nro = contador;
        obs = Obs;
        cliente = Cliente;
        estado = Estado.EnEspera;
    }
}
