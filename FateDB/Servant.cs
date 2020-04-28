
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

        private int _maxcurrentatk;
        private int _maxcurrenthp;
        private int _lvl = 1;
        private int _minatk;
        private int _maxatk;
        private int _minhp;
        private int _maxhp;
        private bool _alive = true;
        public bool isAlive => _alive;
        private string _origin;
        private string _region;
        private string _height;
        private string _weight;
        private string _gender;
        private Alignment _aligment;
        private Aligment2 _aligment2;
        private int _id;
        private NPType _nptype;
        private string _npName;
        private NPRank _rank;
        public int Maxlvl;


        public int Atk
        {
            get => Atk;
            set
            {
                if (value < 0)
                {
                    Atk = 0;
                }
            }
        }

        public int Hp
        {
            get => Hp;
            set
            {
                if (value < 0)
                {
                    //morrer
                }

                if (value > _maxcurrenthp)
                {
                    Hp = _maxcurrenthp;
                }
                else
                {
                    Hp = value;
                }
            }
        }


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
                _maxcurrentatk = _minatk + (_maxatk / Maxlvl * value);
                _maxcurrenthp = _minhp + (_maxatk / Maxlvl * value);
            }
        }

        public int Id => _id;
        public string Name => _name;

        public Servant_Class Class => _class;
        public NPType NPType => _nptype;
        public string NPName => _npName;
        public NPRank NPRank => _rank;
        public int Rarity => _rarity;
        public int Atklvl => _maxcurrentatk;
        public int Hplvl => _maxcurrenthp;

        public int Minatk => _minatk;
        public int Maxatk => _maxatk;
        public int Minhp => _minhp;
        public int Maxhp => _maxhp;
        public string Origin => _origin;
        public string Region => _region;
        public string Height => _height;
        public string Weight => _weight;
        public string Gender => _gender;
        public Alignment Aligment => _aligment;
        public Aligment2 Aligment2 => _aligment2;
        public List<AttackType> cards = new List<AttackType>() { AttackType.ATTACK, AttackType.COUNTER, AttackType.SPELL };

        public AttackType[] atk_definido = new AttackType[3];

        public void Pick_cards()
        {
            AttackType[] res = Drawcards();
            foreach (AttackType este in res)
            {
                Console.Write(este + " ");
            }

            Set_cards(res[int.Parse(Console.ReadLine()) - 1], res[int.Parse(Console.ReadLine()) - 1], res[int.Parse(Console.ReadLine()) - 1]);
        }

        public AttackType[] Drawcards()
        {
            Random r = new Random();
            AttackType[] resultado = new AttackType[3];
            for (int i = 0; i < 5; i++)
            {
                resultado[i] = cards[r.Next(0, cards.Count - 1)];
            }
            return resultado;
        }

        public void Set_cards(AttackType first, AttackType second, AttackType third)
        {
            cards.Add(first); cards.Add(second); cards.Add(third);
            atk_definido = new AttackType[3] { first, second, third };
        }

        public void Set_cards(int first, int second, int third)
        {
            cards.Add((AttackType)third); cards.Add((AttackType)third); cards.Add((AttackType)third);
            atk_definido = new AttackType[3] { (AttackType)first, (AttackType)second, (AttackType)third };
        }

        public Servant(Servant novo)
        {
            _id = novo.Id;
            _name = novo.Name;
            _class = novo.Class;
            _rarity = novo.Rarity;
            _minatk = novo.Minatk;
            _maxatk = novo.Maxatk;
            _minhp = novo.Minhp;
            _maxhp = novo.Maxhp;
            _origin = novo.Origin;
            _region = novo.Region;
            _height = novo.Height;
            _weight = novo.Weight;
            _gender = novo.Gender;
            _aligment = novo.Aligment;
            _aligment2 = novo.Aligment2;
            _npName = novo.NPName;
            _nptype = novo.NPType;
            _rank = novo.NPRank;
            Maxlvl = 100;
            Lvl = 1;
        }

        public Servant(int id, string name, Servant_Class classe, int rarity, int min_atk, int max_atk, int min_hp, int max_hp, string origin, string region, string height, string weight, string gender, Alignment aligment, Aligment2 aligment2, string npname, NPType nptipo, NPRank rank)
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
                _npName = npname;
                _nptype = nptipo;
                _rank = rank;
                Maxlvl = 100;
                Lvl = 1;
            }
            else
            {
                Console.WriteLine("DENIED");
                ThroneOfHeroes.Summon_by_id(id);
            }

        }


        public override string ToString()
        {
            string res = Name + " CLASS:" + Class + " " + Rarity + " STAR SERVANT" + " ORIGIN:" + Origin + " LVL:" + Lvl + " ATK:" + _maxcurrentatk + " HP:" + _maxcurrenthp + " HEIGHT:" + Height + " WEIGHT:" + Weight + " GENDER:" + Gender + " ALIGMENT:" + Aligment2 + " " + Aligment + " Noble Phantasm:" + NPName + " TYPE:" + NPType + " RANK:" + NPRank;
            return res.ToString();
        }


    }
}
