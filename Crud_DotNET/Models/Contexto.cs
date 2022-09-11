using Microsoft.EntityFrameworkCore;

namespace Crud_DotNET.Models
{
    public class Contexto : DbContext

    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<TipoUsuario> TipoUsuario { get; set; }

    }

}
