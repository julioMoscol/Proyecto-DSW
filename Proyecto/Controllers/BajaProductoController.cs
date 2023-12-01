using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;
using System.Data;
using System.Security.Claims;

namespace Proyecto.Controllers
{
    [Authorize]
    public class BajaProductoController : Controller
    {

        IProducto _producto;
            ITrabajador _trabajador;
            IProveedor _proveedor;
            ICargo _cargo;
            IArea _area;
            ITipoBaja _tipoBaja;
            IBaja _baja;
            IConfiguration _config;

            public BajaProductoController(IConfiguration config)
            {
                _config = config;
                _producto = new ProductoSQl();
                _proveedor = new ProveedorSQL();
                _trabajador = new TrabajadorSQL();
                _area = new AreaSQL();
                _tipoBaja = new TipoBajaSQL();
                _baja = new BajaSQL();
                _cargo = new CargoSQL();
            }



            public async Task<IActionResult> list(string nomproducto = "", int p = 0)
            {
                ViewBag.nomproducto = nomproducto;

                IEnumerable<Producto> temporal = _producto.GetProducto();
                IEnumerable<Producto> temporal1 = _producto.GetProductoo(nomproducto);
                int f = 7;
                int c = temporal.Count();
                int pags = c % f == 0 ? c / f : c / f + 1;
                ViewBag.p = p;
                ViewBag.pags = pags;

                if (HttpContext.Session.GetString("Canasta") == null)
                    HttpContext.Session.SetString("Canasta",
                            JsonConvert.SerializeObject(new List<DetalleBaja>()));

                if (nomproducto == null)
                    return View(await Task.Run(() => temporal.Skip(f * p).Take(f)));

                return View(await Task.Run(() => temporal1.Skip(f * p).Take(f)));
            }

            public async Task<IActionResult> Detalle(int id = 0)
            {
                Producto reg = _producto.GetProductoID(id);
                if (reg == null)
                    return RedirectToAction("list");
                else
                    return View(await Task.Run(() => reg));
            }

            [HttpPost]
            public async Task<IActionResult> Detalle(int codigo, int cantidad, int idtipobaja)
            {
                Producto item = _producto.GetProductoID(codigo);

                DetalleBaja reg = new DetalleBaja()
                {
                    idproducto = item.idproducto,
                    idtipobaja = idtipobaja,
                    cantidad = cantidad
                };

                List<DetalleBaja> temporal = JsonConvert.DeserializeObject<List<DetalleBaja>>(HttpContext.Session.GetString("Canasta"));
                temporal.Add(reg);

                HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(temporal));

                ViewBag.mensaje = "Producto Agregado";

                return View(await Task.Run(() => item));
            }

            public async Task<IActionResult> Canasta()
            {
                string idproveedor = "";
                List<DetalleBaja> temporal = JsonConvert.DeserializeObject<List<DetalleBaja>>(
                            HttpContext.Session.GetString("Canasta"));
                return View(await Task.Run(() => temporal));
            }

            [HttpPost]
            public async Task<IActionResult> Canasta(int codigo, int cantidad)
            {
                List<DetalleBaja> canasta = JsonConvert.DeserializeObject<List<DetalleBaja>>(HttpContext.Session.GetString("Canasta"));
                DetalleBaja registro = canasta.FirstOrDefault(r => r.idproducto == codigo);
                if (registro != null)
                {
                    registro.cantidad = cantidad;
                    string canastaJson = JsonConvert.SerializeObject(canasta);
                    HttpContext.Session.SetString("Canasta", canastaJson);
                }
                return RedirectToAction("Canasta");
            }

            public IActionResult Delete(int id)
            {
                List<DetalleBaja> temporal = JsonConvert.DeserializeObject<List<DetalleBaja>>(
                            HttpContext.Session.GetString("Canasta"));

                DetalleBaja reg = temporal.Where(it => it.idproducto == id).First();
                temporal.Remove(reg);

                HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(temporal));


                return RedirectToAction("Canasta");
            }

            public async Task<IActionResult> Baja()
            {
                ClaimsPrincipal claimuser = HttpContext.User;
                string correo = "";

                if (claimuser.Identity.IsAuthenticated)
                {
                    correo = claimuser.Claims.Where(C => C.Type == ClaimTypes.Name).
                    Select(c => c.Value).SingleOrDefault();
                }
                ViewBag.idtrabajador = _trabajador.GetUsuario(correo).idtrabajador;
                List<DetalleBaja> canasta = JsonConvert.DeserializeObject<List<DetalleBaja>>(HttpContext.Session.GetString("Canasta"));

                return View(await Task.Run(() => new BajaProducto()));
            }

            [HttpPost]
            public async Task<IActionResult> Baja(BajaProducto reg, int idbaja = 0)
            {
                ClaimsPrincipal claimuser = HttpContext.User;
                string correo = "";

                if (claimuser.Identity.IsAuthenticated)
                {
                    correo = claimuser.Claims.Where(C => C.Type == ClaimTypes.Name).
                    Select(c => c.Value).SingleOrDefault();
                }
                ViewBag.idtrabajador = _trabajador.GetUsuario(correo).idtrabajador;
                string mensaje = "";
                using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:sql"]))
                {
                    cn.Open();
                    SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_agregar_baja", cn, tr);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", idbaja);
                        cmd.Parameters.AddWithValue("@fecha", reg.fechabaja);
                        cmd.Parameters.AddWithValue("@idtrabajador", reg.idtrabajador);
                        cmd.ExecuteNonQuery();

                        List<DetalleBaja> temporal = JsonConvert.DeserializeObject<List<DetalleBaja>>(
                            HttpContext.Session.GetString("Canasta"));
                        temporal.ForEach(x =>
                        {
                            SqlCommand cmd = new SqlCommand("sp_detalle_baja", cn, tr);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", idbaja);
                            cmd.Parameters.AddWithValue("@idproducto", x.idproducto);
                            cmd.Parameters.AddWithValue("@cant", x.cantidad);
                            cmd.Parameters.AddWithValue("@idtipobaja", x.idtipobaja);
                            cmd.ExecuteNonQuery();
                        });

                        temporal.ForEach(x =>
                        {
                            cmd = new SqlCommand(
                            "update producto set cantidad-=@cantidad Where idproducto=@idproducto", cn, tr);
                            cmd.Parameters.AddWithValue("@idproducto", x.idproducto);
                            cmd.Parameters.AddWithValue("@cantidad", x.cantidad);
                            cmd.ExecuteNonQuery();
                        });

                        tr.Commit();
                        mensaje = "Baja Registrado";
                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.Message; tr.Rollback();
                    }
                    finally { cn.Close(); }
                }
                ViewBag.mensaje = mensaje;
                return View(await Task.Run(() => reg));
            }

            public async Task<IActionResult> listbaja()
            {

                return View(await Task.Run(() => _baja.listado()));
            }

            public async Task<IActionResult> listdetallebaja(int id = 0)
            {
                IEnumerable<DetalleBaja> temporal1 = _baja.listadoDetalle(id);
                return View(await Task.Run(() => temporal1));
            }
   
    }
}
