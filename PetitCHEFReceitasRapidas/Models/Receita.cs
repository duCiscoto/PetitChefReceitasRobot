using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetitCHEFReceitasRapidas.Models
{
    class Receita
    {
        public string titulo { get; set; }
        public string enderecoReceita { get; set; }
        public string avaliacao { get; set; }
        public string quantidadeVotos { get; set; }
        public string quantidadeComentarios { get; set; }
        public string quantidadeCurtidas { get; set; }
        public string tipoReceita { get; set; }
        public string dificuldade { get; set; }
        public string tempoPreparo { get; set; }
        public string calorias { get; set; }
        public string ingredientes { get; set; }
        public string cozedura { get; set; }
        public string semGlutem { get; set; }
    }
}
