namespace CadeteriaMVC.Models;

public class Pedido
{
    private int id;
    private string pedido;
    private int idCliente;
    private int idCadete;
    private int idEstado;
    private string cliente;
    private string cadete;
    private string estado;

    public int Id { get => id; set => id = value; }
    public string Descripcion { get => pedido; set => pedido = value; }
    public int IdCliente { get => idCliente; set => idCliente = value; }
    public int IdCadete { get => idCadete; set => idCadete = value; }
    public int IdEstado { get => idEstado; set => idEstado = value; }
    public string Cliente { get => cliente; set => cliente = value; }
    public string Cadete { get => cadete; set => cadete = value; }
    public string Estado { get => estado; set => estado = value; }

    public Pedido()
    {
    }
}
