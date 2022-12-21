using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels.Clientes;
using CadeteriaMVC.Interfaces;
using CadeteriaMVC.Enums;

namespace CadeteriaMVC.Controllers
{
    public class ClientesController : ControlDeSesionController
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IMapper _mapper;
        private readonly IDBRepository<Cliente> _clientesRepository;

        public ClientesController(ILogger<ClientesController> logger, IMapper mapper, IDBRepository<Cliente> clientesRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _clientesRepository = clientesRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (EsAdministrador() || EsVendedor())
                return RedirectToAction("ListadoDeClientes");
            else
            {
                TempData["Error"] = Mensajes.MostrarError(Errores.AccesoDenegado);
                return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult ListadoDeClientes()
        {
            if (EsAdministrador() || EsVendedor())
            {
                try
                {
                    var ListaDeClientes = _clientesRepository.ObtenerTodos();
                    var ListaDeClientesVM = _mapper.Map<List<ClienteVM>>(ListaDeClientes);
                    var ListadoDeClientesVM = new ListadoDeClientesVM();
                    ListadoDeClientesVM.ListaDeClientesVM = ListaDeClientesVM;
                    return View(ListadoDeClientesVM);
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
            {
                TempData["Error"] = Mensajes.MostrarError(Errores.AccesoDenegado);
                return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult CrearCliente()
        {
            if (EsAdministrador() || EsVendedor())
                return View(new CrearClienteVM());
            else
            {
                TempData["Error"] = Mensajes.MostrarError(Errores.AccesoDenegado);
                return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public IActionResult CrearCliente(CrearClienteVM CrearClienteVM)
        {
            if (EsAdministrador() || EsVendedor())
                if (ModelState.IsValid)
                {
                    try
                    {
                        var Cliente = _mapper.Map<Cliente>(CrearClienteVM);
                        _clientesRepository.Alta(Cliente);
                        return RedirectToAction("ListadoDeClientes");
                    }
                    catch (Exception Ex)
                    {
                        TempData["Error"] = Ex.Message;
                        return View("ErrorControlado");
                    }
                }
                else
                {
                    TempData["Error"] = Mensajes.MostrarError(Errores.ModeloInvalido);
                    return RedirectToAction("CrearCliente");
                }
            else
            {
                TempData["Error"] = Mensajes.MostrarError(Errores.AccesoDenegado);
                return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult EliminarCliente(int Id)
        {
            if (EsAdministrador() || EsVendedor())
            {
                try
                {
                    _clientesRepository.BajaLogica(Id);
                    return RedirectToAction("ListadoDeClientes");
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
            {
                TempData["Error"] = Mensajes.MostrarError(Errores.AccesoDenegado);
                return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult EditarCliente(int Id)
        {
            if (EsAdministrador() || EsVendedor())
            {
                try
                {
                    var Cliente = _clientesRepository.ObtenerPorID(Id);
                    var EditarClienteVM = _mapper.Map<EditarClienteVM>(Cliente);
                    return View(EditarClienteVM);
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
            {
                TempData["Error"] = Mensajes.MostrarError(Errores.AccesoDenegado);
                return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public IActionResult EditarCliente(EditarClienteVM EditarClienteVM)
        {
            if (EsAdministrador() || EsVendedor())
                if (ModelState.IsValid)
                {
                    try
                    {
                        var Cliente = _mapper.Map<Cliente>(EditarClienteVM);
                        _clientesRepository.Modificacion(Cliente);
                        return RedirectToAction("ListadoDeClientes");
                    }
                    catch (Exception Ex)
                    {
                        TempData["Error"] = Ex.Message;
                        return View("ErrorControlado");
                    }
                }
                else
                {
                    TempData["Error"] = Mensajes.MostrarError(Errores.ModeloInvalido);
                    return RedirectToAction("ListadoDeClientes");
                }
            else
            {
                TempData["Error"] = Mensajes.MostrarError(Errores.AccesoDenegado);
                return RedirectToAction("Index", "Inicio");
            }
        }
    }
}
