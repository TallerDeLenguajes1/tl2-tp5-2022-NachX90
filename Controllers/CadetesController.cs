﻿using Microsoft.AspNetCore.Mvc;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels;
using AutoMapper;
using System.Data.SQLite;

namespace CadeteriaMVC.Controllers
{
    public class CadetesController : Controller
    {
        private readonly ILogger<CadetesController> _logger;
        private readonly IMapper _mapper;

        public CadetesController(ILogger<CadetesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListadoDeCadetes()
        {
            List<Cadete> ListaDeCadetes = new();

            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = "select * from cadete;";
            using (var Conexion = new SQLiteConnection(CadenaDeConexion))
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var id = Convert.ToUInt32(string.Format("{0}", Reader[0]));
                        var nombre = string.Format("{0}", Reader[1]);
                        var direccion = string.Format("{0}", Reader[2]);
                        var telefono = Convert.ToUInt32(string.Format("{0}", Reader[3]));
                        Cadete Cad = new(id, nombre, direccion, telefono);
                        ListaDeCadetes.Add(Cad);
                    }
                }
                Conexion.Close();
            }
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
                string CadenaDeConexion = "DataSource=db/RapiBit.db";
                string SentenciaSQL = $"insert into cadete (nombre, direccion, telefono) values ('{CadeteViewModel.Nombre}', '{CadeteViewModel.Direccion}', '{CadeteViewModel.Telefono}');";
                using (var Conexion = new SQLiteConnection(CadenaDeConexion))
                {
                    var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();
                }
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
            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = $"delete from cadete where id = {Id};";
            using (var Conexion = new SQLiteConnection(CadenaDeConexion))
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
            return RedirectToAction("ListadoDeCadetes");
        }

        [HttpGet]
        public IActionResult EditarCadete(int Id)
        {
            CadeteViewModel CadVM = new();
            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = $"select * from cadete where id = {Id};";
            using (var Conexion = new SQLiteConnection(CadenaDeConexion))
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var id = Convert.ToUInt32(string.Format("{0}", Reader[0]));
                        var nombre = string.Format("{0}", Reader[1]);
                        var direccion = string.Format("{0}", Reader[2]);
                        var telefono = Convert.ToUInt32(string.Format("{0}", Reader[3]));
                        Cadete Cad = new(id, nombre, direccion, telefono);
                        CadVM = _mapper.Map<CadeteViewModel>(Cad);
                    }
                }
                Conexion.Close();
            }
            return View(CadVM);
        }

        [HttpPost]
        public IActionResult EditarCadete(CadeteViewModel CadeteViewModel)
        {
            if (ModelState.IsValid)
            {
                string CadenaDeConexion = "DataSource=db/RapiBit.db";
                string SentenciaSQL = $"update cadete set nombre='{CadeteViewModel.Nombre}', direccion='{CadeteViewModel.Direccion}', telefono='{CadeteViewModel.Telefono}' where id = {CadeteViewModel.Id};";
                using (var Conexion = new SQLiteConnection(CadenaDeConexion))
                {
                    var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();
                }
                return RedirectToAction("ListadoDeCadetes");
            }
            else
            {
                return RedirectToAction("EditarCadete");
            }
        }

    }
}
