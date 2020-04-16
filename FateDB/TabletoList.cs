using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    internal static class TabletoList
    {
        
        static TabletoList()
        {

        }
        public static void getList()
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString("https://grandorder.wiki/Servant_List");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            List<List<string>> table = doc.DocumentNode.SelectNodes("//table[last()]")
                        .Descendants("tr")
                        .Skip(1)
                        .Where(tr => tr.Elements("td").Count() > 1)
                        .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                        .ToList();

            Console.WriteLine(table.Count);
            foreach (List<string> este in table)
            {

                Console.WriteLine(este[2]);

            }


        }
    }
}
