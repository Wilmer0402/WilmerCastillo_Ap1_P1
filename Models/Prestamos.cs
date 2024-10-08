﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WilmerCastillo_Ap1_P1.Models
{
    public class Prestamos
    {

        [Key]

        public int PrestamosId { get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Nombre del Deudor")]

        public string Deudor {  get; set; } 


        [Required(ErrorMessage = "Favor Ingresar el Concepto")]

        public string Concepto { get; set; }

        [Required(ErrorMessage = "Favor Ingresar el Monto del Prestamo")]

        public float Monto { get; set; }


        


    }
}
