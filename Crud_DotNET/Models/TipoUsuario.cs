using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Crud_DotNET.Models
{

    [Table("TipoUsuario")]
    public class TipoUsuario
    {
        [Key]
        [Column("Id")]
        [Display(Description = "Código")]
        public int id { get; set; }

        [Column("Tipo")]
        [Display(Description = "Tipo")]
        public string Tipo { get; set; }

    }
}

