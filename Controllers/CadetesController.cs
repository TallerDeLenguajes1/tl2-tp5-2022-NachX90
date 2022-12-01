using Microsoft.AspNetCore.Mvc;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels;
using AutoMapper;
using CadeteriaMVC.Repositories;

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
        public IActionResult ListadoDeCadetes()
        {
            var ListaDeCadetes = _cadetesRepository.ObtenerTodos();
            var ListaDeCadetesViewModel = _mapper.Map<List<CadeteViewModel>>(ListaDeCadetes);
            return View(ListaDeCadetesViewModel);
        }

        [HttpGet]
        public IActionResult CrearCadete()
        {
            return View(new CadeteViewModel());
        }

        [HttpPost]
        public IActionResult CrearCadete(CadeteViewModel CadeteViewModel)
        {
            if (ModelState.IsValid)
            {
                var Cadete = _mapper.Map<Cadete>(CadeteViewModel);
                _cadetesRepository.Crear(Cadete);
                return RedirectToAction("ListadoDeCadetes");
            }
            else
            {
                return RedirectToAction("CrearCadete");
            }
        }

        [HttpGet]
        public IActionResult EliminarCadete(int Id)
        {
            _cadetesRepository.Eliminar(Id);
            return RedirectToAction("ListadoDeCadetes");
        }

        [HttpGet]
        public IActionResult EditarCadete(int Id)
        {
            var Cadete = _cadetesRepository.Obtener(Id);
            var CadeteViewModel = _mapper.Map<CadeteViewModel>(Cadete);
            return View(CadeteViewModel);
        }

        [HttpPost]
        public IActionResult EditarCadete(CadeteViewModel CadeteViewModel)
        {
            if (ModelState.IsValid)
            {
                var Cadete = _mapper.Map<Cadete>(CadeteViewModel);
                _cadetesRepository.Editar(Cadete);
                return RedirectToAction("ListadoDeCadetes");
            }
            else
            {
                return RedirectToAction("EditarCadete");
            }
        }

    }
}
