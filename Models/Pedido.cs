namespace CadeteriaMVC.Models;

public class Pedido
{
    private uint nro;
    private string obs;
    private uint idCliente;
    private uint idCadete;
    private uint idEstado;
    private string estado;

    public uint Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public uint IdCliente { get => idCliente; set => idCliente = value; }
    public uint IdCadete { get => idCadete; set => idCadete = value; }
    public uint IdEstado { get => idEstado; set => idEstado = value; }
    public string Estado { get => estado; set => estado = value; }

    public Pedido()
    {

    }

    public Pedido(uint Nro, string Obs, uint IdCliente)
    {
        nro = Nro;
        obs = Obs;
        idCliente = IdCliente;
        idEstado = 1;
    }

    public Pedido(uint Nro, string Obs, uint IdCliente, uint IdCadete, uint IdEstado, string Estado)
    {
        nro = Nro;
        obs = Obs;
        idCliente = IdCliente;
        idCadete = IdCadete;
        idEstado = IdEstado;
        estado = Estado;
    }
}
