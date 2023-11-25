using EFSRT_RopaStore.Recursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;

namespace Proyecto.Controllers
{
    [Authorize]
    public class TrabajadorController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ICliente _cliente;
        ICargo _cargo;
        IArea _area;
        public TrabajadorController()
        {
            _producto = new ProductoSQl();
            _proveedor = new ProveedorSQL();
            _trabajador = new TrabajadorSQL();
            _cliente = new ClienteSQL();
            _cargo = new CargoSQL();
            _area = new AreaSQL();
        }
        public async Task<IActionResult> list()
        {
            return View(await Task.Run(() => _trabajador.GetTrabajador()));
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.area = new SelectList(_area.listado(), "idarea", "descripcion");
            ViewBag.cargo = new SelectList(_cargo.listado(), "idcargo", "descripcion");
            return View(await Task.Run(() => new Trabajador()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Trabajador reg)
        {
            reg.password = Utilidades.EncriptarClave(reg.password);
            reg.idtrabajador = _trabajador.autogenera();
            ViewBag.area = new SelectList(_area.listado(), "idarea", "descripcion");
            ViewBag.cargo = new SelectList(_cargo.listado(), "idcargo", "descripcion");
            ViewBag.mensaje = _trabajador.agregarTrabajador(reg);
            return View(await Task.Run(() => reg));
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.area = new SelectList(_area.listado(), "idarea", "descripcion");
            ViewBag.cargo = new SelectList(_cargo.listado(), "idcargo", "descripcion");
            if (id == 0)
                return RedirectToAction("list");
            Trabajador reg = _trabajador.GetTrabajador(id);
            return View(await Task.Run(() => reg));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Trabajador reg)
        {
            reg.password = Utilidades.EncriptarClave(reg.password);
            ViewBag.area = new SelectList(_area.listado(), "idarea", "descripcion");
            ViewBag.cargo = new SelectList(_cargo.listado(), "idcargo", "descripcion");
            ViewBag.mensaje = _trabajador.actualizarTrabajador(reg);
            return View(await Task.Run(() => reg));

        }

        public ActionResult Delete(int id)
        {
            if (id==0)
                return RedirectToAction("list");
            _trabajador.eliminarTrabajador(_trabajador.GetTrabajador(id));
            return RedirectToAction("list");
        }

        public async Task<IActionResult> Details(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("list");
            else
                return View(await Task.Run(() => _trabajador.GetTrabajador(id)));
        }
    }
}
