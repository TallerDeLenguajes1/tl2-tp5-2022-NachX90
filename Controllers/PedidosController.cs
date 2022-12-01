using Microsoft.AspNetCore.Mvc;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using CadeteriaMVC.Repositories;

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
        public IActionResult ListadoDePedidos()
        {
            var ListaDePedidos = _pedidosRepository.ObtenerTodos();
            var ListaDePedidosViewModel = _mapper.Map<List<PedidoViewModel>>(ListaDePedidos);
            return View(ListaDePedidosViewModel);
        }

        [HttpGet]
        public IActionResult CrearPedido()
        {
            PedidoViewModel PedidoViewModel = new();
            var ListaDeClientes = _clientesRepository.ObtenerTodos();
            var ListaDeClientesViewModel = _mapper.Map<List<ClienteViewModel>>(ListaDeClientes);
            PedidoViewModel.ListaDeClientes = new SelectList(ListaDeClientesViewModel, "Id", "Nombre");

            return View(PedidoViewModel);
        }

        [HttpPost]
        public IActionResult CrearPedido(PedidoViewModel PedidoViewModel)
        {
            if (ModelState.IsValid)
            {
                var Pedido = _mapper.Map<Pedido>(PedidoViewModel);
                _pedidosRepository.Crear(Pedido);
                return RedirectToAction("ListadoDePedidos");
            }
            else
            {
                return RedirectToAction("CrearPedido");
            }
        }

        [HttpGet]
        public IActionResult EliminarPedido(int Id)
        {
            _pedidosRepository.Eliminar(Id);
            return RedirectToAction("ListadoDePedidos");
        }

        [HttpGet]
        public IActionResult EditarPedido(int Id)
        {
            var Pedido = _pedidosRepository.Obtener(Id);
            var PedidoViewModel = _mapper.Map<PedidoViewModel>(Pedido);

            var ListaDeCadetes = _cadetesRepository.ObtenerTodos();
            var ListaDeCadetesViewModel = _mapper.Map<List<CadeteViewModel>>(ListaDeCadetes);
            PedidoViewModel.ListaDeCadetes = new SelectList(ListaDeCadetesViewModel, "Id", "Nombre");

            var ListaDeEstados = _estadosRepository.ObtenerTodos();
            var ListaDeEstadosViewModel = _mapper.Map<List<EstadoViewModel>>(ListaDeEstados);
            PedidoViewModel.ListaDeEstados = new SelectList(ListaDeEstadosViewModel, "Id", "EstadoPedido");

            return View(PedidoViewModel);
        }

        [HttpPost]
        public IActionResult EditarPedido(PedidoViewModel PedidoViewModel)
        {
            if (ModelState.IsValid)
            {
                var Pedido = _mapper.Map<Pedido>(PedidoViewModel);
                _pedidosRepository.Editar(Pedido);
                return RedirectToAction("ListadoDePedidos");
            }
            else
            {
                return RedirectToAction("EditarPedido");
            }
        }

        [HttpGet]
        public IActionResult ListarPorCadete(int Id)
        {
            ViewData["Cadete"] = _cadetesRepository.Obtener(Id).Nombre;
            var ListaDePedidos = _pedidosRepository.ObtenerPorCadete(Id);
            var ListaDePedidosViewModel = _mapper.Map<List<PedidoViewModel>>(ListaDePedidos);
            return View(ListaDePedidosViewModel);
        }

        [HttpGet]
        public IActionResult ListarPorCliente(int Id)
        {
            ViewData["Cliente"] = _clientesRepository.Obtener(Id).Nombre;
            var ListaDePedidos = _pedidosRepository.ObtenerPorCliente(Id);
            var ListaDePedidosViewModel = _mapper.Map<List<PedidoViewModel>>(ListaDePedidos);
            return View(ListaDePedidosViewModel);
        }
    }
}
