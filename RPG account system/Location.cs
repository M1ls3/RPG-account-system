using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_account_system
{
    public class Location : Request
    {
        int location_id;
        Character character;
        Biome biome;
        int coordinate_x;
        int coordinate_y;
        int coordinate_z;
    }
}
