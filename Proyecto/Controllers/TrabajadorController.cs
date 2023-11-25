using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public TrabajadorController()
        {
            _producto = new ProductoSQl();
            _proveedor = new ProveedorSQL();
            _trabajador = new TrabajadorSQL();
            _cliente = new ClienteSQL();
        }
        public async Task<IActionResult> list()
        {
            return View(await Task.Run(() => _trabajador.GetTrabajador()));
        }
    }
}
