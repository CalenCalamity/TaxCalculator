using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TaxCalculator.Data;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    public class PostalCodesController : Controller
    {
        private readonly TaxCalculatorContext _context;

        public PostalCodesController(TaxCalculatorContext context)
        {
            _context = context;
        }

        // GET: PostalCodes
        public async Task<IActionResult> Index()
        {
            ViewBag.TaxTypes = _context.TaxTypes.Where(x => x.IsDeleted == false);
            return View(await _context.PostalCodes.Where(x => x.IsDeleted == false).ToListAsync());
        }

        // GET: PostalCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var postalCode = await _context.PostalCodes
                .FirstOrDefaultAsync(m => m.PostalCodeID == id && m.IsDeleted == false);

            if (postalCode == null)
                return NotFound();

            return View(postalCode);
        }

        // GET: PostalCodes/Create
        public IActionResult Create()
        {
            ViewBag.TaxTypeList = (IEnumerable<TaxType>)_context.TaxTypes;

            return View();
        }

        // POST: PostalCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostalCodeID,Value,TaxTypeID")] PostalCode postalCode)
        {
            if (_context.PostalCodes.Where(x => x.Value == postalCode.Value && x.IsDeleted == false) != null)
                return Forbid(); //Duplicate record

            if (ModelState.IsValid)
            {
                postalCode.TaxTypeID = _context.TaxTypes.Where(x => x.TaxTypeID == int.Parse(ModelState["TaxTypeID"].RawValue.ToString())).FirstOrDefault().TaxTypeID;
                postalCode.CreatedDate = postalCode.LastModifiedDate = DateTime.Now;
                postalCode.CreatedBy = postalCode.LastModifiedBy = "System";
                postalCode.IsDeleted = false;

                _context.Add(postalCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postalCode);
        }

        // GET: PostalCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var postalCode = await _context.PostalCodes.Where(x => x.PostalCodeID == id && x.IsDeleted == false).FirstOrDefaultAsync();

            if (postalCode == null)
                return NotFound();

            ViewBag.TaxTypeList = (IEnumerable<TaxType>)_context.TaxTypes;

            return View(postalCode);
        }

        // POST: PostalCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostalCodeID,Value,TaxTypeID")] PostalCode postalCode)
        {
            PostalCode updateRec = _context.PostalCodes.Where(x => x.PostalCodeID == postalCode.PostalCodeID && x.IsDeleted == false).FirstOrDefault();

            if (id != postalCode.PostalCodeID | updateRec == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    updateRec.Value = postalCode.Value;
                    updateRec.TaxTypeID = _context.TaxTypes.Where(x => x.TaxTypeID == int.Parse(ModelState["TaxTypeID"].RawValue.ToString())).FirstOrDefault().TaxTypeID;
                    updateRec.LastModifiedBy = "System";
                    updateRec.LastModifiedDate = DateTime.Now;

                    _context.Update(updateRec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostalCodeExists(postalCode.PostalCodeID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postalCode);
        }

        // GET: PostalCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var postalCode = await _context.PostalCodes
                .FirstOrDefaultAsync(m => m.PostalCodeID == id && m.IsDeleted == false);

            if (postalCode == null)
                return NotFound();

            return View(postalCode);
        }

        // POST: PostalCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postalCode = await _context.PostalCodes.FindAsync(id);

            //Soft Delete
            postalCode.IsDeleted = true;
            _context.PostalCodes.Update(postalCode);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostalCodeExists(int id)
        {
            return _context.PostalCodes.Any(e => e.PostalCodeID == id && e.IsDeleted == false);
        }
    }
}
