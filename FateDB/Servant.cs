
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FateDB
{
    /// <summary>
    /// Personagem do jogo
    /// </summary>
    public class Servant: IUnit
    {
        /// <summary>
        /// Nome do personagem
        /// </summary>
        public string Name => _name;
        private string _name;

        /// <summary>
        /// Classe da personagem
        /// </summary>
        public Servant_Class Class => _class;
        private Servant_Class _class;

        /// <summary>
        /// Raridade da personagem de 1 a 5
        /// </summary>
        public int Rarity => _rarity;
        private int _rarity;

        /// <summary>
        /// Nivel da personagem
        /// </summary>
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
                Hp = _maxcurrenthp;
                Atk = _maxcurrentatk;
            }
        }
        private int _lvl = 1;

        private int _atk;
        /// <summary>
        /// Ataque atual da personagem, que varia consoante o nivel.
        /// </summary>
        public int Atk
        {
            get => _atk;
            set
            {
                if (value < 0)
                {
                    _atk = 0;
                }
                else
                {
                    _atk = value;
                }
            }
        }
        /// <summary>
        /// Ataque da personagem a nivel 1
        /// </summary>
        public int Minatk => _minatk;
        private int _minatk;

        /// <summary>
        /// Ataque da personagem a nivel maximo
        /// </summary>
        public int Maxatk => _maxatk;
        private int _maxatk;

        /// <summary>
        /// Ataque maximo da personagem, sem buffs nem debuffs
        /// </summary>
        public int Atklvl => _maxcurrentatk;
        private int _maxcurrentatk;

        private int _hp;
        /// <summary>
        /// Pontos de vida da personagem, que varia com o nivel da mesma
        /// </summary>
        public int Hp
        {
            get => _hp;
            set
            {
                if (value <= 0)
                {
                    //morrer
                    _alive = false;
                    _hp = 0;
                }

                if (value > _maxcurrenthp)
                {
                    _hp = _maxcurrenthp;
                }
                else
                {
                    _hp = value;
                }
            }
        }

        /// <summary>
        /// Maximod e pontos de vida da personagem, sem buffs nem debuffs
        /// </summary>
        public int Hplvl => _maxcurrenthp;
        private int _maxcurrenthp;

        /// <summary>
        /// Pontos de vida da personagem a nivel 1
        /// </summary>
        public int Minhp => _minhp;
        private int _minhp;

        /// <summary>
        /// Pontos de vida da personagem a nivel maximo
        /// </summary>
        public int Maxhp => _maxhp;
        private int _maxhp;

        /// <summary>
        /// Se esta personagem esta viva
        /// </summary>
        public bool IsAlive => _alive;
        private bool _alive = true;

        /// <summary>
        /// Nivel maximo da personagem
        /// </summary>
        public int Maxlvl;

        /// <summary>
        /// Id da personagem, serve para evocar esta personagem
        /// </summary>
        public int Id => _id;
        private int _id;

        /// <summary>
        /// Origem da personagem(se vem de uma estoria, da historia humana, etc...)
        /// </summary>
        public string Origin => _origin;
        private string _origin;

        /// <summary>
        /// Regiao da personagem
        /// </summary>
        public string Region => _region;
        private string _region;

        /// <summary>
        /// Altura da personagem
        /// </summary>
        public string Height => _height;
        private string _height;

        /// <summary>
        /// Peso da personagem
        /// </summary>
        public string Weight => _weight;
        private string _weight;

        /// <summary>
        /// Sexo da personagem
        /// </summary>
        public string Gender => _gender;
        private string _gender;

        /// <summary>
        /// Alinhamento mental da personagem (Chaotic, Lawful, etc..)
        /// </summary>
        public Alignment Aligment => _aligment;
        private Alignment _aligment;

        /// <summary>
        /// Alinhamento mental da personagem (Evil, Good, etc..)
        /// </summary>
        public Aligment2 Aligment2 => _aligment2;
        private Aligment2 _aligment2;

        /// <summary>
        /// <para>Tipo do Noble Phanthasm, nem todos sao ofensivos.</para>
        /// <para>Regra geral, ao usar o NP a personagem tera o seu nome revelado</para>
        /// </summary>
        public NPType NPType => _nptype;
        private NPType _nptype;

        /// <summary>
        /// Nome do Noble Phantasm
        /// </summary>
        public string NPName => _npName;
        private string _npName;

        /// <summary>
        /// Rank do Noble Phantasm, E sendo o mais fraco e EX o mais forte
        /// </summary>
        public SkillRank NPRank => _rank;
        private SkillRank _rank;

        /// <summary>
        /// Caracteristicas da personagem
        /// </summary>
        public List<Trait> Alltraits => _alltraits;
        private List<Trait> _alltraits;

        /// <summary>
        /// Cartas de ataque da personagem
        /// </summary>
        public List<AttackType> cards = new List<AttackType>() { AttackType.ATTACK, AttackType.COUNTER, AttackType.SPELL };

        /// <summary>
        /// Ataques da personagem para o turno
        /// </summary>
        public List<AttackType> Atk_definido { get; set; }

        //Sistema antigo, era lixo
        /// <summary>
        /// Metodo para devolver 5 cartas, para o jogador escolher.
        /// </summary>
        /// <returns></returns>
        //public List<AttackType> Drawcards(int amount)
        //{
        //    Random r = new Random();
        //    List<AttackType> resultado = new List<AttackType>();
        //    for (int i = 0; i < amount; i++)
        //    {
        //        resultado.Add(cards[r.Next(0, cards.Count - 1)]);
        //    }
        //    return resultado;
        //}


        //public void Pick_Cards(int amount)
        //{
        //    Random r = new Random();
        //    List<AttackType> res = Drawcards(amount);
        //    List<AttackType> res_display = new List<AttackType>(res);
        //    List<int> order = new List<int>();

        //    for (int i = 0; i < amount; i++)
        //    {
        //        foreach (AttackType este in res_display)
        //        {
        //            Console.WriteLine(este + " ");
        //        }
        //        Console.WriteLine("ATTACK " + (i + 1));
        //        order.Add(int.Parse(Console.ReadLine()) - 1);
        //        res_display.RemoveAt(order[i]);
        //    }
        //    CardPicker(amount, res, order);
        //}

        //public List<AttackType> CardPicker(int amount, List<AttackType> possiveis_escolhas, List<int> ord)
        //{
        //    Random r = new Random();
        //    List<AttackType> resultado = new List<AttackType>();
        //    for (int i = 0; i < ord.Count; i++)
        //    {
        //        resultado.Add(possiveis_escolhas[ord[i]]);
        //    }
        //    return resultado;
        //}



        //public List<AttackType> CardPickerAI(int amount, List<AttackType> possiveis_escolhas)
        //{
        //    Random r = new Random();
        //    List<AttackType> resultado = new List<AttackType>();
        //    for (int i = 0; i < amount; i++)
        //    {
        //        resultado.Add(possiveis_escolhas[r.Next(0, possiveis_escolhas.Count - 1)]);
        //    }
        //    return resultado;
        //}

        //public void Pick_Cards_AI(int amount)
        //{
        //    Random r = new Random();
        //    List<AttackType> res = Drawcards(amount);

        //    CardPickerAI(amount, res);
        //}

        ///// <summary>
        ///// Metodo para definir as cartas para o proximo turno.
        ///// <para>Este metodo so define as cartas, sem nenhuma base</para>
        ///// </summary>
        ///// <param name="first"></param>
        ///// <param name="second"></param>
        ///// <param name="third"></param>
        //public void Set_cards(int first, int second, int third)
        //{
        //    cards.Add((AttackType)third); cards.Add((AttackType)third); cards.Add((AttackType)third);
        //    atk_definido = new AttackType[3] { (AttackType)first, (AttackType)second, (AttackType)third };
        //}


        public AttackType ParseCardbyList(int n)
        {
            return cards[n - 1];
        }
        public AttackType ParseCardbyEnum(int n)
        {
            try
            {
                return (AttackType)n;
            }
            catch
            {
                Console.WriteLine("Valor fora de range, retornando, attack");
                return AttackType.ATTACK;
            }
        }

        public void Pick_Cards(int turnos)
        {
            Atk_definido = new List<AttackType>();


            for (int i = 0; i < turnos; i++)
            {
                foreach (AttackType este in cards)
                {
                    Console.WriteLine(este);
                }
                Atk_definido.Add(ParseCardbyList(int.Parse(Console.ReadLine())));
            }
        }

        public void Pick_Cards_AI(int turnos)
        {
            Random r = new Random();
            Atk_definido = new List<AttackType>();

            for (int i = 0; i < turnos; i++)
            {
                Atk_definido.Add(ParseCardbyList(r.Next(1, 4)));
            }
        }

        /// <summary>
        /// Construtor para duplicar um servant
        /// </summary>
        /// <param name="novo"></param>
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

        /// <summary>
        /// Construtor para carregar personagens
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="classe"></param>
        /// <param name="rarity"></param>
        /// <param name="min_atk"></param>
        /// <param name="max_atk"></param>
        /// <param name="min_hp"></param>
        /// <param name="max_hp"></param>
        /// <param name="origin"></param>
        /// <param name="region"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="gender"></param>
        /// <param name="aligment"></param>
        /// <param name="aligment2"></param>
        /// <param name="npname"></param>
        /// <param name="nptipo"></param>
        /// <param name="rank"></param>
        /// <param name="atraits"></param>
        public Servant(int id, string name, Servant_Class classe, int rarity, int min_atk, int max_atk, int min_hp, int max_hp, string origin, string region, string height, string weight, string gender, Alignment aligment, Aligment2 aligment2, string npname, NPType nptipo, SkillRank rank, List<Trait> atraits)
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
            _alltraits = atraits;
            Maxlvl = 100;
            Lvl = 1;
        }

        /// <summary>
        /// se usares este construtor u gay
        /// </summary>
        public Servant()
        {
            _id = 0;
            _name = "NO NAME";
            _class = Servant_Class.SABER;
            _rarity = 1;
            _minatk = 1000;
            _maxatk = 6000;
            _minhp = 1000;
            _maxhp = 6000;
            _origin = "UNKNOWN";
            _region = "UNKNOWN";
            _height = "UNKNOWN";
            _weight = "UNKNOWN";
            _gender = "UNKNOWN";
            _aligment = Alignment.BALANCED;
            _aligment2 = Aligment2.NEUTRAL;
            _npName = "UNKNOWN";
            _nptype = NPType.ANTI_ARMY;
            _rank = SkillRank.D;
            Maxlvl = 100;
            Lvl = 1;
        }

        public override string ToString()
        {
            string res = Name + " CLASS:" + Class + " " + Rarity + " STAR SERVANT" + " ORIGIN:" + Origin + " LVL:" + Lvl + " ATK:" + _maxcurrentatk + " HP:" + _maxcurrenthp + " HEIGHT:" + Height + " WEIGHT:" + Weight + " GENDER:" + Gender + " ALIGMENT:" + Aligment2 + " " + Aligment + " Noble Phantasm:" + NPName + " TYPE:" + NPType + " RANK:" + NPRank;
            return res.ToString();
        }
    }

    /// <summary>
    /// Caracteristica de uma personagem
    /// </summary>
    public class Trait
    {
        /// <summary>
        /// Tipo de trait
        /// </summary>
        public TraitDesc trait;
        /// <summary>
        /// Rank do trait da personagem, varia entre E e EX
        /// </summary>
        public SkillRank rank;

        /// <summary>
        /// Construtor do Trait
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="r"></param>
        public Trait(TraitDesc desc, SkillRank r)
        {
            trait = desc;
            rank = r;
        }
    }
}
