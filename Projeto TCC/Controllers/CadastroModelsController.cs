using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto_TCC.Data;
using Projeto_TCC.Models;

namespace Projeto_TCC.Controllers
{
    public class CadastroModelsController : Controller
    {
        private readonly BancoContext _context;

        public CadastroModelsController(BancoContext context)
        {
            _context = context;
        }

        // GET: CadastroModels
        public async Task<IActionResult> Index()
        {
              return _context.cadastro != null ? 
                          View(await _context.cadastro.ToListAsync()) :
                          Problem("Entity set 'BancoContext.cadastro'  is null.");
        }

        // GET: CadastroModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.cadastro == null)
            {
                return NotFound();
            }

            var cadastroModel = await _context.cadastro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cadastroModel == null)
            {
                return NotFound();
            }

            return View(cadastroModel);
        }

        // GET: CadastroModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CadastroModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LugarViagem,datede,dataate,valor,contato")] CadastroModel cadastroModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cadastroModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cadastroModel);
        }

        // GET: CadastroModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.cadastro == null)
            {
                return NotFound();
            }

            var cadastroModel = await _context.cadastro.FindAsync(id);
            if (cadastroModel == null)
            {
                return NotFound();
            }
            return View(cadastroModel);
        }

        // POST: CadastroModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LugarViagem,datede,dataate,valor,contato")] CadastroModel cadastroModel)
        {
            if (id != cadastroModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cadastroModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CadastroModelExists(cadastroModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cadastroModel);
        }

        // GET: CadastroModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.cadastro == null)
            {
                return NotFound();
            }

            var cadastroModel = await _context.cadastro
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cadastroModel == null)
            {
                return NotFound();
            }

            return View(cadastroModel);
        }

        // POST: CadastroModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.cadastro == null)
            {
                return Problem("Entity set 'BancoContext.cadastro'  is null.");
            }
            var cadastroModel = await _context.cadastro.FindAsync(id);
            if (cadastroModel != null)
            {
                _context.cadastro.Remove(cadastroModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CadastroModelExists(int id)
        {
          return (_context.cadastro?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
