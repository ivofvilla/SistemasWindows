using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForms.Modelos
{
    public class Cliente
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string RG { get; set; }
        public DateTime DataExpedicao { get; set; }
        public string OrgaoExpedicao { get; set; }
        public string UF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public Endereco Endereco { get; set; }
    }
}