using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateDB
{
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

    public enum Alignment
    {
        GOOD,
        BALANCED,
        INSANE,
        EVIL,
        SUMMER,
        BEAST
    }
    public enum Aligment2
    {
        CHAOTIC,
        NEUTRAL,
        LAWFUL
    }

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
    public enum NPRank
    {
        EX,
        A,
        B,
        C,
        D,
        E
    }
}
