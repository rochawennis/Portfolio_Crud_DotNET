using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Crud_DotNET.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("Id")]
        [Display(Description = "Código")]
        public int Id { get; set; }

        [Display(Description = "Nome do usuario")]
        [Required(ErrorMessage = "O nome do funcionário é obrigatório", AllowEmptyStrings = false)]
        public string NomeUsuario { get; set; }

        [Column("Idade")]
        [Display(Description = "Idade")]
        [Required(ErrorMessage = "A idade do funcionário é obrigatório", AllowEmptyStrings = false)]

        public int Idade { get; set; }

        [Column("Tipo")]
        [Required(ErrorMessage = "O departamento do funcionário é obrigatório", AllowEmptyStrings = false)]
        [Display(Description = "Tipo de usuário")]
        public int Tipo { get; set; }

        
        [NotMapped]
        public string TipoDescricao { get; set; }

    }
}
