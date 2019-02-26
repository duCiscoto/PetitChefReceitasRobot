namespace PetitCHEFReceitasRapidas.Models
{
    class Receita
    {
        public string Titulo { get; set; }
        public string EnderecoReceita { get; set; }
        public string Avaliacao { get; set; }
        public string QuantidadeVotos { get; set; }
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
