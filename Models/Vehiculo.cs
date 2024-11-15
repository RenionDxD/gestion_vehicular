using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionEstacionamientoRicardo.Models;

public partial class Vehiculo
{
    public int VehiculoId { get; set; }


    [Required(ErrorMessage = "El número de placa es obligatorio")]
    [StringLength(10, ErrorMessage = "El número de placa no puede exceder los 10 caracteres")]
    public string Placa { get; set; } = null!;

    public int TipoId { get; set; }

    public virtual ICollection<RegistroEstancium> RegistroEstancia { get; set; } = new List<RegistroEstancium>();

    public virtual TiposVehiculo Tipo { get; set; } = null!;
}
