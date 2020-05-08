using FateDB.Database;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace FateDB
{

    public static class ThroneOfHeroes
    {

        
        private static List<Servant> _all_servants = new List<Servant>();
        /// <summary>
        /// Todos os default servants existentes
        /// </summary>
        public static List<Servant> All_servants => _all_servants;



        /// <summary>
        /// Funcao que devolve um aligment do tipo Aligment
        /// </summary>
        /// <param name="Linha de texto em string"></param>
        /// <returns></returns>
        private static Alignment Find_alig(string txt)
        {
            string[] allcaps = txt.ToUpper().Split();
            string[] all = Enum.GetNames(typeof(Alignment));
            Alignment result = Alignment.BALANCED;
            foreach (string este in all)
            {
                foreach (string teste in allcaps)
                    if (teste == este)
                    {
                        Enum.TryParse(este, out result);
                        return result;
                    }
            }
            return result;

        }

        /// <summary>
        /// Funcao que devolde a segunda parte do aligment do servant
        /// </summary>
        /// <param name="Linha de texto em string"></param>
        /// <returns></returns>
        private static Aligment2 Find_alig2(string txt)
        {
            string[] allcaps = txt.ToUpper().Split();
            string[] all = Enum.GetNames(typeof(Aligment2));
            Aligment2 result = Aligment2.NEUTRAL;
            foreach (string este in all)
            {
                foreach (string teste in allcaps)
                    if (teste == este)
                    {
                        Enum.TryParse(este, out result);
                        return result;
                    }
            }


            return result;
        }

        /// <summary>
        /// Funcao que devolve a classe da personagem em Servant_Class
        /// </summary>
        /// <param name="txt"></param>
        /// <seealso cref="Servant_Class"/>
        /// <returns></returns>
        private static Servant_Class Find_class(string txt)
        {
            string allcaps = txt.ToUpper();
            string[] all = Servant_Class.GetNames(typeof(Servant_Class));
            Servant_Class result = Servant_Class.ERROR;
            foreach (string este in all)
            {
                if (allcaps.Contains(este))
                {
                    Enum.TryParse<Servant_Class>(este, out result);
                    return result;
                }
            }
            return result;

        }

        /// <summary>
        /// Funcao que devolve o tipo de Noble Phantasm da personagem em NPType
        /// </summary>
        /// <param name="txt"></param>
        /// <seealso cref="NPType"/>
        /// <returns></returns>
        private static NPType Getnptype(string txt)
        {
            NPType result = NPType.ANTI_PERSONEL;
            string allcaps = txt.ToUpper();

            string[] all = Enum.GetNames(typeof(NPType));
            foreach (string este in all)
            {
                if (allcaps.Contains(este.Replace("_", "-")))
                {
                    Enum.TryParse<NPType>(este, out result);
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Funcao que devolve o Rank da habilidade da personagem em SkillRank
        /// </summary>
        /// <param name="txt"></param>
        /// <seealso cref="SkillRank"/>
        /// <returns></returns>
        private static SkillRank Getnprank(string txt)
        {
            SkillRank result = SkillRank.B;
            string allcaps = txt.ToUpper();

            string[] all = SkillRank.GetNames(typeof(SkillRank));
            foreach (string este in all)
            {
                if (allcaps.Replace("RANK", "").Contains(este))
                {
                    Enum.TryParse<SkillRank>(este, out result);
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Devolve uma classe trait para uma personagem
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="trait"></param>
        /// <returns></returns>
        private static Trait GetTrait(string txt, string trait)
        {
            Enum.TryParse<TraitDesc>(trait, out TraitDesc desc);
            Enum.TryParse<SkillRank>(txt.Replace(trait, ""), out SkillRank skillres);
            Trait res = new Trait(desc, skillres);

            return res;
        }

        /// <summary>
        /// Devolve uma classe trait para uma personagem
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private static Trait GetTrait(string txt)
        {
            SkillRank skillres = SkillRank.B;
            TraitDesc traitres = TraitDesc.MADNESS_ENHANCMENT;

            string[] all = Enum.GetNames(typeof(TraitDesc));
            foreach (string este in all)
            {
                if (txt.ToUpper().Contains(este))
                {
                    Enum.TryParse<TraitDesc>(este, out traitres);
                }
            }

            all = Enum.GetNames(typeof(SkillRank));
            foreach (string este in all)
            {
                if (txt.ToUpper().Replace(traitres.ToString(), "").Contains(este))
                {
                    Enum.TryParse<SkillRank>(este, out skillres);
                }
            }




            Trait res = new Trait(traitres, skillres);
            return res;
        }

        /// <summary>
        /// <para>Funcao que atualiza os servants default</para>
        /// <para>Esta funcao demora visto que vai a internet buscar dados das personagens e pode dar HTTP error 500 em dias que estejam a atualizar a pagina</para>
        /// </summary>
        public static void UpdateList()
        {
            WebClient webClient = new WebClient();
            webClient.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)";
            string page = webClient.DownloadString("https://grandorder.wiki/Servant_List");
            string page2 = webClient.DownloadString("https://grandorder.wiki/Servants_by_Profile");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            HtmlDocument doc2 = new HtmlDocument();
            doc2.LoadHtml(page2);
            doc2.OptionEmptyCollection = true;

            List<HtmlNode> ServantStats = doc.DocumentNode.SelectSingleNode("//table[last()]").Descendants("tr").Skip(1).ToList();
            List<HtmlNode> ServantProfiles = doc2.DocumentNode.SelectSingleNode("//table").Descendants("tr").Skip(1).ToList();

            foreach (var row in ServantStats)
            {

                HtmlDocument servanthtml = new HtmlDocument();
                string serv_page = webClient.DownloadString("https://grandorder.wiki" + row.SelectSingleNode("td[3]")
                    .SelectSingleNode("a")
                    .GetAttributeValue("href", "rip"));

                servanthtml.LoadHtml(serv_page);

                int id = int.Parse(row.SelectSingleNode("td[1]").InnerText.ToString());

                string name = row.SelectSingleNode("td[3]")
                    .SelectSingleNode("a")
                    .GetAttributeValue("title", "rip");

                Servant_Class cl = Find_class(row.SelectSingleNode("td[4]")
                    .SelectSingleNode("div")
                    .SelectSingleNode("div")
                    .SelectSingleNode("img")
                    .GetAttributeValue("alt", "unknown"));

                int rarity = getnum(row.SelectSingleNode("td[5]").InnerText.ToString());
                int min_atk = int.Parse((row.SelectSingleNode("td[6]").InnerText.ToString()));
                int max_atk = int.Parse((row.SelectSingleNode("td[7]").InnerText.ToString()));
                int min_hp = int.Parse((row.SelectSingleNode("td[8]").InnerText.ToString()));
                int max_hp = int.Parse((row.SelectSingleNode("td[9]").InnerText.ToString()));
                SkillRank rank = SkillRank.B;

                List<Trait> alltraits = new List<Trait>();
                NPType tipo = NPType.ANTI_PERSONEL;
                string Npname = "???????????";
                bool found = false;
                if (name != "Solomon")
                {
                    // foreach (HtmlNode este in servanthtml.DocumentNode.SelectNodes("//div[contains(@title, 'Rank') or contains(@title, 'NP') or contains(@title, 'EX') or contains(@title, 'Lord Camelot')]").ToList())
                    foreach (HtmlNode este in servanthtml.DocumentNode.SelectNodes("//div").ToList())
                    {
                        if (!found)
                        {

                            if (este.GetAttributeValue("title", "nope").Split().First() == "Rank" ||
                                este.GetAttributeValue("title", "nope").Split().First() == "NP" ||
                                este.GetAttributeValue("title", "nope").Split().First() == "EX" ||
                                este.GetAttributeValue("title", "nope") == "Lord Camelot")
                            {
                                Console.WriteLine("ok");

                                if (este
                               .Descendants("tr")
                               .First()
                               .Descendants("div")
                               .First()
                               .Descendants("b").Count()
                               != 0)
                                {
                                    Npname = este
                                   .Descendants("tr")
                                   .First()
                                   .Descendants("div")
                                   .First()
                                   .Descendants("b")
                                   .First()
                                   .InnerHtml
                                   .Split('<')
                                   .First();
                                }
                                else
                                {
                                    Npname = este
                                  .Descendants("tr")
                                  .First()
                                  .Descendants("div")
                                  .First()
                                  .InnerHtml;
                                }

                                if (Npname == "")
                                {
                                    Npname = este.Descendants("tr").First().Descendants("div").First().Descendants("b").First().InnerHtml.Split('>').Skip(1).First();
                                    if (Npname == "<br")
                                    {
                                        Npname = "????";
                                    }
                                }

                                tipo = Getnptype(este
                                .Descendants("p")
                                .Skip(1)
                                .First()
                                .InnerText);

                                rank = Getnprank(este
                                .GetAttributeValue("title", "unknown"));
                                found = true;

                            }
                            else
                            {
                                if (este.GetAttributeValue("title", "nope") != "nope")
                                {
                                    foreach (string tr in Enum.GetNames(typeof(TraitDesc)).ToList())
                                    {

                                        if (este.GetAttributeValue("title", "nope").ToUpper().Contains(tr.Replace("_", " ")))
                                        {

                                            alltraits.Add(GetTrait(este.GetAttributeValue("title", "nope"), tr));
                                        }
                                    }
                                }
                            }
                        }
                    }

                }





                HtmlNode profile = null;
                string[] tentativa2 = name.Split();
                if (!File.Exists(ServantContainer.path + "/" + id + ".png"))
                {
                    try
                    {
                        webClient.DownloadFile("https://grandorder.wiki" + row.SelectSingleNode("td[2]")
                            .SelectSingleNode("div")
                            .SelectSingleNode("div")
                            .Descendants("img")
                            .Single()
                            .GetAttributeValue("src", "unknown"), ServantContainer.path + "/" + id + ".png");
                    }
                    catch
                    {
                        Console.WriteLine("No image found");
                    }
                }
                foreach (HtmlNode roww in ServantProfiles)
                {
                    if (roww.SelectSingleNode("td[1]").SelectSingleNode("a").GetAttributeValue("title", "rip") == name)
                    {
                        profile = roww;
                        break;
                    }




                }

                if (profile == null)
                {
                    foreach (HtmlNode roww in ServantProfiles)
                    {
                        string[] tent = roww.SelectSingleNode("td[1]")
                            .SelectSingleNode("a")
                            .GetAttributeValue("title", "rip").Split();
                        foreach (string este in tent)
                        {
                            if (este == tentativa2[0])
                            {
                                profile = roww;
                                break;
                            }
                        }
                    }
                }

                string origin = "unknown";
                string region = "unknown";
                string height = "unknown";
                string weight = "unknown";
                string gender = "unknown";
                Alignment align = Alignment.BALANCED;
                Aligment2 align2 = Aligment2.NEUTRAL;
                if (profile != null)
                {
                    origin = profile.SelectSingleNode("td[2]").InnerText;
                    region = profile.SelectSingleNode("td[3]").InnerText;
                    height = profile.SelectSingleNode("td[4]").InnerText;
                    weight = profile.SelectSingleNode("td[5]").InnerText;
                    gender = profile.SelectSingleNode("td[6]").InnerText;
                    align = Find_alig(profile.SelectSingleNode("td[7]").InnerText);
                    align2 = Find_alig2(profile.SelectSingleNode("td[7]").InnerText);

                }
                if (cl == Servant_Class.BEAST)
                {
                    align = Alignment.BEAST;
                    align2 = Aligment2.NEUTRAL;
                }
                if (name != "Solomon")
                {
                    Servant novo = new Servant(id, name, cl, rarity, min_atk, max_atk, min_hp, max_hp, origin, region, height, weight, gender, align, align2, Npname, tipo, rank, alltraits);
                    _all_servants.Add(novo);
                }
            }


            int getnum(string txt)
            {
                return int.Parse(txt[0].ToString());
            }


            webClient.Dispose();
            ServantContainer.Save();
        }

        /// <summary>
        /// Funcao que carrega a lista apartir de um ficheiro XML
        /// </summary>
        public static void Loadfromfile()
        {
            XElement teste = ServantContainer.Load();
            _all_servants.Clear();
            foreach (XElement ola in teste.Elements())
            {
                int id = int.Parse(ola.Attribute("Id").Value);
                string nome = ola.Attribute("Name").Value;
                Servant_Class cl = Find_class(ola.Attribute("Class").Value);
                int rarity = int.Parse(ola.Attribute("Rarity").Value);
                int minatk = int.Parse(ola.Attribute("Minimum_Attack").Value);
                int maxatk = int.Parse(ola.Attribute("Maximum_Attack").Value);
                int minhp = int.Parse(ola.Attribute("Minimum_Health").Value);
                int maxhp = int.Parse(ola.Attribute("Maximum_Health").Value);
                string origin = ola.Attribute("Origin").Value;
                string region = ola.Attribute("Region").Value;
                string height = ola.Attribute("Height").Value;
                string weight = ola.Attribute("Weight").Value;
                string gender = ola.Attribute("Gender").Value;
                Alignment alig = Find_alig(ola.Attribute("Aligment").Value);
                Aligment2 alig2 = Find_alig2(ola.Attribute("Aligment2").Value);
                string npname = ola.Attribute("NPName").Value; ;
                NPType tipo = Getnptype(ola.Attribute("NPType").Value);
                SkillRank rank = Getnprank(ola.Attribute("NPRank").Value);
                List<Trait> traits = new List<Trait>();
                for (int i = 1; i <= Enum.GetValues(typeof(TraitDesc)).Length; i++)
                {
                    if (ola.Attribute("Trait" + i) != null)
                    {
                        traits.Add(GetTrait(ola.Attribute("Trait" + i).Value));
                    }
                }


                _all_servants.Add(new Servant(id, nome, cl, rarity, minatk, maxatk, minhp, maxhp, origin, region, height, weight, gender, alig, alig2, npname, tipo, rank, traits));
            }



        }

        /// <summary>
        /// Funcao para buscar um servant a partir de um ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Servant Summon_by_id(int id)
        {

            if (id > All_servants.Count())
            {
                id -= All_servants.Count();
            }
            if (id <= 0)
            {
                id += All_servants.Count();
            }
            return All_servants.Find(x => x.Id == id);
        }

        /// <summary>
        /// Funcao que retorna todos os servants de uma classe especifica
        /// </summary>
        /// <param name="cl"></param>
        /// <returns></returns>
        public static List<Servant> Summon_by_Class(Servant_Class cl)
        {

            List<Servant> res = All_servants.FindAll((x => x.Class == cl));
            return res;
        }

        /// <summary>
        /// <para>Funcao que retorna um servant de uma classe especifica</para>
        /// <para>O Numero e uma seed para buscar o servant e pode ser qualquer numero inteiro</para>
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Servant Summon_by_Class(Servant_Class cl, int a)
        {

            List<Servant> res = All_servants.FindAll(x => x.Class == cl);
            while (res.Count < a)
            {
                a -= res.Count;
            }
            return res[a];
        }

        /// <summary>
        /// Funcao que devolve uma lista de servants que correspondam a uma lista de classes
        /// </summary>
        /// <param name="cl"></param>
        /// <returns></returns>
        public static List<Servant> Summon_by_Class(List<Servant_Class> cl)
        {
            List<Servant> res = new List<Servant>();
            foreach (Servant_Class este in cl)
            {
                res.AddRange(All_servants.FindAll(x => x.Class == este));
            }
            return res;
        }

        /// <summary>
        /// <para>Funcao que devolve um servant que corresponda a uma lista de classes</para>
        /// <para>O Numero e uma seed para buscar o servant e pode ser qualquer numero inteiro</para>
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Servant Summon_by_Class(List<Servant_Class> cl, int a)
        {
            List<Servant> res = new List<Servant>();
            foreach (Servant_Class este in cl)
            {
                res.AddRange(All_servants.FindAll(x => x.Class == este));
            }
            while (res.Count < a)
            {
                a -= res.Count;
            }
            return res[a];
        }

    }
}
