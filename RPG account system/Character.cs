using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_account_system
{
    enum Class
    {
        [Description("Warrior")]
        Warrior,
        [Description("Mage")]
        Mage,
        [Description("Ranger")]
        Ranger,
        [Description("Rogue")]
        Rogue,
        [Description("Cleric")]
        Cleric,
        [Description("Summoner")]
        Summoner
    }

    public class Character : Request
    {
        int character_id;
        Player player;
        Class char_class;
        string nickname;
        int level;
        int mana;
        int stamina;
        int coins;
        int hp;
        int damage;
        double critdmg;
        double critrate;
        int protection;
    }
}
