using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PryLogin.Models;
using PryLogin.Controllers;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
        public IActionResult Index(Usuario _usuario)
        {
            var usuario = _context.ValidarUsuario(_usuario.Email, _usuario.Clave);

            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [Route ("logout")]

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Acceso");
        }

    }
}
