using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    public static class AttackSystem
    {
        /// <summary>
        /// Batalha entre 2 inimigos
        /// </summary>
        /// <param name="offense"></param>
        /// <param name="defense"></param>
        public static void Battle(IUnit offense, IUnit defense, int turns)
        {

            if (!offense.IsAlive || !defense.IsAlive)
            {
                Console.WriteLine("Ja existe um vencedor");
                return;
            }

            Random r = new Random();
            offense.Pick_Cards(turns);
            defense.Pick_Cards_AI(turns);

            for (int i = 0; i < offense.Atk_definido.Count(); i++)
            {
                if (!offense.IsAlive || !defense.IsAlive)
                {
                    Console.WriteLine("Ja existe um vencedor");
                    return;
                }

                if (offense.Atk_definido[i] == AttackType.COUNTER && defense.Atk_definido[i] == AttackType.COUNTER)
                {
                    //nada acontece
                    Console.WriteLine("Both defended");

                }
                else if (offense.Atk_definido[i] == defense.Atk_definido[i])
                {
                    if (r.Next(0, 2) == 1)
                    {
                        //sem dano
                        //KLINKKKKKK
                        Console.WriteLine("Both Clashed");
                    }
                    else
                    {
                        //ambos apanham
                        Console.WriteLine("Both take damage");
                        Normaldamage(offense, defense);
                        Normaldamage(defense, offense);
                    }
                }
                else
                {
                    //aqui calcula quem apanha base no ataque que escolheram
                    switch (offense.Atk_definido[i])
                    {
                        case AttackType.ATTACK:
                            if (defense.Atk_definido[i] == AttackType.SPELL)
                            {
                                AgressorBonusDamage(offense, defense);
                            }
                            else
                            {
                                Blockeddamage(offense, defense);
                                Normaldamage(defense, offense);
                            }
                            break;
                        case AttackType.COUNTER:
                            if (defense.Atk_definido[i] == AttackType.ATTACK)
                            {
                                Blockeddamage(offense, defense);
                                AgressorBonusDamage(offense, defense);
                            }
                            else
                            {
                                Normaldamage(defense, offense);
                            }
                            break;
                        case AttackType.SPELL:
                            if (defense.Atk_definido[i] == AttackType.COUNTER)
                            {
                                AgressorBonusDamage(offense, defense);
                            }
                            else
                            {
                                Normaldamage(defense, offense);
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Dano normal, sem modificadores
        /// </summary>
        /// <param name="dealer"></param>
        /// <param name="damaged"></param>
        public static void Normaldamage(IUnit dealer, IUnit damaged)
        {
            Random r = new Random();
            damaged.Hp -= (int)Math.Round(dealer.Atk / 4f * (r.Next(94, 105) / 100f));
            Console.WriteLine(dealer.Name + " damages " + damaged.Name + " , it still has " + damaged.Hp);
        }

        /// <summary>
        /// Dano occurido ao defender
        /// </summary>
        /// <param name="dealer"></param>
        /// <param name="damaged"></param>
        public static void Blockeddamage(IUnit dealer, IUnit damaged)
        {
            Random r = new Random();
            damaged.Hp -= (int)Math.Round(dealer.Atk / 4f * (r.Next(94, 105) / 100f) * 0.1f);
            Console.WriteLine(dealer.Name + " gets blocked by " + damaged.Name + " , it still has " + damaged.Hp);
        }

        /// <summary>
        /// Dano com modificador de agressor
        /// </summary>
        /// <param name="dealer"></param>
        /// <param name="damaged"></param>
        public static void AgressorBonusDamage(IUnit dealer, IUnit damaged)
        {
            Random r = new Random();
            damaged.Hp -= (int)Math.Round(dealer.Atk / 4 * (r.Next(94, 105) / 100) * 1.05f);
            Console.WriteLine(dealer.Name + " damages " + damaged.Name + " , it still has " + damaged.Hp);
        }

    }
}
