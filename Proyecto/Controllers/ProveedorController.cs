using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;

namespace Proyecto.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ICliente _cliente;
        public ProveedorController()
        {
            _producto = new ProductoSQl();
            _proveedor = new ProveedorSQL();
            _trabajador = new TrabajadorSQL();
            _cliente = new ClienteSQL();
        }
        public async Task<IActionResult> list()
        {
            return View(await Task.Run(() => _proveedor.GetProveedor()));
        }

        public async Task<IActionResult> Create()
        {
            return View(await Task.Run(() => new Proveedor()));
        }

        [HttpPost] public async Task<IActionResult> Create(Proveedor reg)
        {
            if (!ModelState.IsValid)
                return View(await Task.Run(() => _proveedor.agregarProveedor(reg)));
            else
            {
                reg.idProveedor = _proveedor.autogenera();
                ViewBag.mensaje = _proveedor.agregarProveedor(reg);
                return View(await Task.Run(() => reg));
            }
        }

        public async Task<IActionResult> Edit(int ? id = null)
        {
            if (id == null)
                return RedirectToAction("list");
            Proveedor reg = _proveedor.Buscar(id);
            return View(await Task.Run(() => reg));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Proveedor reg)
        {
            ViewBag.mensaje = _proveedor.actualizarProveedor(reg);
            return View(await Task.Run(() => reg));
        }

        public async Task<IActionResult> Details(int ? id = null)
        {
            if (id == null)
                return RedirectToAction("list");
            else
                return View(await Task.Run(() => _proveedor.Buscar(id)));
        }

        public async Task<IActionResult> Delete(int? id = null)
        {
            if (id == null)
                return RedirectToAction("list");

            return View(await Task.Run(() => _proveedor.Buscar(id.Value)));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Proveedor reg, int ? id = null)
        {
            ViewBag.mensaje = _proveedor.eliminarProveedor(_proveedor.Buscar(id.Value));
            return View(await Task.Run(() => reg));
        }


    }
}
