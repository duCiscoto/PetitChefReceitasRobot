namespace PetitCHEFReceitasRapidas.Models
{
    class Receita
    {
        public string Titulo { get; set; }
        public string EnderecoReceita { get; set; }
        public string Avaliacao { get; set; }
        public string QuantidadeVotos { get; set; }
        public string QuantidadeComentarios { get; set; }
        public string QuantidadeCurtidas { get; set; }
        public string TipoReceita { get; set; }
        public string Dificuldade { get; set; }
        public string TempoPreparo { get; set; }
        public string Calorias { get; set; }
        public string Ingredientes { get; set; }
        public string Cozedura { get; set; }
        public string SemGlutem { get; set; }
    }
}
