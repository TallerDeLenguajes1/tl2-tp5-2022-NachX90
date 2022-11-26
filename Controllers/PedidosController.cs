using Microsoft.AspNetCore.Mvc;
using CadeteriaMVC.Models;
using CadeteriaMVC.ViewModels;
using AutoMapper;
using System.Data.SQLite;

namespace CadeteriaMVC.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IMapper _mapper;

        public PedidosController(ILogger<PedidosController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ListadoDePedidos()
        {
            List<Pedido> ListaDePedidos = new();

            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = "select * from pedido inner join estado using (id_estado);";
            using (var Conexion = new SQLiteConnection(CadenaDeConexion))
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var nro = Convert.ToUInt32(string.Format("{0}", Reader[0]));
                        var obs = string.Format("{0}", Reader[1]);
                        var id_cliente = Convert.ToUInt32(string.Format("{0}", Reader[2]));
                        var id_cadete = Convert.ToUInt32(string.Format("{0}", Reader[3]));
                        var id_estado = Convert.ToUInt32(string.Format("{0}", Reader[4]));
                        var estado = string.Format("{0}", Reader[5]);
                        Pedido Pedido = new(nro, obs, id_cliente, id_cadete, id_estado, estado);
                        ListaDePedidos.Add(Pedido);
                    }
                }
                Conexion.Close();
            }
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
                string CadenaDeConexion = "DataSource=db/RapiBit.db";
                string SentenciaSQL = $"insert into pedido (obs, id_cliente, id_cadete, id_estado) values ('{PedidoViewModel.Obs}', {PedidoViewModel.IdCliente}, {PedidoViewModel.IdCadete}, {PedidoViewModel.IdEstado});";
                using (var Conexion = new SQLiteConnection(CadenaDeConexion))
                {
                    var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();
                }
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
            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = $"delete from pedido where nro = {Id};";
            using (var Conexion = new SQLiteConnection(CadenaDeConexion))
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                Comando.ExecuteNonQuery();
                Conexion.Close();
            }
            return RedirectToAction("ListadoDePedidos");
        }

        [HttpGet]
        public IActionResult EditarPedido(int Id)
        {
            PedidoViewModel PedidoViewModel = new();
            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = $"select * from pedido inner join estado using (id_estado) where nro = {Id};";
            using (var Conexion = new SQLiteConnection(CadenaDeConexion))
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var nro = Convert.ToUInt32(string.Format("{0}", Reader[0]));
                        var obs = string.Format("{0}", Reader[1]);
                        var id_cliente = Convert.ToUInt32(string.Format("{0}", Reader[2]));
                        var id_cadete = Convert.ToUInt32(string.Format("{0}", Reader[3]));
                        var id_estado = Convert.ToUInt32(string.Format("{0}", Reader[4]));
                        var estado = string.Format("{0}", Reader[5]);
                        Pedido Pedido = new(nro, obs, id_cliente, id_cadete, id_estado, estado);
                        PedidoViewModel = _mapper.Map<PedidoViewModel>(Pedido);
                    }
                }
                Conexion.Close();
            }
            return View(PedidoViewModel);
        }

        [HttpPost]
        public IActionResult EditarPedido(PedidoViewModel PedidoViewModel)
        {
            if (ModelState.IsValid)
            {
                string CadenaDeConexion = "DataSource=db/RapiBit.db";
                string SentenciaSQL = $"update pedido set obs='{PedidoViewModel.Obs}', id_cliente={PedidoViewModel.IdCliente}, id_cadete={PedidoViewModel.IdCadete}, id_estado={PedidoViewModel.IdEstado} where nro={PedidoViewModel.Nro};";
                using (var Conexion = new SQLiteConnection(CadenaDeConexion))
                {
                    var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                    Conexion.Open();
                    Comando.ExecuteNonQuery();
                    Conexion.Close();
                }
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
            List<Pedido> ListaDePedidos = new();

            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = $"select * from pedido inner join estado using (id_estado) where id_cadete={Id};";
            using (var Conexion = new SQLiteConnection(CadenaDeConexion))
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var nro = Convert.ToUInt32(string.Format("{0}", Reader[0]));
                        var obs = string.Format("{0}", Reader[1]);
                        var id_cliente = Convert.ToUInt32(string.Format("{0}", Reader[2]));
                        var id_cadete = Convert.ToUInt32(string.Format("{0}", Reader[3]));
                        var id_estado = Convert.ToUInt32(string.Format("{0}", Reader[4]));
                        var estado = string.Format("{0}", Reader[5]);
                        Pedido Pedido = new(nro, obs, id_cliente, id_cadete, id_estado, estado);
                        ListaDePedidos.Add(Pedido);
                    }
                }
                Conexion.Close();
            }
            var ListaDePedidosViewModel = _mapper.Map<List<PedidoViewModel>>(ListaDePedidos);
            return View(ListaDePedidosViewModel);
        }

        [HttpGet]
        public IActionResult ListarPorCliente(int Id)
        {
            List<Pedido> ListaDePedidos = new();

            string CadenaDeConexion = "DataSource=db/RapiBit.db";
            string SentenciaSQL = $"select * from pedido inner join estado using (id_estado) where id_cliente={Id};";
            using (var Conexion = new SQLiteConnection(CadenaDeConexion))
            {
                var Comando = new SQLiteCommand(SentenciaSQL, Conexion);
                Conexion.Open();
                using (SQLiteDataReader Reader = Comando.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        var nro = Convert.ToUInt32(string.Format("{0}", Reader[0]));
                        var obs = string.Format("{0}", Reader[1]);
                        var id_cliente = Convert.ToUInt32(string.Format("{0}", Reader[2]));
                        var id_cadete = Convert.ToUInt32(string.Format("{0}", Reader[3]));
                        var id_estado = Convert.ToUInt32(string.Format("{0}", Reader[4]));
                        var estado = string.Format("{0}", Reader[5]);
                        Pedido Pedido = new(nro, obs, id_cliente, id_cadete, id_estado, estado);
                        ListaDePedidos.Add(Pedido);
                    }
                }
                Conexion.Close();
            }
            var ListaDePedidosViewModel = _mapper.Map<List<PedidoViewModel>>(ListaDePedidos);
            return View(ListaDePedidosViewModel);
        }
    }
}
