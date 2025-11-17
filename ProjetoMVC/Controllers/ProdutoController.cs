using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Data;
using ProjetoMVC.Models;

namespace ProjetoMVC.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;//_context é atributo declarado de DBcontext q  identifica o tipo de classe que está trabalhando
        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }
        /*public IActionResult Index()
        {
            return View(_context.Produtos.ToList());
        }*/

        public async Task<IActionResult> Index()
        {
            var produtos = _context.Produtos.Include(p => p.Fornecedor);
            return View( await produtos.ToListAsync());
        }//Join para mostrar o nome e id do fornecedor, feito de forma assíncrona e espera concluir pra enviar 

        [HttpPost]
        public IActionResult Create(Produto produto)
        {
           // if (ModelState.IsValid) // ModelState vai validar se todos os campos estão preenchidos 
            //{
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return RedirectToAction("Index");
           // }

            ViewData["FornecedorId"] = new SelectList(
                _context.Fornecedores, "Id", "Nome", produto.FornecedorId);//Id e nome para gravar e salvar
            return View(produto);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(
               _context.Fornecedores, "Id", "Nome");// Só exibição dos fornecedores
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> Edit (int ? id)
        {
            if (id == null)
            
                return NotFound();
                var produto = await _context.Produtos
                   .AsNoTracking()
                   .FirstOrDefaultAsync(p => p.Id == id);

                if (produto == null)
                    return NotFound();
                
                ViewData["FornecedorId"] = new SelectList(
                    await _context.Fornecedores.ToListAsync(),
                    "Id", "Nome", produto.FornecedorId);
                return View(produto);// puxa o produto, associando  no select list o fornecedorId

        }
       /* public IActionResult Edit(int id)
        {
            var produto = _context.Produtos.Find(id);
            if(produto == null) return NotFound();
            return View(produto);
        }*/
        [HttpPost]
        //public IActionResult Edit(Produto produto)
        public async Task <IActionResult> Edit (int id, Produto produto)
        {
            //if (ModelState.IsValid)
            if (id != produto.Id) return NotFound();
            try
            {
                //{
                _context.Produtos.Update(produto);
                _context.SaveChanges();
                return RedirectToAction("Index");
                //} List de produtos é igual o id de produtos
            } catch(DbUpdateConcurrencyException)
            {
                if (!_context.Produtos.Any(e =>
                e.Id == produto.Id)) return NotFound();
                else throw;
            }
            ViewData["FornecedorId"] = new SelectList(
                    await _context.Fornecedores.ToListAsync(),
                    "Id", "Nome", produto.FornecedorId);
            return View(produto); // Puxa as outras informações após passar o Id e carrega na tela
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var produto = _context.Produtos.Find(id);
            if(produto == null) return NotFound();
            return View(produto);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var produto = _context.Produtos.Find(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
