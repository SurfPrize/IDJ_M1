using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            ThroneOfHeroes.Loadfromfile();
            Master teste = new Master(ThroneOfHeroes.Summon_by_Class(Servant_Class.AVENGER, 5));
            Servant teste2 = ThroneOfHeroes.Summon_by_Class(Servant_Class.ARCHER, 4);
            teste.Lvl = teste.Maxlvl / 2;
            teste2.Lvl = teste2.Maxlvl / 2;
            Console.WriteLine(teste.ToString());
            Console.WriteLine(teste2.ToString());
            while (teste.IsAlive && teste2.IsAlive)
            {
                AttackSystem.Battle(teste, teste2, 3);
            }
            Console.Write("Enter to Exit");
            Console.ReadLine();
        }
    }
}
