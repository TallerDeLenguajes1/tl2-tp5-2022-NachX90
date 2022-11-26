using Microsoft.AspNetCore.Mvc;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels;
using AutoMapper;
using System.Data.SQLite;

namespace CadeteriaMVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IMapper _mapper;

        public ClientesController(ILogger<ClientesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListadoDeClientes()
        {
            List<Cliente> ListaDeClientes = new();

            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = "select * from cliente;";
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
                        var referenciaDireccion = string.Format("{0}", Reader[3]);
                        var telefono = Convert.ToUInt32(string.Format("{0}", Reader[4]));
                        Cliente Cli = new(id, nombre, direccion, referenciaDireccion, telefono);
                        ListaDeClientes.Add(Cli);
                    }
                }
                Conexion.Close();
            }
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
                string CadenaDeConexion = "DataSource=db/RapiBit.db";
                string SentenciaSQL = $"insert into cliente (nombre, direccion, referencia, telefono) values ('{ClienteViewModel.Nombre}', '{ClienteViewModel.Direccion}', '{ClienteViewModel.ReferenciaDeDireccion}', '{ClienteViewModel.Telefono}');";
                using (var Conexion = new SQLiteConnection(CadenaDeConexion))
                {
                    var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();
                }
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
            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = $"delete from cliente where id = {Id};";
            using (var Conexion = new SQLiteConnection(CadenaDeConexion))
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
            return RedirectToAction("ListadoDeClientes");
        }

        [HttpGet]
        public IActionResult EditarCliente(int Id)
        {
            ClienteViewModel CliVM = new();
            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = $"select * from cliente where id = {Id};";
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
                        var referencia = string.Format("{0}", Reader[3]);
                        var telefono = Convert.ToUInt32(string.Format("{0}", Reader[4]));
                        Cliente Cli = new(id, nombre, direccion, referencia, telefono);
                        CliVM = _mapper.Map<ClienteViewModel>(Cli);
                    }
                }
                Conexion.Close();
            }
            return View(CliVM);
        }

        [HttpPost]
        public IActionResult EditarCliente(ClienteViewModel ClienteViewModel)
        {
            if (ModelState.IsValid)
            {
                string CadenaDeConexion = "DataSource=db/RapiBit.db";
                string SentenciaSQL = $"update cliente set nombre='{ClienteViewModel.Nombre}', direccion='{ClienteViewModel.Direccion}', direccion='{ClienteViewModel.ReferenciaDeDireccion}', telefono='{ClienteViewModel.Telefono}' where id = {ClienteViewModel.Id};";
                using (var Conexion = new SQLiteConnection(CadenaDeConexion))
                {
                    var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();
                }
                return RedirectToAction("ListadoDeClientes");
            }
            else
            {
                return RedirectToAction("EditarCliente");
            }
        }

    }
}
