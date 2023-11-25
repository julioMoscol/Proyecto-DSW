﻿using EFSRT_RopaStore.Recursos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Proyecto.Models;
using System.Data;
using System.Security.Claims;

namespace Proyecto.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly string cadena;

        public UsuarioController()
        {
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetConnectionString("sql");
        }

        public ActionResult login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(Trabajador t)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cadena))
                {
                    using (SqlCommand cmd = new("usp_ingresosistematrabajador", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@correo", t.correo);
                        cmd.Parameters.AddWithValue("@clave", Utilidades.EncriptarClave(t.password));
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["correo"] != null && t.correo != null)
                            {
                                List<Claim> c = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.Name,t.correo),

                                };
                                ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();

                                p.AllowRefresh = true;
                                p.IsPersistent = t.manteneractivo;

                                if (!t.manteneractivo)
                                {
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10);
                                }
                                else
                                {
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1);
                                }

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);
                                return RedirectToAction("Index", "Home");
                            } 
                            else
                            {
                               
                                ViewBag.Error = "Credenciales incorrectas o cuenta no registrada";
                            }

                        }
                        con.Close();
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", "Home");

        }
    }
}
