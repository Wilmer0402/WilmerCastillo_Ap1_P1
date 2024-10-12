using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WilmerCastillo_Ap1_P1.Models
{
    public class Cobros
    {

        [Key]

        public int CobrosId { get; set; }

        [Required(ErrorMessage = "Favor Ingresar la Fecha")]

        public DateTime Fecha {  get; set; }

        [ForeignKey("Deudores")]

        public int DeudorId { get; set; }

        public Deudores? Deudores { get; set; }

        [ForeignKey("CobroId")]

        [Required(ErrorMessage = "Favor Ingresar el Valor Cabrado")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se Permiten Numeros")]

        public double Monto { get; set; }

 
        public ICollection<CobrosDetalles> CobrosDetalles { get; set; } = new List<CobrosDetalles>();

    }
}
