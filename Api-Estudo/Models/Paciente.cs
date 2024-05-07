using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Estudo.Models
{
    public class Paciente
    {

        public int Codigo { get; set; }

        public string Nome { get; set; }

        public DateTime? DataNascimento { get; set; }


    }
}