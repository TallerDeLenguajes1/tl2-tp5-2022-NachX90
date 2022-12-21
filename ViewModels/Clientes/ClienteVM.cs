namespace CadeteriaMVC.ViewModels.Clientes;

public class ClienteVM
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Domicilio { get; set; }
    public string? Referencia { get; set; }
    public long Telefono { get; set; }
}