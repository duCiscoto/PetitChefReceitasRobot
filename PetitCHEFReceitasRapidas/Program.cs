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
                Receita receita = createReceitas(item);

                receitas.Add(receita);
            }

            return receitas;
        }

        private static string getAvaliacao(HtmlNode linha)
        {
            var conjunto = "";

            ////conjuntoSocial = Classificação/Avaliações, Votos e Curtidas;
            var conjuntoSocial = linha.SelectSingleNode("./div/i").GetAttributeValue("title", string.Empty);

            var match = Regex.Match(conjuntoSocial, @"(\d.\d+)");

            if (match.Success)
            {
                conjunto = match.Value;
            }

            return conjunto;
        }

        private static string getVotos(HtmlNode linha)
        {
            var conjunto = "";

            ////conjuntoSocial = Classificação/Avaliações, Votos e Curtidas;
            var conjuntoSocial = linha.SelectSingleNode("./div/i").GetAttributeValue("title", string.Empty);

            var match = Regex.Match(conjuntoSocial, @"\((\d+)");

            if (match.Success)
            {
                conjunto = match.Groups[1].Value;
                match.
            }

            return conjunto;
        }

        private static string getComentarios(HtmlNode linha)
        {
            var conjunto = "";

            ////conjuntoSocial = Classificação/Avaliações, Votos e Curtidas;
            var conjuntoSocial = linha.SelectSingleNode("./div/i[2]/following-sibling::text()").InnerText;

            var match = Regex.Match(conjuntoSocial, @"\d+");

            if (match.Success)
            {
                conjunto = match.Value;
            }

            return conjunto;
        }

        private static string getCurtidas(HtmlNode linha)
        {
            var conjunto = "";

            ////conjuntoSocial = Classificação/Avaliações, Votos e Curtidas;
            var conjuntoSocial = linha.SelectSingleNode("./div/i[3]/following-sibling::text()").InnerText;

            var match = Regex.Match(conjuntoSocial, @"\d+");

            if (match.Success)
            {
                conjunto = match.Value;
            }

            return conjunto;
        }

        private static string getGlutem(HtmlNode linha)
        {
            var conjunto = "Não informado";

            var informacao = linha.SelectSingleNode("/div[@class='i-right']/div/img/@title");

            if (informacao != null)
            {
                conjunto = informacao.InnerText;
            }

            return conjunto;
        }


        private static string getTitulo(HtmlNode linha)
        {
            var conjunto = "Não informado";

            var informacao = linha.SelectSingleNode("./h2/a").InnerText;

            if (informacao != null)
            {
                conjunto = informacao;
            }

            return conjunto;
        }

        private static string getEnderecoReceita(HtmlNode linha)
        {
            var conjunto = "Não informado";

            var informacao = linha.SelectSingleNode("./h2/a").GetAttributeValue("href", string.Empty);

            if (informacao != null)
            {
                conjunto = ("https://pt.petitchef.com" + informacao);
            }

            return conjunto;
        }
        
        private static string getTipoReceita(HtmlNode linha)
        {
            var conjunto = "Não informado";

            var informacao = linha.SelectSingleNode("./div[@class='prop']/span/i[@class='fa fa-utensils fa-fw']/following-sibling::text()").InnerText;

            if (informacao != null)
            {
                conjunto = informacao;
            }

            return conjunto;
        }

        private static string getDificuldade(HtmlNode linha)
        {
            var conjunto = "Não informado";

            var informacao = linha.SelectSingleNode("./div[@class='prop']/span/i[@class='fa fa-signal fa-fw']/following-sibling::text()").InnerText;

            if (informacao != null)
            {
                conjunto = informacao;
            }

            return conjunto;
        }

        private static string getTempoPreparo(HtmlNode linha)
        {
            var conjunto = "Não informado";

            var informacao = linha.SelectSingleNode("./div[@class='prop']/span/i[@class='fa fa-clock fa-fw']/following-sibling::text()").InnerText;

            if (informacao != null)
            {
                conjunto = informacao;
            }

            return conjunto;
        }

        private static string getCalorias(HtmlNode linha)
        {
            var conjunto = "Não informado";

            var informacao = linha.SelectSingleNode("./div[@class='prop']/span/i[@class='fa fa-balance-scale fa-fw']/following-sibling::text()").InnerText;

            if (informacao != null)
            {
                conjunto = informacao;
            }

            return conjunto;
        }

        private static string getIngredientes(HtmlNode linha)
        {
            var conjunto = "Não informado";

            var informacao = linha.SelectSingleNode("./div[@class='ingredients']/span/following-sibling::text()").InnerText;

            if (informacao != null)
            {
                conjunto = informacao;
            }

            return conjunto;
        }

        private static string getCozedura(HtmlNode linha)
        {
            var conjunto = "Não informado";

            var informacao = linha.SelectSingleNode("./div[@class='prop']/span/i[@class='fa fa-fire fa-fw']/following-sibling::text()");

            if (informacao != null)
            {
                conjunto = informacao.InnerText;
            }

            return conjunto;
        }

        private static Receita createReceitas(HtmlNode linha)
        {

            Receita receita = new Receita
            {
                titulo = getTitulo(linha),
                enderecoReceita = getEnderecoReceita(linha),
                avaliacao = getAvaliacao(linha),
                quantidadeVotos = getVotos(linha),
                quantidadeComentarios = getComentarios(linha),
                quantidadeCurtidas = getCurtidas(linha),
                tipoReceita = getTipoReceita(linha),
                dificuldade = getDificuldade(linha),
                tempoPreparo = getTempoPreparo(linha),
                calorias = getCalorias(linha),
                ingredientes = getIngredientes(linha),
                cozedura = getCozedura(linha),
                semGlutem = getGlutem(linha),
            };

            return receita;
        }
    }
}

