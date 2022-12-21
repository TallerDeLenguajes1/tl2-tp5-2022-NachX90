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
            if (EsAdministrador())
                return RedirectToAction("ListadoDeCadetes");
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult ListadoDeCadetes()
        {
            if (EsAdministrador())
            {
                try
                {
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
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult CrearCadete()
        {
            if (EsAdministrador())
                return View(new CrearCadeteVM());
            else
                return View("AccesoDenegado");
        }

        [HttpPost]
        public IActionResult CrearCadete(CrearCadeteVM CrearCadeteVM)
        {
            if (EsAdministrador())
                if (ModelState.IsValid)
                {
                    try
                    {
                        var Empleado = _mapper.Map<Empleado>(CrearCadeteVM);
                        _cadetesRepository.Alta(Empleado, IdUsuario());
                        return RedirectToAction("ListadoDeCadetes");
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
                    return RedirectToAction("CrearCadete");
                }
            else
                return View("AccesoDenegado");
        }

        [HttpGet]
        public IActionResult EliminarCadete(int Id)
        {
            if (EsAdministrador())
            {
                try
                {
                    _cadetesRepository.BajaLogica(Id, IdUsuario());
                    return RedirectToAction("ListadoDeCadetes");
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
        public IActionResult EditarCadete(int Id)
        {
            if (EsAdministrador() || EsCadete() && HttpContext.Session.GetInt32("Id") == Id)
            {
                try
                {
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
            else
                return View("AccesoDenegado");
        }

        [HttpPost]
        public IActionResult EditarCadete(EditarCadeteVM EditarCadeteVM)
        {
            if (EsAdministrador() || EsCadete())
                if (ModelState.IsValid)
                {
                    try
                    {
                        var Empleado = _mapper.Map<Empleado>(EditarCadeteVM);
                        _cadetesRepository.Modificacion(Empleado, IdUsuario());
                        return RedirectToAction("ListadoDeCadetes");
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
                    return RedirectToAction("ListadoDeCadetes");
                }
            else
                return View("AccesoDenegado");
        }
    }
}
