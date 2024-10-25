using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WilmerCastillo_Ap1_P1.Models
{
    public class Prestamos
    {

        [Key]

        public int PrestamosId { get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Concepto del Prestamo")]

        public string Concepto { get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Monto del Prestamo")]

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public float Monto { get; set; }

        [Required(ErrorMessage = "Por favor ingresar el balance")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Solo se permiten números enteros o decimales")]
        public double Balance { get; set; }

        [ForeignKey("DeudorId")]

        public int DeudorId { get; set; }   

        public Deudores? Deudor { get; set; }



    }
}
