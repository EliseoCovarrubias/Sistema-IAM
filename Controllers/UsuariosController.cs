using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sistema_IAM.Models;
using Sistema_IAM.Services;
using Sistema_IAM.ViewModels;

namespace Sistema_IAM.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            var usuarios = _usuarioService
                .ObtenerTodos()
                .Select(u => new UsuarioViewModel
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Correo = u.Correo
                })
                .ToList();

            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new UsuarioViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return View("Crear", usuario);
            }

            var nuevoUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Correo = usuario.Correo
            };

            _usuarioService.Registrar(nuevoUsuario);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var usuario = _usuarioService.ObtenerTodos().FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioViewModel = new UsuarioViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo
            };

            return View(usuarioViewModel);
        }

        [HttpPost]
        public IActionResult Actualizar(UsuarioViewModel usuario)
        {
            if (!ModelState.IsValid)
            {
                return View("Editar", usuario);
            }

            var usuarioActualizado = new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo
            };

            _usuarioService.Actualizar(usuarioActualizado);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            _usuarioService.Eliminar(id);

            return RedirectToAction("Index");
        }
    }
}