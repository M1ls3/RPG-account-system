using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_account_system
{
    enum Category
    {
        [Description("Player")]
        Player,
        [Description("Admin")]
        Admin
    }

    public class Player : Request
    {
        int player_id;
        Category category;
        string login;
        int age;
        string email;
        DateTime registration_date;
        string password;
    }
}
