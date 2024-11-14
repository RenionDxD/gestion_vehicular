using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GestionEstacionamientoRicardo.Models;

public partial class Vehiculo
{
    public int VehiculoId { get; set; }


    [Required]
    [StringLength(9, ErrorMessage = "El número de placa no puede exceder los 9 caracteres")]
    public string Placa { get; set; } = null!;

    public int TipoId { get; set; }

    public virtual ICollection<RegistroEstancium> RegistroEstancia { get; set; } = new List<RegistroEstancium>();

    public virtual TiposVehiculo Tipo { get; set; } = null!;
}
