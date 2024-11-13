using System;
using System.Collections.Generic;

namespace GestionEstacionamientoRicardo.Models;

public partial class RegistroEstancium
{
    public int EstanciaId { get; set; }

    public int VehiculoId { get; set; }


    public DateTime FechaEntrada { get; set; }

    public DateTime? FechaSalida { get; set; }

    public decimal? Monto { get; set; }

    public virtual Vehiculo Vehiculo { get; set; } = null!;
    
}
