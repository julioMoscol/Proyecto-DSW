using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;

namespace Proyecto.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ICliente _cliente;
        ICategoria _categoria;
        IAnimal _animal;
        public ProductoController()
        {
            _producto = new ProductoSQl();
            _proveedor = new ProveedorSQL();
            _trabajador = new TrabajadorSQL();
            _cliente = new ClienteSQL();
            _categoria = new CategoriaSQL();
            _animal = new AnimalSQL();
        }
        public async Task<IActionResult> list()
        {
            return View(await Task.Run(() => _producto.GetProducto()));
        }
        public async Task<IActionResult> Detalle(int id)
        {

            Producto reg = _producto.GetProductoID(id);
            if (reg == null)
                return RedirectToAction("list");
            else
                return View(await Task.Run(() => reg));
        }

        public async Task<IActionResult> Delete(int? id = null)
        {
            if (id == null) { return RedirectToAction("list"); }
            Producto reg = _producto.GetProductoID(id);

            return View(await Task.Run(() => reg));

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Producto reg,int id)
        {
            reg.idproducto = id;
            ViewBag.mensaje = _producto.eliminarProducto(reg);
            return View(await Task.Run(() => reg));

        }

        public async Task<IActionResult> Edit(int? id = null)
        {
            if (id == null) { return RedirectToAction("list"); }
            Producto reg = _producto.GetProductoID(id);
            ViewBag.proveedor = new SelectList(_proveedor.GetProveedor(), "idProveedor", "empresa");
            ViewBag.categoria = new SelectList(_categoria.getCategoria(), "idcat", "nomcat");
            ViewBag.animal = new SelectList(_animal.getAnimal(), "idanimal", "descanimal");
            return View(await Task.Run(() => reg));

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Producto reg)
        {

            ViewBag.mensaje = _producto.actualizarProducto(reg);
            return View(await Task.Run(() => reg));

        }

        public async Task<IActionResult> Create()

        {
            ViewBag.proveedor = new SelectList(_proveedor.GetProveedor(), "idProveedor", "empresa");
            ViewBag.categoria = new SelectList(_categoria.getCategoria(), "idcat", "nomcat");
            ViewBag.animal = new SelectList(_animal.getAnimal(), "idanimal", "descanimal");
            return View(await Task.Run(() => new Producto()));

        }
        [HttpPost]
        public async Task<IActionResult> Create(Producto reg)
        {
            if (!ModelState.IsValid)
                return View(await Task.Run(() => _producto.agregarProducto(reg)));
            else
            {
                reg.idproducto = _producto.autogenera();
                ViewBag.mensaje = _producto.agregarProducto(reg);
                return View(await Task.Run(() => reg));
            }

        }

       

    }
}
