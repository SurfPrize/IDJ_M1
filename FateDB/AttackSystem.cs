using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    public static class AttackSystem
    {

        public static void Battle(Servant offense, Servant defense)
        {
            if (!offense.isAlive || !defense.isAlive)
            {
                Console.WriteLine("Ja existe um vencedor");
                return;
            }

            Random r = new Random();
            offense.Pick_cards();
            defense.Set_cards(1, 2, 3);

            for (int i = 0; i < offense.atk_definido.Count(); i++)
            {
                if (!offense.isAlive || !defense.isAlive)
                {
                    Console.WriteLine("Ja existe um vencedor");
                    return;
                }
               
                if (offense.atk_definido[i] == AttackType.COUNTER && defense.atk_definido[i] == AttackType.COUNTER)
                {
                    //nada acontece


                }
                else if (offense.atk_definido[i] == defense.atk_definido[i])
                {
                    if (r.Next(0, 2) == 1)
                    {
                        //sem dano
                        //KLINKKKKKK

                    }
                    else
                    {
                        //ambos apanham
                        Normaldamage(offense, defense);
                        Normaldamage(defense, offense);
                    }
                }
                else
                {
                    //aqui calcula quem apanha base no ataque que escolheram
                    switch (offense.atk_definido[i])
                    {
                        case AttackType.ATTACK:
                            if (defense.atk_definido[i] == AttackType.SPELL)
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
                            if (defense.atk_definido[i] == AttackType.ATTACK)
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
                            if (defense.atk_definido[i] == AttackType.COUNTER)
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

        public static void Normaldamage(Servant dealer, Servant damaged)
        {
            Random r = new Random();
            damaged.Hp -= (int)Math.Round(dealer.Atk / 4f * (r.Next(94, 105) / 100f));
        }

        public static void Blockeddamage(Servant dealer, Servant damaged)
        {
            Random r = new Random();
            damaged.Hp -= (int)Math.Round(dealer.Atk / 4f * (r.Next(94, 105) / 100f) * 0.1f);
        }

        public static void AgressorBonusDamage(Servant dealer, Servant damaged)
        {
            Random r = new Random();
            damaged.Hp -= (int)Math.Round(dealer.Atk / 4 * (r.Next(94, 105) / 100) * 1.05f);
        }

    }
}
