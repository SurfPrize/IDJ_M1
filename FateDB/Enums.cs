using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
    /// <summary>
    /// Classes de um servant
    /// </summary>
    public enum Servant_Class
    {
        SABER,
        LANCER,
        ARCHER,
        RIDER,
        ASSASSIN,
        CASTER,
        BERSERKER,
        RULER,
        AVENGER,
        MOONCANCER,
        SHIELDER,
        ALTER,
        FOREIGNER,
        BEAST,
        ERROR
    }

    /// <summary>
    /// 2a parte de um aligment de um servant
    /// </summary>
    public enum Alignment
    {
        GOOD,
        BALANCED,
        INSANE,
        EVIL,
        SUMMER,
        BEAST
    }
    /// <summary>
    /// 1a parte de um aligment de um servant
    /// </summary>
    public enum Aligment2
    {
        CHAOTIC,
        NEUTRAL,
        LAWFUL
    }

    /// <summary>
    /// O tipo de Noble Phantasm de um servant, O noble phantsm e uma habilidade especial que revela a natureza historica de um servant
    /// </summary>
    public enum NPType
    {
        ANTI_ARMY,
        ANTI_BARRIER,
        ANTI_VEHICLE,
        ANTI_BEAST,
        ANTI_CIVILIZATION,
        ANTI_DEMON,
        ANTI_DIVINE,
        ANTI_GOD,
        ANTI_EVIL,
        ANTI_FORTRESS,
        ANTI_HUMANITY,
        ANTI_KING,
        ANTI_MAGIC,
        ANTI_THAUMATURGY,
        ANTI_PERSONEL,
        ANTI_PURGE,
        ANTI_STAR,
        ANTI_TERRAIN,
        ANTI_UNIT,
        ANTI_WORLD,
        BARRIER,
        LABYRINTH,
        MAGECRAFT,
        POETRY,
        SPIRIT
    }

    /// <summary>
    /// O rank de uma habilidade, que varia entre E e EX, sendo EX o melhor rank
    /// </summary>
    public enum SkillRank
    {
        EX,
        A,
        B,
        C,
        D,
        E
    }

    /// <summary>
    /// Descricao de uma trait, isto pode ser utilizado para dar passivas especificas a servants que tenham estas passivas
    /// </summary>
    public enum TraitDesc
    {
        MADNESS_ENHANCMENT,
        INDEPENDENT_ACTION,
        TERRITORY_CREATION,
        RIDING,
        DIVINITY,
        TRANSCENDENTAL_EXISTENCE,
        CORE_OF_THE_GODDESS,
        ITEM_CONSTRUCTION,
        MAGIC_RESISTANCE,
        PRESENCE_CONCELAMENT,
        SELFRESTORATION
    }

    /// <summary>
    /// Tipos de ataques que um servant pode usar
    /// </summary>
    public enum AttackType
    {
        ATTACK,
        COUNTER,
        SPELL
    }
}
