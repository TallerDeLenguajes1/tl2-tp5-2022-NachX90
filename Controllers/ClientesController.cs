using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoMapper;
using CadeteriaMVC.Repositories;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels.Clientes;
using CadeteriaMVC.Enums;

namespace CadeteriaMVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IMapper _mapper;
        private readonly IClientesRepository _clientesRepository;

        public ClientesController(ILogger<ClientesController> logger, IMapper mapper, IClientesRepository clientesRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _clientesRepository = clientesRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    return RedirectToAction("ListadoDeClientes");
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult ListadoDeClientes()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    var ListaDeClientes = _clientesRepository.ObtenerTodos();
                    var ListaDeClientesVM = _mapper.Map<List<ClienteVM>>(ListaDeClientes);
                    var ListadoDeClientesVM = new ListadoDeClientesVM();
                    ListadoDeClientesVM.ListaDeClientesVM = ListaDeClientesVM;
                    return View(ListadoDeClientesVM);
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult CrearCliente()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    return View(new CrearClienteVM());
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public IActionResult CrearCliente(CrearClienteVM CrearClienteVM)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    if (ModelState.IsValid)
                    {
                        var Cliente = _mapper.Map<Cliente>(CrearClienteVM);
                        _clientesRepository.Crear(Cliente);
                        return RedirectToAction("ListadoDeClientes");
                    }
                    else
                    {
                        return RedirectToAction("CrearCliente");
                    }
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult EliminarCliente(int Id)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    _clientesRepository.Eliminar(Id);
                    return RedirectToAction("ListadoDeClientes");
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult EditarCliente(int Id)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    var Cliente = _clientesRepository.Obtener(Id);
                    var EditarClienteVM = _mapper.Map<EditarClienteVM>(Cliente);
                    return View(EditarClienteVM);
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public IActionResult EditarCliente(EditarClienteVM EditarClienteVM)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    if (ModelState.IsValid)
                    {
                        var Cliente = _mapper.Map<Cliente>(EditarClienteVM);
                        _clientesRepository.Editar(Cliente);
                        return RedirectToAction("ListadoDeClientes");
                    }
                    else
                    {
                        return RedirectToAction("EditarCliente");
                    }
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }
    }
}
