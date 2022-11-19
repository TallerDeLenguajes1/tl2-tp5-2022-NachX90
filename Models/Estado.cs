namespace CadeteriaMVC.Models;

public enum Estado
{
    EnEspera,       // El cliente realiza el pedido y está a la espera de la acreditación
    Aprobado,       // Se acreditó el pago del pedido
    Rechazado,      // Se rechazó el pago del pedido
    Asignado,       // Se acreditó el pago del pedido y el cadete fue asignado para su entrega
    Entregado,      // El cadete entregó el pedido
    SinEntregar     // El cadete no pudo entregar el pedido. Devuelto a la sucursal
}
