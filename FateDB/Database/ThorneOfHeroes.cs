using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FateDB.Database
{
    internal static class ThorneOfHeroes
    {
        private static List<Servant> _all_servants;
        private static List<Servant> _summoned_servants;
        public static List<Servant> All_servants => _all_servants;
        public static List<Servant> Summoned_servants => _summoned_servants;



        public static void UpdateList()
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString("https://grandorder.wiki/Servant_List");
            string page2 = webClient.DownloadString("https://grandorder.wiki/Servants_by_Profile");
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            HtmlAgilityPack.HtmlDocument doc2 = new HtmlAgilityPack.HtmlDocument();
            doc2.LoadHtml(page2);
            doc2.OptionEmptyCollection = true;


            List<HtmlNode> ServantStats = doc.DocumentNode.SelectSingleNode("//table[last()]").Descendants("tr").Skip(1).ToList();
            List<HtmlNode> ServantProfiles = doc2.DocumentNode.SelectSingleNode("//table").Descendants("tr").Skip(1).ToList();

            foreach (var row in ServantStats)
            {
                int id = int.Parse(row.SelectSingleNode("td[1]").InnerText.ToString());
                string name = row.SelectSingleNode("td[3]").SelectSingleNode("a").GetAttributeValue("title", "rip");
                Servant_Class cl = find_class(row.SelectSingleNode("td[4]").SelectSingleNode("div").SelectSingleNode("div").SelectSingleNode("img").GetAttributeValue("alt", "unknown"));
                int rarity = getnum(row.SelectSingleNode("td[5]").InnerText.ToString());
                int min_atk = int.Parse((row.SelectSingleNode("td[6]").InnerText.ToString()));
                int max_atk = int.Parse((row.SelectSingleNode("td[7]").InnerText.ToString()));
                int min_hp = int.Parse((row.SelectSingleNode("td[8]").InnerText.ToString()));
                int max_hp = int.Parse((row.SelectSingleNode("td[9]").InnerText.ToString()));
                HtmlNode profile = null;
                string[] tentativa2 = name.Split();

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

                        string[] tent = roww.SelectSingleNode("td[1]").SelectSingleNode("a").GetAttributeValue("title", "rip").Split();
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

                string origin = "";
                string region = "";
                string height = "";
                string weight = "";
                string gender = "";
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
                else
                {
                    origin = "unknown";
                }
                if (cl == Servant_Class.BEAST)
                {
                    align = Alignment.BEAST;
                    align2 = Aligment2.NEUTRAL;
                }
                Servant novo = new Servant(id, name, cl, rarity, min_atk, max_atk, min_hp, max_hp, origin, region, height, weight, gender, align, align2);
                _all_servants.Add(novo);
            }


            int getnum(string txt)
            {
                return int.Parse(txt[0].ToString());
            }


            Alignment find_alig(string txt)
            {
                string[] allcaps = txt.ToUpper().Split();
                string[] all = Servant_Class.GetNames(typeof(Alignment));
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

            Aligment2 find_alig2(string txt)
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

            Servant_Class find_class(string txt)
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
        }

        public static Servant Summon(int id)
        {
            return All_servants.Find(x=> x.Id==id);
        }
    }
}
