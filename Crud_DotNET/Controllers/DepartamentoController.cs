using Crud_DotNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_DotNET.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly Contexto _context;

        public DepartamentoController(Contexto context)
        {
            _context = context;
        }


        // GET: TipoUsuarios
        public async Task<IActionResult> Index()
        {
            return _context.Departamento != null ?
                        View(await _context.Departamento.ToListAsync()) :
                        Problem("Entity set 'Contexto.TipoUsuario'  is null.");

        }

        // GET: TipoUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departamento == null)
            {
                return NotFound();
            }

            var tipoUsuario = await _context.Departamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return View(tipoUsuario);
        }

        // GET: TipoUsuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Tipo")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                TempData["Mensagem"] = "Departamento criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: TipoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departamento == null)
            {
                return NotFound();
            }

            var tipoUsuario = await _context.Departamento.FindAsync(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }
            return View(tipoUsuario);
        }

        // POST: TipoUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,Tipo")] Departamento departamento)
        {

            if (Id != departamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoUsuarioExists(departamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Mensagem"] = "Alteração gravada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: TipoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departamento == null)
            {
                return NotFound();
            }

            var tipoUsuario = await _context.Departamento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return View(tipoUsuario);
        }

        // POST: TipoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departamento == null)
            {
                return Problem("Entity set 'Contexto.TipoUsuario'  is null.");
            }
            var tipoUsuario = await _context.Departamento.FindAsync(id);

            var lista = CarregaDados(id).Count;

            if (tipoUsuario != null && lista == 0)
            {
                _context.Departamento.Remove(tipoUsuario);
                await _context.SaveChangesAsync();
                TempData["Mensagem"] = "Departamento excluído com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            await _context.SaveChangesAsync();
            TempData["Mensagem"] = "Departamento com funcionários vinculados!";
            return RedirectToAction(nameof(Delete));

        }

        private bool TipoUsuarioExists(int id)
        {
            return (_context.Departamento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public List<Usuario> CarregaDados(int id)

        {

            var lista = (from u in _context.Usuario
                         join t in _context.Departamento on u.Tipo equals t.Id
                         where t.Id == id
                         select new Usuario
                         {
                             Id = u.Id,
                             Idade = u.Idade,
                             Tipo = u.Tipo,
                             NomeUsuario = u.NomeUsuario,
                             TipoDescricao = t.Tipo
                         }).ToList();

            return lista;

        }

    }
}
