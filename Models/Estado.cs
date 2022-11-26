namespace CadeteriaMVC.Models;

public class Estado
{
    private uint id;
    private string estado;
    private string descripcion;

    public uint Id { get => id; set => id = value; }
    public string EstadoPedido { get => estado; set => estado = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public Estado()
    {

    }

    public Estado(uint Id, string Estado, string Descripcion)
    {
        id = Id;
        estado = Estado;
        descripcion = Descripcion;
    }
}
