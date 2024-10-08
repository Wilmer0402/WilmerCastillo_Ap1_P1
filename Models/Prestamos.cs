using System.ComponentModel.DataAnnotations;

namespace WilmerCastillo_Ap1_P1.Models
{
    public class Prestamos
    {

        [Key]

        public int PrestamosId { get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Nombre del Deudador")]

        public string Deudor {  get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Concepto")]

        public string Concepto { get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Monto del Prestamo")]

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public float Monto { get; set; }    



    }
}
