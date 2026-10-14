using System.Text.Json;
using Sistema_IAM.Models;

namespace Sistema_IAM.Services
{
    public class EmpresaService
    {
        private readonly string _rutaArchivo;

        public EmpresaService()
        {
            _rutaArchivo = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "empresas.json"
            );
        }

        public List<Empresa> ObtenerTodos()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<Empresa>();
            }

            var json = File.ReadAllText(_rutaArchivo);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Empresa>();
            }

            return JsonSerializer.Deserialize<List<Empresa>>(json)
                   ?? new List<Empresa>();
        }

        public void Registrar(Empresa empresa)
        {
            var empresas = ObtenerTodos();

            empresa.Id = empresas.Count > 0
                ? empresas.Max(u => u.Id) + 1
                : 1;

            empresas.Add(empresa);

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(empresas, opciones);

            File.WriteAllText(_rutaArchivo, json);
        }

        public void Actualizar(Empresa empresa)
        {
            var empresas = ObtenerTodos();

            var empresaExistente = empresas.FirstOrDefault(u => u.Id == empresa.Id);

            if (empresaExistente == null)
            {
                return;
            }

            empresaExistente.Nombre = empresa.Nombre;

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(empresas, opciones);

            File.WriteAllText(_rutaArchivo, json);
        }

        public void Eliminar(int id)
        {
            var empresas = ObtenerTodos();

            var empresa = empresas.FirstOrDefault(u => u.Id == id);

            if (empresa == null)
            {
                return;
            }

            empresas.Remove(empresa);

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(empresas, opciones);

            File.WriteAllText(_rutaArchivo, json);
        }
    }
}