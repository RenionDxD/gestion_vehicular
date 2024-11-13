using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionEstacionamientoRicardo.Models;

namespace GestionEstacionamientoRicardo.Controllers
{
    public class TiposVehiculosController : Controller
    {
        private readonly EstacionamientoDbContext _context;

        public TiposVehiculosController(EstacionamientoDbContext context)
        {
            _context = context;
        }

        // GET: TiposVehiculos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposVehiculos.ToListAsync());
        }

        // GET: TiposVehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposVehiculo = await _context.TiposVehiculos
                .FirstOrDefaultAsync(m => m.TipoId == id);
            if (tiposVehiculo == null)
            {
                return NotFound();
            }

            return View(tiposVehiculo);
        }

        // GET: TiposVehiculos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposVehiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoId,Descripcion,TarifaPorMinuto")] TiposVehiculo tiposVehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposVehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposVehiculo);
        }

        // GET: TiposVehiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposVehiculo = await _context.TiposVehiculos.FindAsync(id);
            if (tiposVehiculo == null)
            {
                return NotFound();
            }
            return View(tiposVehiculo);
        }

        // POST: TiposVehiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoId,Descripcion,TarifaPorMinuto")] TiposVehiculo tiposVehiculo)
        {
            if (id != tiposVehiculo.TipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposVehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposVehiculoExists(tiposVehiculo.TipoId))
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
            return View(tiposVehiculo);
        }

        // GET: TiposVehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposVehiculo = await _context.TiposVehiculos
                .FirstOrDefaultAsync(m => m.TipoId == id);
            if (tiposVehiculo == null)
            {
                return NotFound();
            }

            return View(tiposVehiculo);
        }

        // POST: TiposVehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var tiposVehiculo = await _context.TiposVehiculos.FindAsync(id);
            var vehiculos = await _context.Vehiculos.Where(v => v.TipoId == id).ToListAsync();
            var tipoVehiculo = await _context.TiposVehiculos.FindAsync(id);
            if (vehiculos != null)
            {
                _context.TiposVehiculos.Remove(tipoVehiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposVehiculoExists(int id)
        {
            return _context.TiposVehiculos.Any(e => e.TipoId == id);
        }
    }
}
