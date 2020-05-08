using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    public interface IUnit
    {

        string Name { get; }
        /// <summary>
        /// Classe da Unidade
        /// </summary>
        Servant_Class Class { get; }

        /// <summary>
        /// Nivel da Unidade
        /// </summary>
        int Lvl { get; set; }

        /// <summary>
        /// Ataque base da unidade
        /// </summary>
        int Atk { get; set; }

        /// <summary>
        /// Pontos de vida da personagem
        /// </summary>
        int Hp { get; set; }

        /// <summary>
        /// Se esta personagem esta viva
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Tipo de Noble Phastasm(Abilidade especial) desta Unidade
        /// </summary>
        NPType NPType { get; }

        /// <summary>
        /// Nome do Noble Phastasm(Abilidade especial) desta Unidade
        /// </summary>
        string NPName { get; }

        /// <summary>
        /// Rank do Noble Phastasm(Abilidade especial) de E a EX
        /// </summary>
        SkillRank NPRank { get; }

        /// <summary>
        /// traits da unidade
        /// </summary>
        List<Trait> Alltraits { get; }

        /// <summary>
        /// Metodo para escolher as cartas da personagem
        /// </summary>
        /// <param name="turnos"></param>
        void Pick_Cards(int turnos);
        /// <summary>
        /// metodo para escolher as cartas a partir de um algoritmo
        /// </summary>
        /// <param name="turnos"></param>
        void Pick_Cards_AI(int turnos);

        /// <summary>
        /// Ataques que a unidade vai realizar no turno
        /// </summary>
        List<AttackType> Atk_definido { get; set; }

    }
}
