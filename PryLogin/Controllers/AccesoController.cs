using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PryLogin.Models;
using PryLogin.Controllers;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace PryLogin.Controllers
{
    public class AccesoController : Controller
    {

        private readonly WebContext _context;

        public AccesoController(WebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async  Task <IActionResult> Index(Usuario _usuario)
        {
            var usuario = _context.ValidarUsuario(_usuario.Email, _usuario.Clave);

            if (usuario != null)

            {
                //Creacion de la cookie de autorizacion 

                var claims = new List<Claim> {

                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim("Correo", usuario.Email),
                };

                var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [Route ("logout")]

        public async Task <IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Acceso");
        }

    }
}
