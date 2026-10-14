using System.ComponentModel.DataAnnotations;

namespace Sistema_IAM.ViewModels
{
    public class EmpresaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        public bool Activo { get; set; } = true;
    }
    
}