using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels.Cadetes;
using CadeteriaMVC.ViewModels.Clientes;
using CadeteriaMVC.ViewModels.Estados;
using CadeteriaMVC.ViewModels.Pedidos;
using CadeteriaMVC.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using CadeteriaMVC.Enums;

namespace CadeteriaMVC.Controllers
{
    public class PedidosController : ControlDeSesionController
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IMapper _mapper;
        private readonly IPedidosRepository _pedidosRepository;
        private readonly IDBRepository<Cliente> _clientesRepository;
        private readonly IDBRepository<Empleado> _cadetesRepository;
        private readonly IDBRepository<Estado> _estadosRepository;

        public PedidosController(ILogger<PedidosController> logger, IMapper mapper, IPedidosRepository pedidosRepository, IDBRepository<Cliente> clientesRepository, IDBRepository<Empleado> cadetesRepository, IDBRepository<Estado> estadosRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _pedidosRepository = pedidosRepository;
            _clientesRepository = clientesRepository;
            _cadetesRepository = cadetesRepository;
            _estadosRepository = estadosRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (EsAdministrador() || EsVendedor())
                return RedirectToAction("ListadoDePedidos");
            else if (EsCadete())
                return RedirectToAction("ListarPorCadete", new { id = HttpContext.Session.GetInt32("Id") });
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult ListadoDePedidos()
        {
            if (EsAdministrador() || EsVendedor())
            {
                try
                {
                    var ListaDePedidos = _pedidosRepository.ObtenerTodos();
                    var ListaDePedidosVM = _mapper.Map<List<PedidoVM>>(ListaDePedidos);
                    var ListadoDePedidosVM = new ListadoDePedidosVM();
                    ListadoDePedidosVM.ListaDePedidosVM = ListaDePedidosVM;
                    return View(ListadoDePedidosVM);
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult CrearPedido()
        {
            if (EsAdministrador() || EsVendedor())
            {
                try
                {
                    var CrearPedidoVM = new CrearPedidoVM();

                    var ListaDeClientes = _clientesRepository.ObtenerTodos();
                    var ListaDeClientesVM = _mapper.Map<List<ClienteVM>>(ListaDeClientes);
                    CrearPedidoVM.ListaDeClientesVM = new SelectList(ListaDeClientesVM, "Id", "Nombre");

                    return View(CrearPedidoVM);
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
                return View("AccesoDenegado");
        }

        [HttpPost]
        public IActionResult CrearPedido(CrearPedidoVM CrearPedidoVM)
        {
            if (EsAdministrador() || EsVendedor())
                if (ModelState.IsValid)
                {
                    try
                    {
                        CrearPedidoVM.IdEstado = 1;
                        var Pedido = _mapper.Map<Pedido>(CrearPedidoVM);
                        _pedidosRepository.Alta(Pedido);
                        return RedirectToAction("ListadoDePedidos");
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
                    return RedirectToAction("CrearPedido");
                }
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult EliminarPedido(int Id)
        {
            if (EsAdministrador() || EsVendedor())
            {
                try
                {
                    _pedidosRepository.BajaLogica(Id);
                    return RedirectToAction("ListadoDePedidos");
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult EditarPedido(int Id)
        {
            if (EsAdministrador() || EsVendedor())
            {
                try
                {
                    var Pedido = _pedidosRepository.ObtenerPorID(Id);
                    var EditarPedidoVM = _mapper.Map<EditarPedidoVM>(Pedido);

                    var ListaDeCadetes = _cadetesRepository.ObtenerTodos();
                    var ListaDeCadetesVM = _mapper.Map<List<CadeteVM>>(ListaDeCadetes);
                    EditarPedidoVM.ListaDeCadetesVM = new SelectList(ListaDeCadetesVM, "Id", "Nombre");

                    var ListaDeEstados = _estadosRepository.ObtenerTodos();
                    var ListaDeEstadosVM = _mapper.Map<List<EstadoVM>>(ListaDeEstados);
                    EditarPedidoVM.ListaDeEstadosVM = new SelectList(ListaDeEstadosVM, "Id", "NombreEstado");

                    return View(EditarPedidoVM);
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
                return View("AccesoDenegado");
        }

        [HttpPost]
        public IActionResult EditarPedido(EditarPedidoVM EditarPedidoVM)
        {
            if (EsAdministrador() || EsVendedor())
                if (ModelState.IsValid)
                {
                    try
                    {
                        var Pedido = _mapper.Map<Pedido>(EditarPedidoVM);
                        _pedidosRepository.Modificacion(Pedido);
                        return RedirectToAction("ListadoDePedidos");
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
                    return RedirectToAction("ListadoDePedidos");
                }
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult CambiarEstadoPedido(int Id)
        {
            var Pedido = _pedidosRepository.ObtenerPorID(Id);

            if (EsCadete() && Pedido.IdCadete == HttpContext.Session.GetInt32("Id"))
            {
                try
                {
                    var CambiarEstadoPedidoVM = _mapper.Map<CambiarEstadoPedidoVM>(Pedido);

                    var ListaDeEstados = _estadosRepository.ObtenerTodos();
                    var ListaDeEstadosVM = _mapper.Map<List<EstadoVM>>(ListaDeEstados);
                    CambiarEstadoPedidoVM.ListaDeEstadosVM = new SelectList(ListaDeEstadosVM, "Id", "NombreEstado");

                    return View(CambiarEstadoPedidoVM);
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
                return View("AccesoDenegado");
        }

        [HttpPost]
        public IActionResult CambiarEstadoPedido(CambiarEstadoPedidoVM CambiarEstadoPedidoVM)
        {
            if (EsCadete() && CambiarEstadoPedidoVM.IdCadete == HttpContext.Session.GetInt32("Id"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var Pedido = _mapper.Map<Pedido>(CambiarEstadoPedidoVM);
                        _pedidosRepository.Modificacion(Pedido);
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
                }
                return RedirectToAction("ListarPorCadete", new { id = HttpContext.Session.GetInt32("Id") });
            }
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult ListarPorCadete(int Id)
        {
            if (EsAdministrador() || EsVendedor() || ( EsCadete() && Id == HttpContext.Session.GetInt32("Id") ))
            {
                try
                {
                    var ListarPorCadeteVM = new ListarPorCadeteVM();

                    ListarPorCadeteVM.NombreCadete = _cadetesRepository.ObtenerPorID(Id).Nombre;
            
                    var ListaDePedidos = _pedidosRepository.ObtenerPorCadete(Id);
                    var ListaDePedidosVM = _mapper.Map<List<PedidoVM>>(ListaDePedidos);
                    ListarPorCadeteVM.ListaDePedidosVM = ListaDePedidosVM;
            
                    return View(ListarPorCadeteVM);
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult ListarPorCliente(int Id)
        {
            if (EsAdministrador() || EsVendedor())
            {
                try
                {
                    var ListarPorClienteVM = new ListarPorClienteVM();

                    ListarPorClienteVM.NombreCliente = _clientesRepository.ObtenerPorID(Id).Nombre;

                    var ListaDePedidos = _pedidosRepository.ObtenerPorCliente(Id);
                    var ListaDePedidosVM = _mapper.Map<List<PedidoVM>>(ListaDePedidos);
                    ListarPorClienteVM.ListaDePedidosVM = ListaDePedidosVM;

                    return View(ListarPorClienteVM);
                }
                catch (Exception Ex)
                {
                    TempData["Error"] = Ex.Message;
                    return View("ErrorControlado");
                }
            }
            else
                return View("AccesoDenegado");
        }
    }
}
