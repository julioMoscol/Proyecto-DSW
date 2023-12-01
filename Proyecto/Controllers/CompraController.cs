﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Microsoft.Win32;
using System;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto.Controllers
{

    [Authorize]
    public class CompraController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ICliente _cliente;
        ICategoria _categoria;
        IAnimal _animal;
        public CompraController()
        {
            _producto = new ProductoSQl();
            _proveedor = new ProveedorSQL();
            _trabajador = new TrabajadorSQL();
            _cliente = new ClienteSQL();
            _categoria = new CategoriaSQL();
            _animal = new AnimalSQL();
        }

        public async Task<IActionResult> list(int p = 0, int idproveedor = 0)
        {
            IEnumerable<Producto> temporal = _producto.GetProducto();


            if (HttpContext.Session.GetString("Canasta") == null)
                HttpContext.Session.SetString("Canasta",
                        JsonConvert.SerializeObject(new List<DetalleCompra>()));



            return View(await Task.Run(() => _proveedor.GetProveedor()));
        }
        /*
        public async Task<IActionResult> Detalle(string id = "")
        {
            Producto reg = _producto.GetProducto(id);
            if (reg == null)
                return RedirectToAction("list");
            else
                return View(await Task.Run(() => reg));
        }

        [HttpPost]
        public async Task<IActionResult> Detalle(string codigo, int cantidad)
        {
            Producto item = _producto.GetProducto(codigo);

            DetalleCompra reg = new DetalleCompra()
            {
                idproveedor = item.idproveedor,
                idproducto = item.idproducto,
                preciocompra = item.precio,
                cantidad = cantidad
            };

            List<DetalleCompra> temporal = JsonConvert.DeserializeObject<List<DetalleCompra>>(HttpContext.Session.GetString("Canasta"));
            temporal.Add(reg);

            HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(temporal));

            ViewBag.mensaje = "Producto Agregado";

            return View(await Task.Run(() => item));
        }

        public async Task<IActionResult> Canasta()
        {
            string idproveedor = "";
            List<DetalleCompra> temporal = JsonConvert.DeserializeObject<List<DetalleCompra>>(
                        HttpContext.Session.GetString("Canasta"));
            foreach (var item in temporal)
            {
                idproveedor = item.idproveedor;
            }
            ViewBag.idproveedor = idproveedor;
            return View(await Task.Run(() => temporal));
        }

        [HttpPost]
        public async Task<IActionResult> Canasta(string codigo, int cantidad)
        {
            List<DetalleCompra> canasta = JsonConvert.DeserializeObject<List<DetalleCompra>>(HttpContext.Session.GetString("Canasta"));
            DetalleCompra registro = canasta.FirstOrDefault(r => r.idproducto == codigo);
            if (registro != null)
            {
                registro.cantidad = cantidad;
                string canastaJson = JsonConvert.SerializeObject(canasta);
                HttpContext.Session.SetString("Canasta", canastaJson);
            }
            ViewBag.idproveedor = registro.idproveedor;
            return RedirectToAction("Canasta");
        }

        public IActionResult Delete(string id)
        {
            List<DetalleCompra> temporal = JsonConvert.DeserializeObject<List<DetalleCompra>>(
                        HttpContext.Session.GetString("Canasta"));

            DetalleCompra reg = temporal.Where(it => it.idproducto == id).First();
            temporal.Remove(reg);

            HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(temporal));


            return RedirectToAction("Canasta");
        }

        public async Task<IActionResult> Pedido()
        {
            string idproveedor = "";
            decimal monto = 0;
            List<DetalleCompra> canasta = JsonConvert.DeserializeObject<List<DetalleCompra>>(HttpContext.Session.GetString("Canasta"));
            foreach (var item in canasta)
            {
                monto += item.monto;
                idproveedor = item.idproveedor;
            }
            ViewBag.suma = monto;
            ViewBag.idproveedor = idproveedor;
            return View(await Task.Run(() => new CompraProducto()));
        }

        [HttpPost]
        public async Task<IActionResult> Pedido(CompraProducto reg, string idpedido = "")
        {
            decimal monto = 0;
            string idproveedor = "";
            ViewBag.idproveedor = new SelectList(_proveedor.GetProveedores(), "idproveedor", "empresa");
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(_config["ConnectionStrings:sql"]))
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_agregar_compra", cn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idpedido);
                    cmd.Parameters.AddWithValue("@fecha", reg.fechapedido);
                    cmd.Parameters.AddWithValue("@idproveedor", reg.idproveedor);
                    cmd.Parameters.AddWithValue("@montot", reg.montoT);
                    cmd.ExecuteNonQuery();

                    List<DetalleCompra> temporal = JsonConvert.DeserializeObject<List<DetalleCompra>>(
                        HttpContext.Session.GetString("Canasta"));
                    temporal.ForEach(x =>
                    {
                        SqlCommand cmd = new SqlCommand("sp_detalle_compraa", cn, tr);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", idpedido);
                        cmd.Parameters.AddWithValue("@idproducto", x.idproducto);
                        cmd.Parameters.AddWithValue("@precio", x.preciocompra);
                        cmd.Parameters.AddWithValue("@cant", x.cantidad);
                        cmd.Parameters.AddWithValue("@montot", x.monto);
                        monto += x.monto;
                        idproveedor = x.idproveedor;
                        cmd.ExecuteNonQuery();
                    });

                    temporal.ForEach(x =>
                    {
                        cmd = new SqlCommand(
                        "update producto set cantidad+=@cantidad Where idproducto=@idproducto", cn, tr);
                        cmd.Parameters.AddWithValue("@idproducto", x.idproducto);
                        cmd.Parameters.AddWithValue("@cantidad", x.cantidad);
                        cmd.ExecuteNonQuery();
                    });

                    tr.Commit();
                    mensaje = "Pedido Registrado";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message; tr.Rollback();
                }
                finally { cn.Close(); }
            }
            ViewBag.idproveedor = idproveedor;
            ViewBag.suma = monto;
            ViewBag.mensaje = mensaje;
            return View(await Task.Run(() => reg));
        }

        public async Task<IActionResult> listproveedor(int p = 0, string idproveedor = "")
        {
            ViewBag.idproveedor = idproveedor;
            IEnumerable<Proveedor> temporal = _proveedor.GetProveedores();
            IEnumerable<Proveedor> temporal1 = _proveedor.GetProveedores(idproveedor);
            int f = 7;
            int c = temporal.Count();
            int pags = c % f == 0 ? c / f : c / f + 1;
            ViewBag.p = p;
            ViewBag.pags = pags;
            if (idproveedor == null)
                return View(await Task.Run(() => temporal.Skip(f * p).Take(f)));

            return View(await Task.Run(() => temporal1.Skip(f * p).Take(f)));
        }

        public async Task<IActionResult> listboleta()
        {

            return View(await Task.Run(() => _comprobante.listado()));
        }

        public async Task<IActionResult> listdetalleboleta(string id = "")
        {
            IEnumerable<DetalleCompra> temporal1 = _comprobante.GetBoleta(id);
            return View(await Task.Run(() => temporal1));
        }
    }
        */
    }
}
