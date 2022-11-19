using Microsoft.AspNetCore.Mvc;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels;
using AutoMapper;

namespace CadeteriaMVC.Controllers
{
    public class CadetesController : Controller
    {
        private readonly ILogger<CadetesController> _logger;
        private readonly IMapper _mapper;
        private readonly static List<Cadete> ListaDeCadetes = new();

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListadoDeCadetes()
        {
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
                Cadete cadete = new Cadete(CadeteViewModel.Nombre, CadeteViewModel.Direccion, CadeteViewModel.Telefono); // debo crear un nuevo cadete para que el autoincremental me asigne un id
                //Cadete cadete = _mapper.Map<Cadete>(CadeteViewModel); // no puedo mapear porque a pesar de que aumenta el autonumérico al crear un Cadete, asigna al Id de CadeteViewModel que es 0 por defecto.
                ListaDeCadetes.Add(cadete);
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
            ListaDeCadetes.RemoveAll(i => i.Id == Id);
            return RedirectToAction("ListadoDeCadetes");
        }

        [HttpGet]
        public IActionResult EditarCadete(int Id)
        {
            Cadete Cadete = ListaDeCadetes.Single(x => x.Id == Id);
            CadeteViewModel CadeteViewModel = _mapper.Map<CadeteViewModel>(Cadete);
            return View(CadeteViewModel);
        }

        [HttpPost]
        public IActionResult EditarCadete(CadeteViewModel CadeteViewModel)
        {
            int i = ListaDeCadetes.FindIndex(x => x.Id == CadeteViewModel.Id);
            Cadete Cadete = _mapper.Map<Cadete>(CadeteViewModel); //este mapeo crea un nuevo cadete y por lo tanto incrementa el contador.
            Cadete.Contador--;
            ListaDeCadetes[i] = Cadete;
            return RedirectToAction("ListadoDeCadetes");
        }

    }
}
