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
        public static string path = @"C:\Users\Utilizador\Documents\FateDB\IDJ_M1\teste\meme.xml";

        public static XElement Load()
        {

            XElement servants = XElement.Load(path);

            return servants;

        }

        public static void Save()
        {

            XElement xml = new XElement("Servants", ThroneOfHeroes.All_servants.Select(x => new XElement("Servant",
                                               new XAttribute("Id", x.Id),
                                               new XAttribute("Name", x.Name),
                                               new XAttribute("Class", x.Class),
                                               new XAttribute("Rarity", x.Rarity),
                                               new XAttribute("Minimum_Attack", x.Minatk),
                                               new XAttribute("Maximum_Attack", x.Maxatk),
                                               new XAttribute("Minimum_Health", x.Minhp),
                                               new XAttribute("Maximum_Health", x.Maxhp),
                                               new XAttribute("Origin", x.Origin),
                                               new XAttribute("Region", x.Region),
                                               new XAttribute("Height", x.Height),
                                               new XAttribute("Gender", x.Gender),
                                               new XAttribute("Aligment", x.Aligment),
                                               new XAttribute("Aligment2", x.Aligment2))));
            Console.WriteLine(path);
            xml.Save(path);
        }
    }


}

