using System.ComponentModel.DataAnnotations;

namespace WilmerCastillo_Ap1_P1.Models
{
    public class Prestamos
    {

        [Key]

        public int prestamosId { get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Nombre del Deudador")]

        public string Deudador {  get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Concepto")]

        public string Concepto { get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Monto del Prestamo")]

        public float Monto { get; set; }    



    }
}
