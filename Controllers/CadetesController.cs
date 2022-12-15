using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoMapper;
using CadeteriaMVC.Repositories;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels.Cadetes;
using CadeteriaMVC.Enums;

namespace CadeteriaMVC.Controllers
{
    public class CadetesController : Controller
    {
        private readonly ILogger<CadetesController> _logger;
        private readonly IMapper _mapper;
        private readonly ICadetesRepository _cadetesRepository;

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper, ICadetesRepository cadetesRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _cadetesRepository = cadetesRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int) Roles.Administrador:
                    return RedirectToAction("ListadoDeCadetes");
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult ListadoDeCadetes()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int) Roles.Administrador:
                    var ListaDeCadetes = _cadetesRepository.ObtenerTodos();
                    var ListaDeCadetesVM = _mapper.Map<List<CadeteVM>>(ListaDeCadetes);
                    var ListadoDeCadetesVM = new ListadoDeCadetesVM();
                    ListadoDeCadetesVM.ListaDeCadetesVM = ListaDeCadetesVM;
                    return View(ListadoDeCadetesVM);
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult CrearCadete()
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int) Roles.Administrador:
                    return View(new CrearCadeteVM());
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public IActionResult CrearCadete(CrearCadeteVM CrearCadeteVM)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int) Roles.Administrador:
                    if (ModelState.IsValid)
                    {
                        var Cadete = _mapper.Map<Cadete>(CrearCadeteVM);
                        _cadetesRepository.Crear(Cadete);
                        return RedirectToAction("ListadoDeCadetes");
                    }
                    else
                    {
                        TempData["Error"] = "Ocurrió un problema al procesar los datos. Por favor intente nuevamente";
                        return RedirectToAction("CrearCadete");
                    }
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult EliminarCadete(int Id)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int) Roles.Administrador:
                    _cadetesRepository.Eliminar(Id);
                    return RedirectToAction("ListadoDeCadetes");
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpGet]
        public IActionResult EditarCadete(int Id)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int) Roles.Administrador:
                    var Cadete = _cadetesRepository.Obtener(Id);
                    var EditarCadeteVM = _mapper.Map<EditarCadeteVM>(Cadete);
                    return View(EditarCadeteVM);
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }

        [HttpPost]
        public IActionResult EditarCadete(EditarCadeteVM EditarCadeteVM)
        {
            switch (HttpContext.Session.GetInt32("IdRol"))
            {
                case (int) Roles.Administrador:
                    if (ModelState.IsValid)
                    {
                        var Cadete = _mapper.Map<Cadete>(EditarCadeteVM);
                        _cadetesRepository.Editar(Cadete);
                        return RedirectToAction("ListadoDeCadetes");
                    }
                    else
                    {
                        TempData["Error"] = "Ocurrió un problema al procesar los datos. Por favor intente nuevamente";
                        return RedirectToAction("EditarCadete");
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
                case (int) Roles.Administrador:
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                default:
                    return RedirectToAction("Index", "Inicio");
            }
        }
    }
}
