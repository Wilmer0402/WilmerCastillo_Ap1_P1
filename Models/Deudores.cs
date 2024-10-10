using System.ComponentModel.DataAnnotations;


namespace WilmerCastillo_Ap1_P1.Models
{
    public class Deudores
    {

      [Key]

        public int DeudorId { get; set; }   

        [Required(ErrorMessage ="Este Campo es Obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Solo se Puede Ingresar Letras")]

        public string? Nombres { get; set; }    


    }
}
