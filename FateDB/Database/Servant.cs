
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FateDB
{
    public class Servant
    {


        private string _name;
        private Servant_Class _class;
        private int _rarity;

        private int _currentatk;
        private int _currenthp;
        private int _lvl = 1;

        private string _origin;
        private string _region;
        private string _height;
        private string _weight;
        private string _gender;
        private Alignment _aligment;
        private Aligment2 _aligment2;
        private int _id;

        public int Maxlvl;


        public int atk
        {
            get => atk;
            set
            {
                if (value < 0)
                {
                    atk = 0;
                }
            }
        }

        public int Hp { get; set; }
        public int Lvl
        {
            get => _lvl;
            set
            {
                if (value > Maxlvl)
                    value = Maxlvl;
                else if (value < 1)
                {
                    _lvl = 1;
                }
                _currentatk = _minatk + (_maxatk / Maxlvl * value);
                _currenthp = _minhp + (_maxatk / Maxlvl * value);
            }
        }

        [XmlElement("ID")]
        public int Id => _id;
        [XmlElement("Name")]
        public string Name => _name;

        [XmlElement("Class")]
        public Servant_Class Class => _class;

        [XmlElement("Rarity")]
        public int Rarity => _rarity;
        public int Atklvl => _currentatk;
        public int Hplvl => _currenthp;
        [XmlElement("Mininum Attack")]
        private int _minatk;
        [XmlElement("Maximum Attack")]
        private int _maxatk;
        [XmlElement("Mininum HP")]
        private int _minhp;
        [XmlElement("Maximum HP")]
        private int _maxhp;

        [XmlElement("Origin")]
        public string Origin => _origin;
        [XmlElement("Region")]
        public string Region => _region;
        [XmlElement("Height")]
        public string Height => _height;
        [XmlElement("Weight")]
        public string Weight => _weight;
        [XmlElement("Gender")]
        public string Gender => _gender;
        [XmlElement("Aligment")]
        public Alignment Aligment => _aligment;
        [XmlElement("Aligment2")]
        public Aligment2 Aligment2 => _aligment2;

        public Servant(int id, string name, Servant_Class classe, int rarity, int min_atk, int max_atk, int min_hp, int max_hp, string origin, string region, string height, string weight, string gender, Alignment aligment, Aligment2 aligment2)
        {
            if (ThroneOfHeroes.Allow_insert)
            {
                _id = id;
                _name = name;
                _class = classe;
                _rarity = rarity;
                _minatk = min_atk;
                _maxatk = max_atk;
                _minhp = min_hp;
                _maxhp = max_hp;
                _origin = origin;
                _region = region;
                _height = height;
                _weight = weight;
                _gender = gender;
                _aligment = aligment;
                _aligment2 = aligment2;
                Maxlvl = 100;
                Lvl = 1;
            }
            else
            {
                Console.WriteLine("DENIED");
                ThroneOfHeroes.Summon(id);
            }

        }
        public Servant()
        {

        }

        public override string ToString()
        {
            string res = Name + " CLASS:" + Class + " " + Rarity + " STAR SERVANT" + " ORIGIN:" + Origin + " LVL:" + Lvl + " ATK:" + _currentatk + " HP:" + _currenthp + " HEIGHT:" + Height + " WEIGHT:" + Weight + " GENDER:" + Gender + " ALIGMENT:" + Aligment2 + " " + Aligment;
            return res.ToString();
        }
    }
}
