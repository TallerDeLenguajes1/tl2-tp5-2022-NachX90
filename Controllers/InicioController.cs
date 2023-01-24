using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoMapper;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels.Inicio;
using CadeteriaMVC.Enums;
using CadeteriaMVC.Interfaces;

namespace CadeteriaMVC.Controllers;

public class InicioController : ControlDeSesionController
{
    private readonly ILogger<InicioController> _logger;
    private readonly IMapper _mapper;
    private readonly IEmpleadosRepository _usuariosRepository;

    public InicioController(ILogger<InicioController> logger, IMapper mapper, IEmpleadosRepository usuariosRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _usuariosRepository = usuariosRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        if (ExisteSesion())
            return View();
        else
            return RedirectToAction("Ingresar");
    }

    [HttpGet]
    public IActionResult Ingresar()
    {
        if (ExisteSesion())
            return RedirectToAction("Index");
        else
        {
            // La lista de empleados es solo para la presentación. Se debe eliminar antes de la producción.
            try
            {
                var IngresarVM = new IngresarVM();
                var ListaDeEmpleados = _usuariosRepository.ObtenerTodos();
                var ListaDeEmpleadosVM = _mapper.Map<List<EmpleadoVM>>(ListaDeEmpleados);
                IngresarVM.ListaDeEmpleadosVM = ListaDeEmpleadosVM;
                return View(IngresarVM);
            }
            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
                return View("ErrorControlado");
            }
        }
    }

    [HttpPost]
    public IActionResult Ingresar(IngresarVM IngresarVM)
    {
        if (ExisteSesion())
            return RedirectToAction("Index");
        else
        {
            try
            {
                var Empleado = _mapper.Map<Empleado>(IngresarVM);
                Empleado = _usuariosRepository.Verificar(Empleado);
                if (Empleado.IDRol == (int)Roles.Administrador || Empleado.IDRol == (int)Roles.Vendedor || Empleado.IDRol == (int)Roles.Cadete)
                {
                    HttpContext.Session.SetInt32("Id", Empleado.Id);
                    HttpContext.Session.SetString("Nombre", Empleado.Nombre);
                    HttpContext.Session.SetString("Usuario", Empleado.Usuario);
                    HttpContext.Session.SetInt32("IdRol", Empleado.IDRol);
                    HttpContext.Session.SetString("Rol", Empleado.Rol);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = Mensajes.MostrarError(Errores.LogueoIncorrecto);
                    return RedirectToAction("Ingresar");
                }
            }
            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
                return View("ErrorControlado");
            }
        }
    }

    [HttpGet]
    public IActionResult Salir()
    {
        if (ExisteSesion())
        {
            HttpContext.Session.Clear();
            TempData["Error"] = Mensajes.MostrarError(Errores.CerrarSesionExito);
        }
        else
            TempData["Error"] = Mensajes.MostrarError(Errores.CerrarSesionCerrada);
        return RedirectToAction("Ingresar");
    }

    public IActionResult Privacidad()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        if (ExisteSesion())
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        else
            return RedirectToAction("Ingresar");
    }
}