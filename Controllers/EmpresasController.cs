using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sistema_IAM.Models;
using Sistema_IAM.Services;
using Sistema_IAM.ViewModels;

namespace Sistema_IAM.Controllers
{
    public class EmpresasController : Controller
    {

        private readonly EmpresaService _empresaService;

        public EmpresasController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        public IActionResult Index()
        {
            var empresas = _empresaService
                .ObtenerTodos()
                .Select(u => new EmpresaViewModel
                {
                    Id = u.Id,
                    Nombre = u.Nombre
                })
                .ToList();

            return View(empresas);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new EmpresaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(EmpresaViewModel empresa)
        {
            if (!ModelState.IsValid)
            {
                return View("Crear", empresa);
            }

            var nuevaEmpresa = new Empresa
            {
                Nombre = empresa.Nombre,
            };

            _empresaService.Registrar(nuevaEmpresa);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var empresa = _empresaService.ObtenerTodos().FirstOrDefault(u => u.Id == id);

            if (empresa == null)
            {
                return NotFound();
            }

            var empresaViewModel = new EmpresaViewModel
            {
                Id = empresa.Id,
                Nombre = empresa.Nombre,
            };

            return View(empresaViewModel);
        }

        [HttpPost]
        public IActionResult Actualizar(EmpresaViewModel empresa)
        {
            if (!ModelState.IsValid)
            {
                return View("Editar", empresa);
            }

            var empresaActualizada = new Empresa
            {
                Id = empresa.Id,
                Nombre = empresa.Nombre,
            };

            _empresaService.Actualizar(empresaActualizada);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            _empresaService.Eliminar(id);

            return RedirectToAction("Index");
        }
    }
}