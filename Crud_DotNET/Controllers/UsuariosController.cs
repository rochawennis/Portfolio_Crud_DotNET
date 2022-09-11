using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crud_DotNET.Models;
using Microsoft.Data.SqlClient;

namespace Crud_DotNET.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly Contexto _contexto;

        public UsuariosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            //Somente utilizar na primeira execução
            //IniciaCadastro();

            //var lista = _contexto.Usuario.ToList();

            var lista = (from u in _contexto.Usuario
                         join t in _contexto.Departamento on u.Tipo equals t.Id
                         select new Usuario
                         {
                             Id = u.Id,
                             Idade = u.Idade,
                             Tipo = u.Tipo,
                             NomeUsuario = u.NomeUsuario,
                             TipoDescricao = t.Tipo
                         }).ToList();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var usuario = new Usuario();
            CarregaTipoUsuario();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            _contexto.Usuario.Add(usuario);
            _contexto.SaveChanges();
            TempData["Mensagem"] = "Funcionário criado com sucesso!";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var usuario = _contexto.Usuario.Find(Id);

            CarregaTipoUsuario();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Edit(Usuario usuario)
        {
            _contexto.Usuario.Update(usuario);
            _contexto.SaveChanges();
            TempData["Mensagem"] = "Alteração gravada com sucesso!";
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var usuario = _contexto.Usuario.Find(Id);
            CarregaTipoUsuario();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Delete(Usuario _usuario)
        {
            var usuario = _contexto.Usuario.Find(_usuario.Id);
            if (usuario != null)
            {
                _contexto.Usuario.Remove(usuario);
                _contexto.SaveChanges();
                TempData["Mensagem"] = "Funcionário excluído com sucesso!";
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            var usuario = _contexto.Usuario.Find(Id);
            CarregaTipoUsuario();
            return View(usuario);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CarregaTipoUsuario()
        {
            var ItensTipoUsuario = new List<SelectListItem>();

            //var tipoUsuario = _contexto.TipoUsuario.ToList();

            var tipoUsuarioProcedure = _contexto.Departamento.FromSqlRaw("ListaTipoUsuario").ToList();

            tipoUsuarioProcedure.ForEach(t =>
            {
                ItensTipoUsuario.Add(new SelectListItem { Value = t.Id.ToString(), Text = t.Tipo });
            });

            //var ItensTipoUsuario = new List<SelectListItem>
            //{
            //    new SelectListItem{ Value ="1", Text ="Administrador"},
            //     new SelectListItem{ Value ="2", Text ="Técnico"},
            //      new SelectListItem{ Value ="3", Text ="Usuário Normal"}
            //};

            ViewBag.TiposUsuario = ItensTipoUsuario;
        }



        private void IniciaCadastro()
        {
            //_contexto.TipoUsuario.Add(new TipoUsuario { Tipo = "Administrador" });
            //_contexto.SaveChanges();

            var listaTipoUsuario = new List<Departamento>();


            foreach (var item in listaTipoUsuario)
            {
                var parametro1 = new SqlParameter("@Tipo", item.Tipo);
                _contexto.Database.ExecuteSqlRaw("SalvaTipoUsuario @Tipo", parametro1);
                _contexto.SaveChanges();
            }


        }



    }
}
