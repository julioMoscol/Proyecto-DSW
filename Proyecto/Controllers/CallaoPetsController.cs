﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;

namespace Proyecto.Controllers
{
    
    public class CallaoPetsController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ICliente _cliente;
        public CallaoPetsController()
        {
            _producto =new ProductoSQl();
            _proveedor = new ProveedorSQL();
            _trabajador = new TrabajadorSQL();
            _cliente = new ClienteSQL();
        }
        public async Task< IActionResult> Index()
        {
            return View(await Task.Run(()=>_proveedor.GetProveedor()));
        }
        public async Task<IActionResult> Trabajador()
        {
            return View(await Task.Run(()=>_trabajador.GetTrabajador()));
        }
        public async Task<IActionResult> Producto()
        {
            return View(await Task.Run(()=>_producto.GetProducto()));
        }

        /*CLIENTE*/
        public async Task<IActionResult> Cliente(string nom = "")
        {
            ViewBag.nom = nom;

            return View(await Task.Run(() => _cliente.GetClientes(nom)));
        }
    }
}
