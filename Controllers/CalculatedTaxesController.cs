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
    public class CalculatedTaxesController : Controller
    {
        private readonly TaxCalculatorContext _context;

        public CalculatedTaxesController(TaxCalculatorContext context)
        {
            _context = context;
        }

        // GET: CalculatedTaxes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CalculatedTaxes.ToListAsync());
        }

        // GET: CalculatedTaxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calculatedTax = await _context.CalculatedTaxes
                .FirstOrDefaultAsync(m => m.CalculatedTaxID == id);
            if (calculatedTax == null)
            {
                return NotFound();
            }

            return View(calculatedTax);
        }

        // GET: CalculatedTaxes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalculatedTaxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalculatedTaxID,Value,CreatedDate,LastModifiedDate,CreatedBy,LastModifiedBy")] CalculatedTax calculatedTax)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calculatedTax);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calculatedTax);
        }

        // GET: CalculatedTaxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calculatedTax = await _context.CalculatedTaxes.FindAsync(id);
            if (calculatedTax == null)
            {
                return NotFound();
            }
            return View(calculatedTax);
        }

        // POST: CalculatedTaxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalculatedTaxID,Value,CreatedDate,LastModifiedDate,CreatedBy,LastModifiedBy")] CalculatedTax calculatedTax)
        {
            if (id != calculatedTax.CalculatedTaxID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calculatedTax);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalculatedTaxExists(calculatedTax.CalculatedTaxID))
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
            return View(calculatedTax);
        }

        // GET: CalculatedTaxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calculatedTax = await _context.CalculatedTaxes
                .FirstOrDefaultAsync(m => m.CalculatedTaxID == id);
            if (calculatedTax == null)
            {
                return NotFound();
            }

            return View(calculatedTax);
        }

        // POST: CalculatedTaxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calculatedTax = await _context.CalculatedTaxes.FindAsync(id);
            _context.CalculatedTaxes.Remove(calculatedTax);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalculatedTaxExists(int id)
        {
            return _context.CalculatedTaxes.Any(e => e.CalculatedTaxID == id);
        }
    }
}
