using System.ComponentModel.DataAnnotations;

namespace GestionEstacionamientoRicardo.Models;

public partial class TiposVehiculo
{
    public int TipoId { get; set; }

    [Required(ErrorMessage = "Es obligatorio la descripcion del tipo")]
    [StringLength(10, ErrorMessage = "la descripcion no debe ser de mas de 10 letras")]
    public string Descripcion { get; set; } = null!;

    [Required(ErrorMessage = "La tarifa por minuto es obligatoria")]
    [Range(0.00, 99.99, ErrorMessage = "La tarifa por minuto debe ser un valor decimal entre 0.00 y 99.99")]
    [DataType(DataType.Currency)]
    public decimal TarifaPorMinuto { get; set; }

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
