using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels.Cadetes;
using CadeteriaMVC.Interfaces;
using CadeteriaMVC.Enums;

namespace CadeteriaMVC.Controllers
{
    public class CadetesController : ControlDeSesionController
    {
        private readonly ILogger<CadetesController> _logger;
        private readonly IMapper _mapper;
        private readonly IDBRepository<Empleado> _cadetesRepository;

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper, IDBRepository<Empleado> cadetesRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _cadetesRepository = cadetesRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!EsAdministrador()) return View("AccesoDenegado");
            return RedirectToAction("ListadoDeCadetes");
        }

        [HttpGet]
        public IActionResult ListadoDeCadetes()
        {
            try
            {
                if (!EsAdministrador()) return View("AccesoDenegado");
                var ListaDeEmpleados = _cadetesRepository.ObtenerTodos(IdUsuario());
                var ListaDeEmpleadosVM = _mapper.Map<List<CadeteVM>>(ListaDeEmpleados);
                var ListadoDeCadetesVM = new ListadoDeCadetesVM();
                ListadoDeCadetesVM.ListaDeCadetesVM = ListaDeEmpleadosVM;
                return View(ListadoDeCadetesVM);
            }
            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
                return View("ErrorControlado");
            }
        }

        [HttpGet]
        public IActionResult CrearCadete()
        {
            if (!EsAdministrador()) return View("AccesoDenegado");
            return View(new CrearCadeteVM());
        }

        [HttpPost]
        public IActionResult CrearCadete(CrearCadeteVM CrearCadeteVM)
        {
            try
            {
                if (!EsAdministrador()) return View("AccesoDenegado");
                if (ModelState.IsValid)
                {
                        var Empleado = _mapper.Map<Empleado>(CrearCadeteVM);
                        _cadetesRepository.Alta(Empleado, IdUsuario());
                        return RedirectToAction("ListadoDeCadetes");
                }
                TempData["Error"] = Mensajes.MostrarError(Errores.ModeloInvalido);
                return RedirectToAction("CrearCadete");
            }
            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
                return View("ErrorControlado");
            }
        }

        [HttpGet]
        public IActionResult EliminarCadete(int Id)
        {
            try
            {
                if (!EsAdministrador()) return View("AccesoDenegado");
                _cadetesRepository.BajaLogica(Id, IdUsuario());
                return RedirectToAction("ListadoDeCadetes");
            }
            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
                return View("ErrorControlado");
            }
        }

        [HttpGet]
        public IActionResult EditarCadete(int Id)
        {
            try
            {
                if (!EsAdministrador() && !(EsCadete() && HttpContext.Session.GetInt32("Id") == Id)) return View("AccesoDenegado");
                var Empleado = _cadetesRepository.ObtenerPorID(Id, IdUsuario());
                var EditarCadeteVM = _mapper.Map<EditarCadeteVM>(Empleado);
                return View(EditarCadeteVM);
            }
            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
                return View("ErrorControlado");
            }
        }
            
        [HttpPost]
        public IActionResult EditarCadete(EditarCadeteVM EditarCadeteVM)
        {
            try
            {
                if (!EsAdministrador() && !EsCadete()) return View("AccesoDenegado");
                if (ModelState.IsValid)
                {
                    var Empleado = _mapper.Map<Empleado>(EditarCadeteVM);
                    _cadetesRepository.Modificacion(Empleado, IdUsuario());
                    return RedirectToAction("ListadoDeCadetes");
                }
                TempData["Error"] = Mensajes.MostrarError(Errores.ModeloInvalido);
                return RedirectToAction("ListadoDeCadetes");
            }
            catch (Exception Ex)
            {
                TempData["Error"] = Ex.Message;
                return View("ErrorControlado");
            }
        }
    }
}
