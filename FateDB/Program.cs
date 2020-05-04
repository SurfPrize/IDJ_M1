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
            Console.WriteLine(teste.ToString());
            Servant teste2 = ThroneOfHeroes.Summon_by_Class(Servant_Class.ARCHER, 4);
            while (teste.IsAlive || teste2.IsAlive)
            {
                AttackSystem.Battle(teste, teste2, 3);
            }
            Console.Write("Enter to Exit");
            Console.ReadLine();
        }
    }
}
