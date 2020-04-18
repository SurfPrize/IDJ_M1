using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FateDB.Database
{
    [XmlRoot("CServantCollection")]
    public class ServantContainer
    {

        [XmlArray("ServantList")]
        [XmlArrayItem("Servant")]
        public List<Servant> Servants = new List<Servant>();

        public static ServantContainer Load(string path)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(ServantContainer));

            StringReader reader = new StringReader(path);

            ServantContainer servants = serializer.Deserialize(reader) as ServantContainer;

            reader.Close();

            return servants;

        }

        public static void Save(string path)
        {

            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;

            XmlSerializer serializer = new XmlSerializer(ThroneOfHeroes.All_servants.GetType());
            tw = new XmlTextWriter(sw);
            FileStream file = File.Create(path);
            serializer.Serialize(file, ThroneOfHeroes.All_servants);
            
            file.Close();


            XmlDocument teste = new XmlDocument();
            XmlNamespaceManager ns= new XmlNamespaceManager(teste.NameTable);
            ns.AddNamespace("nome",path);
            
        }
    }


}

