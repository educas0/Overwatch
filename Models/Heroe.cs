using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch.Models
{
    /// <summary>
    /// Clase de Heroe
    /// </summary>
    public class Heroe
    {
        public int IdHeroe { get; set; }

        [Required(ErrorMessage = "* Nombre obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder de 50 caracteres")]
        public string Nombre { get; set; }

        public string Rol { get; set; }

        public int Vida { get; set; }
        public int Dano { get; set; }
        public int Cura { get; set; }

    }
}
