namespace Sistema_IAM.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }
    }
}



/*
using System.ComponentModel.DataAnnotations;

namespace SistemaUsuarios.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "El nombre es obligatorio.")]
        //[StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        //[Required(ErrorMessage = "El correo es obligatorio.")]
        //[EmailAddress(ErrorMessage = "El correo no es válido.")]
        //[StringLength(150)]
        public string Correo { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Puesto { get; set; }

        [StringLength(50)]
        public string? Expediente { get; set; }

        // Claves foráneas de los catálogos
        [Required]
        public int IdEmpresa { get; set; }

        [Required]
        public int IdTipoUsuario { get; set; }

        // Propiedades de navegación
        //public Empresa? Empresa { get; set; }

        //public TipoUsuario? TipoUsuario { get; set; }

        public bool Estado { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaActualizacion { get; set; }

    }
}

*/