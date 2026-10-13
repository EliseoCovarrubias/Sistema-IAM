using System.Text.Json;
using Sistema_IAM.Models;

namespace Sistema_IAM.Services
{
    public class UsuarioService
    {
        private readonly string _rutaArchivo;

        public UsuarioService()
        {
            _rutaArchivo = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Data",
                "usuarios.json"
            );
        }

        public List<Usuario> ObtenerTodos()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<Usuario>();
            }

            var json = File.ReadAllText(_rutaArchivo);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Usuario>();
            }

            return JsonSerializer.Deserialize<List<Usuario>>(json)
                   ?? new List<Usuario>();
        }

        public void Registrar(Usuario usuario)
        {
            var usuarios = ObtenerTodos();

            usuario.Id = usuarios.Count > 0
                ? usuarios.Max(u => u.Id) + 1
                : 1;

            usuarios.Add(usuario);

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(usuarios, opciones);

            File.WriteAllText(_rutaArchivo, json);
        }

        public void Actualizar(Usuario usuario)
        {
            var usuarios = ObtenerTodos();

            var usuarioExistente = usuarios.FirstOrDefault(u => u.Id == usuario.Id);

            if (usuarioExistente == null)
            {
                return;
            }

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Correo = usuario.Correo;

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(usuarios, opciones);

            File.WriteAllText(_rutaArchivo, json);
        }

        public void Eliminar(int id)
        {
            var usuarios = ObtenerTodos();

            var usuario = usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return;
            }

            usuarios.Remove(usuario);

            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(usuarios, opciones);

            File.WriteAllText(_rutaArchivo, json);
        }
    }
}