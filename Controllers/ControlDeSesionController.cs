using Microsoft.AspNetCore.Mvc;
using CadeteriaMVC.Enums;

namespace CadeteriaMVC.Controllers;

public class ControlDeSesionController : Controller
{
    public ControlDeSesionController()
    {
    }

    protected bool ExisteSesion() => HttpContext.Session.GetInt32("IdRol") > 0;
    protected bool EsAdministrador() => HttpContext.Session.GetInt32("IdRol") == (int)Roles.Administrador;
    protected bool EsVendedor() => HttpContext.Session.GetInt32("IdRol") == (int)Roles.Vendedor;
    protected bool EsCadete() => HttpContext.Session.GetInt32("IdRol") == (int)Roles.Cadete;
    protected int IdUsuario() => (int)HttpContext.Session.GetInt32("Id");
}
