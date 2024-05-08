using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Numerics;

namespace RPG_account_system
{
    public enum Class
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
        private int character_id;
        private Player player;
        private Class char_class;
        private string nickname;
        private int level;
        private int mana;
        private int stamina;
        private int coins;
        private int hp;
        private int damage;
        private double critdmg;
        private double critrate;
        private int protection;

        public int CharacterId
        { get { return character_id; } set { character_id = value; } }

        public Player Player
        { get { return player; } set { player = value; } }
        
        public Class CharClass
        { get { return char_class; } set { char_class = value; } }

        public string Nickname
        { get { return nickname; } set { nickname = value; } }

        public int Level
        { get { return level; } set { level = value; } }

        public int Mana
        { get { return mana; } set { mana = value; } }

        public int Stamina
        { get { return stamina; } set { stamina = value; } }

        public int Coins
        { get { return coins; } set { coins = value; } }

        public int Hp
        { get { return hp; } set { hp = value; } }

        public int Damage
        { get { return damage; } set { damage = value; } }

        public double CritDmg
        { get { return critdmg; } set { critdmg = value; } }

        public double CritRate
        { get { return critrate; } set { critrate = value; } }

        public int Protection
        { get { return protection; } set { protection = value; } }


        public Character() { }

        public Character(Player player, Class char_class, string nickname, int level, int mana, int stamina, int coins, int hp, int damage, double critdmg, double critrate, int protection)
        {
            this.player = player;
            this.char_class = char_class;
            this.nickname = nickname;
            this.level = level;
            this.mana = mana;
            this.stamina = stamina;
            this.coins = coins;
            this.hp = hp;
            this.damage = damage;
            this.critdmg = critdmg;
            this.critrate = critrate;
            this.protection = protection;
        }

        public Character(string request)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();

                    string sql = request;
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                character_id = reader.GetInt32(0);
                                player = new Player($"SELECT * FROM players WHERE player_id = {reader.GetInt32(1)}");
                                char_class = EnumHelper.GetClassFromDescription(reader.GetString(2));
                                nickname = reader.GetString(3);
                                level = reader.GetInt32(4);
                                mana = reader.GetInt32(5);
                                stamina = reader.GetInt32(6);
                                coins = reader.GetInt32(7);
                                hp = reader.GetInt32(8);
                                damage = reader.GetInt32(9);
                                critdmg = reader.GetDouble(10);
                                critrate = reader.GetDouble(11);
                                protection = reader.GetInt32(12);
                            }
                            else
                            {
                                MessageBox.Show($"Character not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Character constructor: {e.Message}");
            }
        }

        static public void CreateCharacter(Character character)
        {
            string request = $"INSERT INTO public.\"character\" (player_id, class, nickname, level, mana, stamina, coins, hp, damage, critdmg, critrate, protection)\n" +
                 $"VALUES ({character.Player.PlayerId}, '{character.CharClass}', '{character.Nickname}', {character.Level}, {character.Mana}, {character.Stamina}, " +
                 $"{character.Coins}, {character.Hp}, {character.Damage}, {character.CritDmg.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
                 $"{character.CritRate.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {character.Protection})";
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    using (var cmd = new NpgsqlCommand(request, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Character created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to create character", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CreateCharacter: An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static public void DeleteCharacter(Character character)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    string sql = $"DELETE FROM character WHERE character_id = {character.character_id}";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Character deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete Character", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DeleteCharacter: An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static public void EditCharacter(Character characterPrimary, Character characterChanged)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    string sql = $"UPDATE public.\"character\" " +
             $"SET class = '{characterChanged.CharClass}', " +
             $"nickname = '{characterChanged.Nickname}', " +
             $"level = {characterChanged.Level}, " +
             $"mana = {characterChanged.Mana}, " +
             $"stamina = {characterChanged.Stamina}, " +
             $"coins = {characterChanged.Coins}, " +
             $"hp = {characterChanged.Hp}, " +
             $"damage = {characterChanged.Damage}, " +
             $"critdmg = {characterChanged.CritDmg.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
             $"critrate = {characterChanged.CritRate.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
             $"protection = {characterChanged.Protection} " +
             $"WHERE character_id = {characterPrimary.CharacterId};";


                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Character information updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update character information", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"EditCharacter: An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
