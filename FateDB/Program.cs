using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Master teste = new Master();
            teste.servant = ThroneOfHeroes.Summon(10);
            Servant testee = new Servant(9,"a", Servant_Class.RIDER,3,1,1,1,1, "a","a","a","a", "a",Alignment.BALANCED,Aligment2.CHAOTIC);
            Console.WriteLine(teste.ToString());
            Console.Write("Enter to Exit");
            Console.ReadLine();
        }
    }
}
