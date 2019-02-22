using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PetitCHEFReceitasRapidas.Models;

namespace PetitCHEFReceitasRapidas
{
    class Program
    {
        static public HtmlNode _doc { get; set; }
        static public HtmlWeb _web = new HtmlWeb();
        static public List<Receita> ColecaoReceitas = new List<Receita>();

        static void Main(string[] args)
        {
            NavegaPagina("https://pt.petitchef.com/receitas/rapida");
            getReceitas(_doc);
        }

        /// <summary>
        /// Função para navegar na página a partir de uma URL
        /// </summary>
        /// <param name="url">URL desejada</param>
        private static void NavegaPagina(string url)
        {
            _doc = _web.Load(url).DocumentNode;
        }

        /// <summary>
        /// Função para capturar todos os docNodes de todas as páginas de receitas (paginação)
        /// </summary>
        /// <param name="docNode">docNodes</param>
        private static void getReceitas(HtmlNode docNode)
        {
            var receitasPagina = parseReceitas(docNode);

            ColecaoReceitas.AddRange(receitasPagina);

            var paginacao = docNode.SelectSingleNode("//div[@class='pages']/span/following-sibling::a[1]");

            if (paginacao != null)
            {
                NavegaPagina("https://pt.petitchef.com" + paginacao.GetAttributeValue("href", string.Empty));

                getReceitas(_doc);
            }
        }

        /// <summary>
        /// Função para 
        /// </summary>
        /// <param name="docNode"></param>
        /// <returns></returns>
        private static List<Receita> parseReceitas(HtmlNode docNode)
        {
            var linhas = docNode.SelectNodes("//div[@class='recipe-list']/div/div[@class='i-right']");

            List<Receita> receitas = new List<Receita>();

            foreach (var item in linhas)
            {
                Receita receita = createReceitas(linhas);

                receita.Add(receita);
            }

            return receitas;
        }

        private static Receita createReceitas(HtmlNode linha)
        {
            Receita receita = new Receita
            {
                titulo = linha.SelectSingleNode("./div/i[@class='note-fa']").GetAttributeValue("title", string.Empty),
                enderecoReceita = linha.SelectSingleNode("./h2/a").GetAttributeValue("href", string.Empty),
                avaliacao = /*fazer regex*/,
                quantidadeVotos = /*fazer regex*/,
                quantidadeComentarios ,
                quantidadeCurtidas /*fazer regex*/,
                tipoRecita ,
                dificuldade ,
                tempoPreparo ,
                calorias ,
                ingredientes linha.SelectSingleNode("./div[@class='ingredients']/span/following-sibling::text()").InnerText,
                cozedura ,
                public bool semGlutem { get; set; }
            };

    //title="(4.48)/5 ((85) votos)"

    //var match = Regex.Match("entrada", @"regex");

    string static getAvaliacoes() { }

    var match = Regex.Match("entrada", @"regex");

    var match = Regex.Match("entrada", @"");

            if (match.Success)
            {
                match.Value;
                    match.Groups[1].Value;
            }

var avaliacao

            var preQuantidadeVotos { get; set; }

var quantidadeComentarios { get; set; }

var quantidadeCurtidas { get; set; }

var tipoRecita { get; set; }

var dificuldade { get; set; }

var tempoPreparo { get; set; }

var calorias { get; set; }

var ingredientes { get; set; }

var cozedura { get; set; }

var semGlutem { get; set; }
    }
}
}
