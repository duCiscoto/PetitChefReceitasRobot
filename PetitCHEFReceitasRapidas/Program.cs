using HtmlAgilityPack;
using PetitCHEFReceitasRapidas.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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


            //// Imprime os falores capturados
            foreach (var item in ColecaoReceitas)
            {
                Console.WriteLine(ColecaoReceitas.IndexOf(item) + " -  " + item.Titulo + " / " + item.TipoReceita + " / " + item.EnderecoReceita);
            }
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
                if (item.SelectSingleNode("./div[@class='ingredients']/span/text()") != null)
                {
                    Receita receita = createReceitas(item);

                    receitas.Add(receita);
                }
            }

            return receitas;
        }

        private static string getAvaliacao(HtmlNode linha)
        {
            var conjunto = "Ainda não há avaliações";

            ////conjuntoSocial = Classificação/Avaliações, Votos e Curtidas;
            var conjuntoSocial = linha.SelectSingleNode("./div/i");

            if (conjuntoSocial != null)
            {
                var match = Regex.Match(conjuntoSocial.GetAttributeValue("title", string.Empty), @"(\d.\d+)");

                if (match.Success)
                    conjunto = match.Value;
            }

            return conjunto;
        }

        private static string getVotos(HtmlNode linha)
        {
            var conjunto = "Ainda não recebeu votos";

            ////conjuntoSocial = Classificação/Avaliações, Votos e Curtidas;
            var conjuntoSocial = linha.SelectSingleNode("./div/i");

            if (conjuntoSocial != null)
            {
                var match = Regex.Match(conjuntoSocial.GetAttributeValue("title", string.Empty), @"\((\d+)");

                if (match.Success)
                    conjunto = match.Groups[1].Value;
            }

            return conjunto;
        }

        private static string getComentarios(HtmlNode linha)
        {
            var conjunto = "Ainda não recebeu comentários";

            ////conjuntoSocial = Classificação/Avaliações, Votos e Curtidas;
            var conjuntoSocial = linha.SelectSingleNode("./div/i[2]/following-sibling::text()");

            if (conjuntoSocial != null)
            {
                var match = Regex.Match(conjuntoSocial.InnerText, @"\d+");

                if (match.Success)

                    conjunto = match.Value;
            }

            return conjunto;
        }

        private static string getCurtidas(HtmlNode linha)
        {
            var conjunto = "Ainda não recebeu curtidas";

            ////conjuntoSocial = Classificação/Avaliações, Votos e Curtidas;
            var conjuntoSocial = linha.SelectSingleNode("./div/i[3]/following-sibling::text()");

            if (conjuntoSocial != null)
            {
                var match = Regex.Match(conjuntoSocial.InnerText, @"\d+");

                if (conjuntoSocial != null && match.Success)
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

            var informacao = linha.SelectSingleNode("./div[@class='prop']/span/i[@class='fa fa-balance-scale fa-fw']/following-sibling::text()");

            if (informacao != null)
            {
                conjunto = informacao.InnerText;
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
                Titulo = getTitulo(linha),
                EnderecoReceita = getEnderecoReceita(linha),
                Avaliacao = getAvaliacao(linha),
                QuantidadeVotos = getVotos(linha),
                QuantidadeComentarios = getComentarios(linha),
                QuantidadeCurtidas = getCurtidas(linha),
                TipoReceita = getTipoReceita(linha),
                Dificuldade = getDificuldade(linha),
                TempoPreparo = getTempoPreparo(linha),
                Calorias = getCalorias(linha),
                Ingredientes = getIngredientes(linha),
                Cozedura = getCozedura(linha),
                SemGlutem = getGlutem(linha),
            };

            return receita;
        }
    }
}

