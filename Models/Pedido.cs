namespace CadeteriaMVC.Models;

public class Pedido
{
    private uint nro;
    private string obs;
    private uint idCliente;
    private uint idCadete;
    private uint idEstado;
    private string cliente;
    private string cadete;
    private string estado;

    public uint Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public uint IdCliente { get => idCliente; set => idCliente = value; }
    public uint IdCadete { get => idCadete; set => idCadete = value; }
    public uint IdEstado { get => idEstado; set => idEstado = value; }
    public string Cliente { get => cliente; set => cliente = value; }
    public string Cadete { get => cadete; set => cadete = value; }
    public string Estado { get => estado; set => estado = value; }

    public Pedido()
    {

    }
}
