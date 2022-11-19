namespace CadeteriaMVC.Models;

public class Cadete : Persona
{
    private List<Pedido> listaDePedidos;

    public List<Pedido> ListaDePedidos { get => listaDePedidos; set => listaDePedidos = value; }

    public Cadete(string Nombre, string Direccion, ulong Telefono) : base(Nombre, Direccion, Telefono)
    {
        listaDePedidos = new List<Pedido>();
    }

    public void AsignarPedido(Pedido Pedido)
    {
        ListaDePedidos.Add(Pedido);
        Pedido.Estado = Estado.Asignado;
    }
}
