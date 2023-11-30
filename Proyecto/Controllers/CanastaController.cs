using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;

namespace Proyecto.Controllers
{
    [Authorize]
    public class CanastaController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;
        ICliente _cliente;
        ICategoria _categoria;
        IAnimal _animal;
        public CanastaController()
        {
            _producto = new ProductoSQl();
            _proveedor = new ProveedorSQL();
            _trabajador = new TrabajadorSQL();
            _cliente = new ClienteSQL();
            _categoria = new CategoriaSQL();
            _animal = new AnimalSQL();
        }
        public async Task<IActionResult> Carrito()
        {
            return View(await Task.Run(() => _producto.GetProducto()));
        }
    }
}
