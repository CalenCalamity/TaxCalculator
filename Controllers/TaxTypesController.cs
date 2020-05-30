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
    public class TaxTypesController : Controller
    {
        private readonly TaxCalculatorContext _context;

        public TaxTypesController(TaxCalculatorContext context)
        {
            _context = context;
        }

        // GET: TaxTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaxTypes.Where(x => x.IsDeleted == false).ToListAsync());
        }

        // GET: TaxTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var taxType = await _context.TaxTypes
                .FirstOrDefaultAsync(m => m.TaxTypeID == id && m.IsDeleted == false);

            if (taxType == null)
                return NotFound();

            return View(taxType);
        }

        // GET: TaxTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaxTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaxTypeID,Description,Code")] TaxType taxType)
        {
            if (_context.TaxTypes.Where(x => x.Code == taxType.Code && x.IsDeleted == false) != null)
                return Forbid(); //Duplicate record

            if (ModelState.IsValid)
            {
                taxType.CreatedDate = taxType.LastModifiedDate = DateTime.Now;
                taxType.CreatedBy = taxType.LastModifiedBy = "System";
                taxType.IsDeleted = false;

                _context.Add(taxType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxType);
        }

        // GET: TaxTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var taxType = await _context.TaxTypes.Where(x => x.TaxTypeID == id && x.IsDeleted == false).FirstOrDefaultAsync();

            if (taxType == null)
                return NotFound();

            return View(taxType);
        }

        // POST: TaxTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaxTypeID,Description,Code")] TaxType taxType)
        {
            TaxType updateRec = _context.TaxTypes.Where(x => x.TaxTypeID == id && x.IsDeleted == false).FirstOrDefault();

            if (id != taxType.TaxTypeID | updateRec == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    updateRec.Description = taxType.Description;
                    updateRec.Code = taxType.Code;
                    updateRec.LastModifiedBy = "System"; //In place should user roles become a factor
                    updateRec.LastModifiedDate = DateTime.Now;

                    _context.Update(updateRec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxTypeExists(taxType.TaxTypeID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taxType);
        }

        // GET: TaxTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var taxType = await _context.TaxTypes
                .FirstOrDefaultAsync(m => m.TaxTypeID == id && m.IsDeleted == false);

            if (taxType == null)
                return NotFound();

            return View(taxType);
        }

        // POST: TaxTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taxType = await _context.TaxTypes.Where(x => x.TaxTypeID == id && x.IsDeleted == false).FirstOrDefaultAsync();

            //Soft Delete
            taxType.IsDeleted = true;
            _context.TaxTypes.Update(taxType);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxTypeExists(int id)
        {
            return _context.TaxTypes.Any(e => e.TaxTypeID == id && e.IsDeleted == false);
        }
    }
}
