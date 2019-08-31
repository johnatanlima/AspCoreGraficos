using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspCoreGraficos.Data;
using AspCoreGraficos.Models;

namespace AspCoreGraficos.Controllers
{
    public class MoedaController : Controller
    {
        private readonly MoedaDbContexto _context;

        public MoedaController(MoedaDbContexto context)
        {
            _context = context;
        }

        // GET: Moeda
        public async Task<IActionResult> Index()
        {
            return View(await _context.Moedas.ToListAsync());
        }

        // GET: Moeda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moeda = await _context.Moedas
                .FirstOrDefaultAsync(m => m.MoedaId == id);
            if (moeda == null)
            {
                return NotFound();
            }

            return View(moeda);
        }

        //IMPLEMENTAÇÃO
        public async Task<IActionResult> EscolhaMoedas(IList<Moeda> moedasParam)
        {
            foreach (var item in moedasParam)
            {
                if (item.CheckboxMarcado == true)
                {
                    Moeda varMoeda = await _context.Moedas.FirstAsync(x => x.MoedaId == item.MoedaId);
                    varMoeda.Quantidade = varMoeda.Quantidade + 1;

                    _context.Update(varMoeda);
                    await _context.SaveChangesAsync();

                }
            }

            return RedirectToAction(nameof(Index));
        }


        //IMPLEMENTAÇÃO
        public JsonResult DadosGrafico()
        {
            return Json(_context.Moedas.Select(x => new {x.Nome, x.Quantidade}));
        }

        // GET: Moeda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Moeda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoedaId,Nome,Quantidade")] Moeda moeda)
        {
            moeda.Quantidade = 0;
            
            if (ModelState.IsValid)
            {
                _context.Add(moeda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moeda);
        }

        // GET: Moeda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moeda = await _context.Moedas.FindAsync(id);
            if (moeda == null)
            {
                return NotFound();
            }
            return View(moeda);
        }

        // POST: Moeda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoedaId,Nome,Quantidade")] Moeda moeda)
        {
            if (id != moeda.MoedaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moeda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoedaExists(moeda.MoedaId))
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
            return View(moeda);
        }

        // GET: Moeda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moeda = await _context.Moedas
                .FirstOrDefaultAsync(m => m.MoedaId == id);
            if (moeda == null)
            {
                return NotFound();
            }

            return View(moeda);
        }

        // POST: Moeda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moeda = await _context.Moedas.FindAsync(id);
            _context.Moedas.Remove(moeda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoedaExists(int id)
        {
            return _context.Moedas.Any(e => e.MoedaId == id);
        }
    }
}
