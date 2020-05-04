using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FateDB.Database
{
    public class ServantContainer
    {
        public static string path = @"C:\Users\Utilizador\Documents\FateDB\IDJ_M1\teste";
        public static string dbname = @"\meme.xml";
        public static XElement Load()
        {

            XElement servants = XElement.Load(path + dbname);

            return servants;

        }

        public static void Save()
        {

            //XElement xml = new XElement("Servants", ThroneOfHeroes.All_servants.Select(x => new XElement("Servant",
            //                                   new XAttribute("Id", x.Id),
            //                                   new XAttribute("Name", x.Name),
            //                                   new XAttribute("Class", x.Class),
            //                                   new XAttribute("Rarity", x.Rarity),
            //                                   new XAttribute("Minimum_Attack", x.Minatk),
            //                                   new XAttribute("Maximum_Attack", x.Maxatk),
            //                                   new XAttribute("Minimum_Health", x.Minhp),
            //                                   new XAttribute("Maximum_Health", x.Maxhp),
            //                                   new XAttribute("Origin", x.Origin),
            //                                   new XAttribute("Region", x.Region),
            //                                   new XAttribute("Height", x.Height),
            //                                   new XAttribute("Weight", x.Weight),
            //                                   new XAttribute("Gender", x.Gender),
            //                                   new XAttribute("Aligment", x.Aligment),
            //                                   new XAttribute("Aligment2", x.Aligment2),
            //                                   new XAttribute("NPName", x.NPName),
            //                                   new XAttribute("NPType", x.NPType),
            //                                   new XAttribute("NPRank", x.NPRank),
            //                                   new XAttribute("Traits", x.Alltraits))));

            XElement xml = new XElement("Servants");

            foreach (Servant x in ThroneOfHeroes.All_servants)
            {
                XElement current = new XElement("Servant");
                current.Add(new XAttribute("Id", x.Id));
                current.Add(new XAttribute("Name", x.Name));
                current.Add(new XAttribute("Class", x.Class));
                current.Add(new XAttribute("Rarity", x.Rarity));
                current.Add(new XAttribute("Minimum_Attack", x.Minatk));
                current.Add(new XAttribute("Maximum_Attack", x.Maxatk));
                current.Add(new XAttribute("Minimum_Health", x.Minhp));
                current.Add(new XAttribute("Maximum_Health", x.Maxhp));
                current.Add(new XAttribute("Origin", x.Origin));
                current.Add(new XAttribute("Region", x.Region));
                current.Add(new XAttribute("Height", x.Height));
                current.Add(new XAttribute("Weight", x.Weight));
                current.Add(new XAttribute("Gender", x.Gender));
                current.Add(new XAttribute("Aligment", x.Aligment));
                current.Add(new XAttribute("Aligment2", x.Aligment2));
                current.Add(new XAttribute("NPName", x.NPName));
                current.Add(new XAttribute("NPType", x.NPType));
                current.Add(new XAttribute("NPRank", x.NPRank));
                for (int i = 1; i <= x.Alltraits.Count; i++)
                {
                    current.Add(new XAttribute("Trait" + i, x.Alltraits[i-1].trait + " " + x.Alltraits[i-1].rank));
                }

                xml.Add(current);
            }
            xml.Save(path + dbname);
        }
    }


}

