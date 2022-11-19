using Microsoft.AspNetCore.Mvc;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels;
using AutoMapper;

namespace CadeteriaMVC.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IMapper _mapper;
        private readonly static List<Pedido> ListaDePedidos = new();

        //Estos clientes servirán por ahora. Después se implementará el CRUD Clientes.
        //private readonly static List<Cliente> ListaDeClientes = new()
        //{
        //    new Cliente("Javier", "Maipú 324", 3815111111, "Dejar en portería"),
        //    new Cliente("Sergio", "San martín 702", 3815222222, "Edificio esquina"),
        //    new Cliente("Agustin", "Córdoba 536", 3815333333, "Oficina 4, Escribanía")
        //};
        private readonly static List<string> ListaDeClientes = new()
        {
            "Javier",
            "Sergio",
            "Agustin"
        };
        Random Random = new(); //Este random es solo para seleccionar un cliente aleatorio

        public PedidosController(ILogger<PedidosController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListadoDePedidos()
        {
            var ListaDePedidosViewModel = _mapper.Map<List<PedidoViewModel>>(ListaDePedidos);
            return View(ListaDePedidosViewModel);
        }

        [HttpGet]
        public IActionResult CrearPedido()
        {
            return View(new PedidoViewModel());
        }

        [HttpPost]
        public IActionResult CrearPedido(PedidoViewModel PedidoViewModel)
        {
            if (ModelState.IsValid)
            {
                var Pedido = new Pedido(PedidoViewModel.Obs, ListaDeClientes[Random.Next(ListaDeClientes.Count())]);
                ListaDePedidos.Add(Pedido);
                return RedirectToAction("ListadoDePedidos");
            }
            else
            {
                return RedirectToAction("CrearPedido");
            }
        }

        [HttpGet]
        public IActionResult EliminarPedido(int Nro)
        {
            ListaDePedidos.RemoveAll(i => i.Nro == Nro);
            return RedirectToAction("ListadoDePedidos");
        }

        [HttpGet]
        public IActionResult EditarPedido(int Nro)
        {
            Pedido Pedido = ListaDePedidos.Single(i => i.Nro == Nro);
            PedidoViewModel PedidoViewModel = _mapper.Map<PedidoViewModel>(Pedido);
            return View(PedidoViewModel);
        }

        [HttpPost]
        public IActionResult EditarPedido(PedidoViewModel PedidoViewModel)
        {
            int i = ListaDePedidos.FindIndex(i => i.Nro == PedidoViewModel.Nro);
            Pedido Pedido = _mapper.Map<Pedido>(PedidoViewModel);
            Pedido.Contador--;
            ListaDePedidos[i] = Pedido;
            return RedirectToAction("ListadoDePedidos");
        }

    }
}
