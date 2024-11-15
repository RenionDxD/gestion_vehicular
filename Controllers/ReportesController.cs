using GestionEstacionamientoRicardo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;


// La verdad este si lo hice con copilot y mis practicas den DAD Software

namespace GestionEstacionamientoRicardo.Controllers
{
    public class ReportesController : Controller
    {
        private readonly EstacionamientoDbContext _context; 
        public ReportesController(EstacionamientoDbContext context) { _context = context; }
        [HttpGet] public IActionResult Reporte() 
        {
            var model = new Reporte
            {
                Fecha = DateTime.Today,
                HoraInicio = DateTime.Now.TimeOfDay,
                HoraFin = DateTime.Now.TimeOfDay
            };
            return View(model);
        }
        //[HttpPost] public async Task<IActionResult>
        //GenerarReporte(Reporte model) 
        //{ 
        //    if (ModelState.IsValid) 
        //    { 
        //        var inicio = model.Fecha.Date + model.HoraInicio; 
        //        var fin = model.Fecha.Date + model.HoraFin; 
        //        var detalles = await _context.RegistroEstancia.Include(r => r.Vehiculo).ThenInclude(v => v.Tipo).Where(r => r.FechaEntrada >= inicio && r.FechaSalida <= fin).Select(r => new ReporteDetalle { NumeroPlaca = r.Vehiculo.Placa, TiempoEstacionado = (r.FechaSalida - r.FechaEntrada).Value.ToString(@"hh\:mm"), Tipo = r.Vehiculo.Tipo.Descripcion, Monto = r.Monto ?? 0m}).ToListAsync(); model.Detalles = detalles; 
        //    } 
        //    return View("Reporte", model); 
        //}
        [HttpPost]
        public async Task<IActionResult> GenerarReporte(Reporte model)
        {
            if (ModelState.IsValid)
            {
                var inicio = model.Fecha.Date + model.HoraInicio;
                var fin = model.Fecha.Date + model.HoraFin;

                var detalles = await _context.RegistroEstancia
                    .Include(r => r.Vehiculo)
                        .ThenInclude(v => v.Tipo)
                    .Where(r => r.FechaEntrada >= inicio && r.FechaSalida <= fin)
                    .Select(r => new ReporteDetalle
                    {
                        NumeroPlaca = r.Vehiculo.Placa,
                        TiempoEstacionado = (r.FechaSalida - r.FechaEntrada).Value.ToString(@"hh\:mm"),
                        Tipo = r.Vehiculo.Tipo.Descripcion,
                        Monto = r.Monto ?? 0m
                    })
                    .ToListAsync();

                model.Detalles = detalles;
            }

            return View("Reporte", model);
        }

        public IActionResult ExportarExcel(Reporte model)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte");

                // Encabezados de las columnas
                worksheet.Cell(1, 1).Value = "Núm. Placa";
                worksheet.Cell(1, 2).Value = "Tiempo Estacionado";
                worksheet.Cell(1, 3).Value = "Tipo";
                worksheet.Cell(1, 4).Value = "Cantidad a Pagar";

                // Llenar datos de los detalles en las celdas
                for (int i = 0; i < model.Detalles.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = model.Detalles[i].NumeroPlaca;
                    worksheet.Cell(i + 2, 2).Value = model.Detalles[i].TiempoEstacionado;
                    worksheet.Cell(i + 2, 3).Value = model.Detalles[i].Tipo;
                    worksheet.Cell(i + 2, 4).Value = model.Detalles[i].Monto;
                }

                // Exportar el archivo en formato Excel
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "reporte.xlsx");
                }
            }
        }

    }
}
