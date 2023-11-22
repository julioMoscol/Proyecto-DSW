using Microsoft.AspNetCore.Mvc;
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
    }
}
