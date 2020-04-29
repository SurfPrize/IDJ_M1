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


        private static List<Servant> _summoned_servants = new List<Servant>();
        private static List<Servant> _all_servants = new List<Servant>();
        public static List<Servant> All_servants => _all_servants;
        public static List<Servant> Summoned_servants => _summoned_servants;
        private static bool _allow_insert = false;
        public static bool Allow_insert => _allow_insert;

        private static Alignment find_alig(string txt)
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

        private static Aligment2 find_alig2(string txt)
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

        private static Servant_Class find_class(string txt)
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

        private static NPType getnptype(string txt)
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

        private static SkillRank getnprank(string txt)
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

        private static Trait GetTrait(string txt, string trait)
        {
            Enum.TryParse<TraitDesc>(trait, out TraitDesc desc);
            Enum.TryParse<SkillRank>(txt.Replace(trait, ""), out SkillRank skillres);
            Trait res = new Trait(desc, skillres);

            return res;
        }
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

        public static void UpdateList()
        {
            _allow_insert = true;
            WebClient webClient = new WebClient();
            webClient.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)";
            string page = webClient.DownloadString("https://grandorder.wiki/Servant_List");
            string page2 = webClient.DownloadString("https://grandorder.wiki/Servants_by_Profile");
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            HtmlDocument doc2 = new HtmlDocument();
            doc2.LoadHtml(page2);
            doc2.OptionEmptyCollection = true;

            List<HtmlNode> ServantStats = doc.DocumentNode.SelectSingleNode("//table[last()]").Descendants("tr").Skip(1).ToList();
            List<HtmlNode> ServantProfiles = doc2.DocumentNode.SelectSingleNode("//table").Descendants("tr").Skip(1).ToList();

            foreach (var row in ServantStats)
            {
                Console.WriteLine("https://grandorder.wiki" + row.SelectSingleNode("td[3]")
                    .SelectSingleNode("a")
                    .GetAttributeValue("href", "rip"));
                HtmlDocument servanthtml = new HtmlDocument();
                string serv_page = webClient.DownloadString("https://grandorder.wiki" + row.SelectSingleNode("td[3]")
                    .SelectSingleNode("a")
                    .GetAttributeValue("href", "rip"));

                servanthtml.LoadHtml(serv_page);

                int id = int.Parse(row.SelectSingleNode("td[1]").InnerText.ToString());

                string name = row.SelectSingleNode("td[3]")
                    .SelectSingleNode("a")
                    .GetAttributeValue("title", "rip");

                Servant_Class cl = find_class(row.SelectSingleNode("td[4]")
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
                            if (este.GetAttributeValue("title", "nope").Contains("NP") ||
                                este.GetAttributeValue("title", "nope").Contains("EX") ||
                                este.GetAttributeValue("title", "nope").Contains("Lord Camelot"))
                            {
                                if (este.GetAttributeValue("title", "nope").Split().First() == "Rank" ||
                                    este.GetAttributeValue("title", "nope").Split().First() == "NP" ||
                                    este.GetAttributeValue("title", "nope").Split().First() == "EX" ||
                                    este.GetAttributeValue("title", "nope") == "Lord Camelot")
                                {
                                    

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

                                    tipo = getnptype(este
                                    .Descendants("p")
                                    .Skip(1)
                                    .First()
                                    .InnerText);

                                    rank = getnprank(este
                                    .GetAttributeValue("title", "unknown"));
                                    found = true;
                                }
                            }
                            else
                            {
                                if (este.GetAttributeValue("title", "nope") != "nope")
                                {
                                    foreach (string tr in Enum.GetNames(typeof(TraitDesc)).ToList())
                                    {

                                        if (este.GetAttributeValue("title", "nope").ToUpper().Contains(tr.Replace("_", " ")))
                                        {
                                            Console.WriteLine(name + " " + tr);
                                            alltraits.Add(GetTrait(este.GetAttributeValue("title", "nope"), tr));
                                        }
                                    }
                                }
                            }
                        }

                    }
                }



                // string skill1 = servanthtml.DocumentNode.SelectSingleNode("//div[@title='1st Skill']").Descendants("div").Count();

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
                    align = find_alig(profile.SelectSingleNode("td[7]").InnerText);
                    align2 = find_alig2(profile.SelectSingleNode("td[7]").InnerText);

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


            _allow_insert = false;
            webClient.Dispose();
            ServantContainer.Save();
        }

        public static void Loadfromfile()
        {
            XElement teste = ServantContainer.Load();
            _all_servants.Clear();
            _allow_insert = true;
            foreach (XElement ola in teste.Elements())
            {
                int id = int.Parse(ola.Attribute("Id").Value);
                string nome = ola.Attribute("Name").Value;
                Servant_Class cl = find_class(ola.Attribute("Class").Value);
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
                Alignment alig = find_alig(ola.Attribute("Aligment").Value);
                Aligment2 alig2 = find_alig2(ola.Attribute("Aligment2").Value);
                string npname = ola.Attribute("NPName").Value; ;
                NPType tipo = getnptype(ola.Attribute("NPType").Value);
                SkillRank rank = getnprank(ola.Attribute("NPRank").Value);
                List<Trait> traits = new List<Trait>();
                for (int i = 1; i <= Enum.GetValues(typeof(TraitDesc)).Length; i++)
                {
                    if (ola.Attribute("Trait" + i) != null)
                    {
                        traits.Add(GetTrait(ola.Attribute("Trait " + i).Value));
                    }
                }


                _all_servants.Add(new Servant(id, nome, cl, rarity, minatk, maxatk, minhp, maxhp, origin, region, height, weight, gender, alig, alig2, npname, tipo, rank, traits));
            }
            _allow_insert = false;


        }

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

        public static List<Servant> Summon_by_Class(Servant_Class cl)
        {

            List<Servant> res = All_servants.FindAll((x => x.Class == cl));
            return res;
        }

        public static Servant Summon_by_Class(Servant_Class cl, int a)
        {

            List<Servant> res = All_servants.FindAll(x => x.Class == cl);
            while (res.Count < a)
            {
                a -= res.Count;
            }
            return res[a];
        }

        public static List<Servant> Summon_by_Class(List<Servant_Class> cl)
        {
            List<Servant> res = new List<Servant>();
            foreach (Servant_Class este in cl)
            {
                res.AddRange(All_servants.FindAll(x => x.Class == este));
            }
            return res;
        }

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
