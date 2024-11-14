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
    public class RegistroEstaciumController : Controller
    {
        private readonly EstacionamientoDbContext _context;

        public RegistroEstaciumController(EstacionamientoDbContext context)
        {
            _context = context;
        }

        // GET: RegistroEstacium
        public async Task<IActionResult> Index()
        {
            var estacionamientoDbContext = _context.RegistroEstancia.Include(r => r.Vehiculo);
            return View(await estacionamientoDbContext.ToListAsync());
        }

        // GET: RegistroEstacium/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroEstancium = await _context.RegistroEstancia
                .Include(r => r.Vehiculo)
                .FirstOrDefaultAsync(m => m.EstanciaId == id);
            if (registroEstancium == null)
            {
                return NotFound();
            }

            return View(registroEstancium);
        }

        // GET: RegistroEstacium/Create
        public IActionResult Create()
        {
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "Placa");
            var model = new RegistroEstancium
            {
                FechaEntrada = DateTime.Now // Fecha de hoy en el label
            };
            return View(model);
        }

        // POST: RegistroEstacium/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstanciaId,VehiculoId,FechaEntrada,FechaSalida")] RegistroEstancium registroEstancium)
        {
            registroEstancium.FechaEntrada = DateTime.Now;
            var vehiculo = await _context.Vehiculos.FindAsync(registroEstancium.VehiculoId); 
            bool vehiculoEnEstacionamiento = await _context.RegistroEstancia.AnyAsync(re => re.Vehiculo.Placa == vehiculo.Placa && re.FechaSalida == null);
            if (vehiculoEnEstacionamiento)
            {
                ModelState.AddModelError("VehiculoID", "Este vehículo sigue en el estacionamiento."); 
                ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", registroEstancium.VehiculoId); 
                return View(registroEstancium);
            }
            
            if (!ModelState.IsValid)
            {
                
                _context.Add(registroEstancium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", registroEstancium.VehiculoId);
            return View(registroEstancium);
        }

        // GET: RegistroEstacium/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroEstancium = await _context.RegistroEstancia.FindAsync(id);
            if (registroEstancium == null)
            {
                return NotFound();
            }
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "Placa", registroEstancium.VehiculoId);
            return View(registroEstancium);
        }

        // POST: RegistroEstacium/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstanciaId,VehiculoId,FechaEntrada,FechaSalida")] RegistroEstancium registroEstancium)
        {
            if (id != registroEstancium.EstanciaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroEstancium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroEstanciumExists(registroEstancium.EstanciaId))
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
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "VehiculoId", "VehiculoId", registroEstancium.VehiculoId);
            return View(registroEstancium);
        }

        // GET: RegistroEstacium/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroEstancium = await _context.RegistroEstancia
                .Include(r => r.Vehiculo)
                .FirstOrDefaultAsync(m => m.EstanciaId == id);
            if (registroEstancium == null)
            {
                return NotFound();
            }

            return View(registroEstancium);
        }

        // POST: RegistroEstacium/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroEstancium = await _context.RegistroEstancia.FindAsync(id);
            if (registroEstancium != null)
            {
                _context.RegistroEstancia.Remove(registroEstancium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ///check 
        public async Task<IActionResult> CheckOut(int id)
        {
            var registroEstancium = await _context.RegistroEstancia.Include(r => r.Vehiculo).ThenInclude(v => v.Tipo).FirstOrDefaultAsync(r => r.EstanciaId == id); //obtener tipo
            if (registroEstancium != null)
            {
                registroEstancium.FechaSalida = DateTime.Now;


                var tiempo = registroEstancium.FechaSalida.Value - registroEstancium.FechaEntrada; 
                var tarifaPorMinuto = registroEstancium.Vehiculo.Tipo.TarifaPorMinuto; 
                registroEstancium.Monto = CalcularCobro(tiempo, tarifaPorMinuto);

                //registroEstancium.Monto = CalcularCobro(tiempoEnParkin, Tarifa);

                _context.Update(registroEstancium);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private decimal CalcularCobro(TimeSpan tiempoEnParkin, decimal tarifaPorHora)
        {
            return (decimal)tiempoEnParkin.TotalMinutes * tarifaPorHora;
        }


        private bool RegistroEstanciumExists(int id)
        {
            return _context.RegistroEstancia.Any(e => e.EstanciaId == id);
        }
    }
}
