using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud_DotNET.Models
{

    [Table("TipoUsuario")]
    public class Departamento
    {
        [Key]
        [Column("Id")]
        [Display(Description = "Código")]
        public int Id { get; set; }

        [Column("Tipo")]
        [Required(ErrorMessage = "O nome do departamento é obrigatório", AllowEmptyStrings = false)]
        [Display(Description = "Tipo")]
        public string Tipo { get; set; }


    }

}

