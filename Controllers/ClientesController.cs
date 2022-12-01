using Microsoft.AspNetCore.Mvc;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels;
using AutoMapper;
using System.Data.SQLite;
using CadeteriaMVC.Repositories;

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
        public IActionResult ListadoDeClientes()
        {
            var ListaDeClientes = _clientesRepository.ObtenerTodos();
            var ListaDeClientesViewModel = _mapper.Map<List<ClienteViewModel>>(ListaDeClientes);
            return View(ListaDeClientesViewModel);
        }

        [HttpGet]
        public IActionResult CrearCliente()
        {
            return View(new ClienteViewModel());
        }

        [HttpPost]
        public IActionResult CrearCliente(ClienteViewModel ClienteViewModel)
        {
            if (ModelState.IsValid)
            {
                var Cliente = _mapper.Map<Cliente>(ClienteViewModel);
                _clientesRepository.Crear(Cliente);
                return RedirectToAction("ListadoDeClientes");
            }
            else
            {
                return RedirectToAction("CrearCliente");
            }
        }

        [HttpGet]
        public IActionResult EliminarCliente(int Id)
        {
            _clientesRepository.Eliminar(Id);
            return RedirectToAction("ListadoDeClientes");
        }

        [HttpGet]
        public IActionResult EditarCliente(int Id)
        {
            var Cliente = _clientesRepository.Obtener(Id);
            var ClienteViewModel = _mapper.Map<ClienteViewModel>(Cliente);
            return View(ClienteViewModel);
        }

        [HttpPost]
        public IActionResult EditarCliente(ClienteViewModel ClienteViewModel)
        {
            if (ModelState.IsValid)
            {
                var Cliente = _mapper.Map<Cliente>(ClienteViewModel);
                _clientesRepository.Editar(Cliente);
                return RedirectToAction("ListadoDeClientes");
            }
            else
            {
                return RedirectToAction("EditarCliente");
            }
        }

    }
}
