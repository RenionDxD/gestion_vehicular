using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionEstacionamientoRicardo.Models;

public partial class TiposVehiculo
{
    public int TipoId { get; set; }

    [Required(ErrorMessage = "La descripcion es obligatorio")]
    [StringLength(15, ErrorMessage = "El número de descripcion no puede exceder los 15 caracteres")]
    public string Descripcion { get; set; } = null!;

    [Required(ErrorMessage = "La tarifa por minuto es obligatoria")]
    [Range(0.00, 99.99, ErrorMessage = "La tarifa por minuto debe ser un valor decimal entre 0.00 y 99.99")]
    [DataType(DataType.Currency)]
    public decimal TarifaPorMinuto { get; set; }

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
