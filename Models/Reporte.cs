using System;
using System.Collections.Generic;

namespace GestionEstacionamientoRicardo.Models
{
    public class Reporte
    {
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public List<ReporteDetalle> Detalles { get; set; } = new List<ReporteDetalle>();
    }
    public class ReporteDetalle 
    { 
        public string NumeroPlaca { get; set; } 
        public string TiempoEstacionado { get; set; } 
        public string Tipo { get; set; } 
        public decimal Monto { get; set; } 
    }
}
