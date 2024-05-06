using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RPG_account_system
{
    public class Statistic : Request
    {
        int statistic_id;
        Player player;
        int total_losses;
        int total_kills;
        int total_coins;
        NpgsqlInterval interval;
    }
}
