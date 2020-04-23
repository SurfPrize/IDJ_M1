using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    public class Master : Servant
    {
        private string _name = "NO NAME";
        public string Master_Name
        {
            get => _name; set
            {
                if (value == "" || value == null)
                {
                    _name = "NO NAME";
                }
                else
                {
                    _name = value;
                }
            }
        }
        private Servant parse_servant(Servant serv)
        {
            switch (serv.Class)
            {
                case Servant_Class.SABER:
                case Servant_Class.ARCHER:
                case Servant_Class.LANCER:
                case Servant_Class.ASSASSIN:
                case Servant_Class.CASTER:
                case Servant_Class.BERSERKER:
                case Servant_Class.RIDER:
                    serv = ThroneOfHeroes.Summon_by_Class(new List<Servant_Class>() {Servant_Class.AVENGER,
                        Servant_Class.RULER,
                        Servant_Class.MOONCANCER,
                        Servant_Class.FOREIGNER,
                        Servant_Class.ALTER,
                        Servant_Class.BEAST}, DateTime.Today.Second);
                    break;
            }
            return serv;
        }
        public string Master_region;

        public Master(string name, string region, Servant serv) : base(serv)
        {
            serv = parse_servant(serv);
            Master_Name = name;
            Master_region = region;
        }

        public Master(Servant serv):base(serv)
        {
            serv = parse_servant(serv);
        }

        public override string ToString()
        {

            string resultado = Name + " Region:" + Master_region + " \n " + Name + "'S SERVANT:" + base.ToString();


            return resultado;
        }

    }
}
