using EFSRT_RopaStore.Recursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;

namespace Proyecto.Controllers
{
    public class ClienteController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ICliente _cliente;
        public ClienteController()
        {
            _producto = new ProductoSQl();
            _proveedor = new ProveedorSQL();
            _trabajador = new TrabajadorSQL();
            _cliente = new ClienteSQL();
        }
        public async Task<IActionResult> list(string nom = "")
        {
            ViewBag.nom = nom;

            return View(await Task.Run(() => _cliente.GetClientes(nom)));
        }
        public async Task<IActionResult> Create()
        {
            

            return View(await Task.Run(() => new Cliente()));
        }


        [HttpPost]
        public async Task<IActionResult> Create(Cliente reg)
        {
            reg.password = Utilidades.EncriptarClave(reg.password);
            reg.idcliente = _cliente.autogenera();
            ViewBag.mensaje = _cliente.agregarCliente(reg);
            return View(await Task.Run(() => reg));
        }
        public async Task<IActionResult> Edit(int? id = null)
        {
            if (id == null)
                return RedirectToAction("list");
            Cliente reg = _cliente.buscarCliente(id.Value);

            return View(await Task.Run(() => reg));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Cliente reg)
        {
            ViewBag.mensaje = _cliente.modificarCliente(reg);

            return View(await Task.Run(() => reg));
        }
        public async Task<IActionResult> Details(int? id = null)
        {
            if (id == null)
                return RedirectToAction("Index");
            else
                return View(await Task.Run(() => _cliente.buscarCliente(id.Value)));
        }

        public async Task<IActionResult> Delete(int? id = null)
        {
            if (id == null)
                return RedirectToAction("list");

            return View(await Task.Run(() => _cliente.buscarCliente(id.Value)));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Cliente reg, int? id = null)
        {
            ViewBag.mensaje = _cliente.eliminarCliente(_cliente.buscarCliente(id.Value));
            return View(await Task.Run(() => reg));


        }
    }
}
