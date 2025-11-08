using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Data;
using ProjetoMVC.Models;

namespace ProjetoMVC.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly AppDbContext _context;
        public FornecedorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Fornecedores.ToList());
        }
        [HttpPost]
        public IActionResult Create(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                _context.Fornecedores.Add(fornecedor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var fornecedor = _context.Fornecedores.Find(id);
            if (fornecedor == null) return NotFound();
            return View(fornecedor);
        }
        [HttpPost]
        public IActionResult Edit(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                _context.Fornecedores.Update(fornecedor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var fornecedor =_context.Fornecedores.Find(id);
            if (fornecedor == null) return NotFound();
            return View(fornecedor);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var fornecedor = _context.Fornecedores.Find(id);
            if (fornecedor != null)
            {
                _context.Fornecedores.Remove(fornecedor);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
