using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WilmerCastillo_Ap1_P1.Models
{
    public class CobrosDetalles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int DetallesId { get; set; }

        public int CobrosId { get; set; }

        [ForeignKey("Prestamos")]
        public int PrestamosId { get; set; }

        public Prestamos? Prestamos { get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Valor Cobrado")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se Permiten Numeros")]
        public double ValorCobrado { get; set; }
    }
}
