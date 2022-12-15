using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoMapper;
using CadeteriaMVC.Repositories;
using CadeteriaMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CadeteriaMVC.ViewModels.Cadetes;
using CadeteriaMVC.ViewModels.Clientes;
using CadeteriaMVC.ViewModels.Estados;
using CadeteriaMVC.ViewModels.Pedidos;
using CadeteriaMVC.Enums;

namespace CadeteriaMVC.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IMapper _mapper;
        private readonly IPedidosRepository _pedidosRepository;
        private readonly IClientesRepository _clientesRepository;
        private readonly ICadetesRepository _cadetesRepository;
        private readonly IEstadosRepository _estadosRepository;

        public PedidosController(ILogger<PedidosController> logger, IMapper mapper, IPedidosRepository pedidosRepository, IClientesRepository clientesRepository, ICadetesRepository cadetesRepository, IEstadosRepository estadosRepository)
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
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    return RedirectToAction("ListadoDePedidos");
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult ListadoDePedidos()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    var ListaDePedidos = _pedidosRepository.ObtenerTodos();
                    var ListaDePedidosVM = _mapper.Map<List<PedidoVM>>(ListaDePedidos);
                    var ListadoDePedidosVM = new ListadoDePedidosVM();
                    ListadoDePedidosVM.ListaDePedidosVM = ListaDePedidosVM;
                    return View(ListadoDePedidosVM);
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult CrearPedido()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    var CrearPedidoVM = new CrearPedidoVM();

                    var ListaDeClientes = _clientesRepository.ObtenerTodos();
                    var ListaDeClientesVM = _mapper.Map<List<ClienteVM>>(ListaDeClientes);
                    CrearPedidoVM.ListaDeClientesVM = new SelectList(ListaDeClientesVM, "Id", "Nombre");

                    return View(CrearPedidoVM);
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public IActionResult CrearPedido(CrearPedidoVM CrearPedidoVM)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    if (ModelState.IsValid)
                    {
                        var Pedido = _mapper.Map<Pedido>(CrearPedidoVM);
                        _pedidosRepository.Crear(Pedido);
                        return RedirectToAction("ListadoDePedidos");
                    }
                    else
                    {
                        return RedirectToAction("CrearPedido");
                    }
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult EliminarPedido(int Id)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    _pedidosRepository.Eliminar(Id);
                    return RedirectToAction("ListadoDePedidos");
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult EditarPedido(int Id)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                case (int)Roles.Cadete:
                    var Pedido = _pedidosRepository.Obtener(Id);
                    
                    if (HttpContext.Session.GetInt32("IdRol") == (int)Roles.Administrador || (HttpContext.Session.GetInt32("IdRol") == (int)Roles.Cadete && Pedido.IdCadete == HttpContext.Session.GetInt32("IdCadete")))
                    {
                        var EditarPedidoVM = _mapper.Map<EditarPedidoVM>(Pedido);

                        var ListaDeCadetes = _cadetesRepository.ObtenerTodos();
                        var ListaDeCadetesVM = _mapper.Map<List<CadeteVM>>(ListaDeCadetes);
                        EditarPedidoVM.ListaDeCadetesVM = new SelectList(ListaDeCadetesVM, "Id", "Nombre");

                        var ListaDeEstados = _estadosRepository.ObtenerTodos();
                        var ListaDeEstadosVM = _mapper.Map<List<EstadoVM>>(ListaDeEstados);
                        EditarPedidoVM.ListaDeEstadosVM = new SelectList(ListaDeEstadosVM, "Id", "EstadoPedido");

                        return View(EditarPedidoVM);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Inicio");
                    }
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public IActionResult EditarPedido(EditarPedidoVM EditarPedidoVM)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                case (int)Roles.Cadete:
                    if (ModelState.IsValid)
                    {
                        var Pedido = _mapper.Map<Pedido>(EditarPedidoVM);
                        _pedidosRepository.Editar(Pedido);
                        return RedirectToAction("ListadoDePedidos");
                    }
                    else
                    {
                        return RedirectToAction("EditarPedido");
                    }
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult ListarPorCadete(int Id)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                case (int)Roles.Cadete:
                    if (HttpContext.Session.GetInt32("IdRol") == (int)Roles.Administrador || (HttpContext.Session.GetInt32("IdRol") == (int)Roles.Cadete && Id == HttpContext.Session.GetInt32("IdCadete")))
                    {
                        var ListarPorCadeteVM = new ListarPorCadeteVM();

                        ListarPorCadeteVM.NombreCadete = _cadetesRepository.Obtener(Id).Nombre;
            
                        var ListaDePedidos = _pedidosRepository.ObtenerPorCadete(Id);
                        var ListaDePedidosVM = _mapper.Map<List<PedidoVM>>(ListaDePedidos);
                        ListarPorCadeteVM.ListaDePedidosVM = ListaDePedidosVM;
            
                        return View(ListarPorCadeteVM);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Inicio");
                    }
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult ListarPorCliente(int Id)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int)Roles.Administrador:
                    var ListarPorClienteVM = new ListarPorClienteVM();

                    ListarPorClienteVM.NombreCliente = _clientesRepository.Obtener(Id).Nombre;

                    var ListaDePedidos = _pedidosRepository.ObtenerPorCliente(Id);
                    var ListaDePedidosVM = _mapper.Map<List<PedidoVM>>(ListaDePedidos);
                    ListarPorClienteVM.ListaDePedidosVM = ListaDePedidosVM;
                    return View(ListarPorClienteVM);
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
