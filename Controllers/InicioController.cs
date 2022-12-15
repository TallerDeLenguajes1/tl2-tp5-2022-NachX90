using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoMapper;
using CadeteriaMVC.Repositories;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels.Inicio;
using CadeteriaMVC.Enums;

namespace CadeteriaMVC.Controllers
{
    public class InicioController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsuariosRepository _usuariosRepository;

        public InicioController(ILogger<InicioController> logger, IMapper mapper, IUsuariosRepository usuariosRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    return View();
                case (int)Roles.Cadete:
                    return RedirectToAction("ListarPorCadete", "Pedidos", new { id = HttpContext.Session.GetInt32("IdCadete") });
                default:
                    return RedirectToAction("Ingresar");
            }
        }

        [HttpGet]
        public IActionResult Ingresar()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                case (int)Roles.Cadete:
                    return RedirectToAction("Index");
                default:
                    return View(new IngresarVM());
            }
        }

        [HttpPost]
        public IActionResult Ingresar(IngresarVM IngresarVM)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                case (int)Roles.Cadete:
                    return RedirectToAction("Index");
                default:
                    var Usuario = _mapper.Map<Usuario>(IngresarVM);
                    Usuario = _usuariosRepository.Verificar(Usuario);
                    if (Usuario.IdRol != (int)Roles.None)                   // El usuario fue verificado
                    {
                        HttpContext.Session.SetString("Nombre", Usuario.Nombre);
                        HttpContext.Session.SetString("Usuario", Usuario.Nickname);
                        HttpContext.Session.SetInt32("IdRol", Convert.ToInt32(Usuario.IdRol));
                        if (Usuario.IdRol == (int)Roles.Cadete)
                            HttpContext.Session.SetInt32("IdCadete", Convert.ToInt32(Usuario.IdCadete));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Los datos ingresados son incorrectos. Por favor intente nuevamente";
                        return RedirectToAction("Ingresar");
                    }
            }
        }

        [HttpGet]
        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Ingresar");
        }

        public IActionResult Privacidad()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                case (int)Roles.Cadete:
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                default:
                    return RedirectToAction("Ingresar");
            }
        }
    }
}