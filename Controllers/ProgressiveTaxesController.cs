using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    public class ProgressiveTaxesController : Controller
    {
        private readonly TaxCalculatorContext _context;

        public ProgressiveTaxesController(TaxCalculatorContext context)
        {
            _context = context;
        }

        // GET: ProgressiveTaxes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProgressiveTaxes.Where(x => x.IsDeleted == false).ToListAsync());
        }

        // GET: ProgressiveTaxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progressiveTax = await _context.ProgressiveTaxes
                .FirstOrDefaultAsync(m => m.ProgressiveTaxID == id && m.IsDeleted == false);
            if (progressiveTax == null)
            {
                return NotFound();
            }

            return View(progressiveTax);
        }

        // GET: ProgressiveTaxes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProgressiveTaxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgressiveTaxID,Rate,From,To")] ProgressiveTax progressiveTax)
        {
            if (_context.ProgressiveTaxes.Where(x => x.Rate == progressiveTax.Rate && x.IsDeleted == false).FirstOrDefault() != null)
                return Forbid(); //Duplicate Rate

            if (ModelState.IsValid)
            {
                progressiveTax.CreatedDate = progressiveTax.LastModifiedDate = DateTime.Now;
                progressiveTax.CreatedBy = progressiveTax.LastModifiedBy = "System";
                progressiveTax.IsDeleted = false;

                _context.Add(progressiveTax);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(progressiveTax);
        }

        // GET: ProgressiveTaxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var progressiveTax = await _context.ProgressiveTaxes.Where(x => x.ProgressiveTaxID == id && x.IsDeleted == false).FirstOrDefaultAsync();

            if (progressiveTax == null)
                return NotFound();

            return View(progressiveTax);
        }

        // POST: ProgressiveTaxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgressiveTaxID,Rate,From,To")] ProgressiveTax progressiveTax)
        {
            ProgressiveTax updateRec = _context.ProgressiveTaxes.Where(x => x.ProgressiveTaxID == id && x.IsDeleted == false).FirstOrDefault();

            if (id != progressiveTax.ProgressiveTaxID | updateRec == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    updateRec.Rate = progressiveTax.Rate;
                    updateRec.From = progressiveTax.From;
                    updateRec.To = progressiveTax.To;
                    updateRec.LastModifiedBy = "System";
                    updateRec.LastModifiedDate = DateTime.Now;

                    _context.Update(updateRec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgressiveTaxExists(progressiveTax.ProgressiveTaxID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(progressiveTax);
        }

        // GET: ProgressiveTaxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var progressiveTax = await _context.ProgressiveTaxes
                .FirstOrDefaultAsync(m => m.ProgressiveTaxID == id && m.IsDeleted == false);
            
            if (progressiveTax == null)
                return NotFound();

            return View(progressiveTax);
        }

        // POST: ProgressiveTaxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var progressiveTax = await _context.ProgressiveTaxes.FindAsync(id);

            //Soft Delete
            progressiveTax.IsDeleted = true;
            _context.ProgressiveTaxes.Update(progressiveTax);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgressiveTaxExists(int id)
        {
            return _context.ProgressiveTaxes.Any(e => e.ProgressiveTaxID == id && e.IsDeleted == false);
        }
    }
}
