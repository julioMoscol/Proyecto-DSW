using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Models;
using Proyecto.Repositorio.Interface;
using Proyecto.Repositorio.RepositorioSQL;
using System.Diagnostics;
using System.Security.Claims;

namespace Proyecto.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IProducto _producto;
        ITrabajador _trabajador;
        IProveedor _proveedor;

        public HomeController()
        {
            _producto = new ProductoSQl();
            _proveedor = new ProveedorSQL();
            _trabajador = new TrabajadorSQL();
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            string correo = "";
            if (claimuser.Identity.IsAuthenticated)
            {
                correo = claimuser.Claims.Where(C => C.Type == ClaimTypes.Name).
                Select(c => c.Value).SingleOrDefault();
            }
            ViewBag.msj = _trabajador.GetUsuario(correo).nomtrabajador ;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "Usuario");
        }
    }
}