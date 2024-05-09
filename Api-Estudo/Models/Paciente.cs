using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api_Estudo.Models
{
    public class Paciente
    {

        public int Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
       
        [Required]
        public DateTime DataNascimento { get; set; }


     
    }
}