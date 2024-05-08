using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RPG_account_system
{
    public enum Category
    {
        [Description("Player")]
        Player,
        [Description("Admin")]
        Admin
    }

    public class Player : Request
    {
        private int player_id;
        private Category category;
        private int age;
        private DateTime registration_date;
        private string password;
        private string login;
        private string email;

        public int PlayerId
        { get { return player_id; } set { player_id = value; } }

        public Category Category
        { get { return category; } set { category = value; } }

        public int Age
        { get { return age; } set { age = value; } }

        public DateTime RegistrationDate
        { get { return registration_date; } set { registration_date = value; } }

        public string Password
        { get { return password; } set { password = value; } }

        public string Login
        { get { return login; } set { login = value; } }

        public string Email
        { get { return email; } set { email = value; } }

        public Player(string request)
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
                                player_id = reader.GetInt32(0);
                                category = EnumHelper.GetCategoryFromDescription(reader.GetString(1));
                                age = reader.GetInt32(2);
                                registration_date = reader.GetDateTime(3).Date;
                                password = reader.GetString(4);
                                this.login = reader.GetString(5);
                                email = reader.GetString(6);
                            }
                            else
                            {
                                MessageBox.Show($"Player not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public Player(Category category, int age, DateTime registration_date, string password, string login, string email)
        {
            this.category = category;
            this.age = age;
            this.registration_date = registration_date;
            this.password = password;
            this.login = login;
            this.email = email;
        }

        public Player() { }

        static public void CreatePlayer(Player player)
        {
            string request = $"INSERT INTO players (category, age, registration_date, password, login, email)\n" +
                $"VALUES ('{player.Category}', {player.Age}, '{player.RegistrationDate:yyyy-MM-dd}', '{player.Password}', '{player.Login}', '{player.Email}');";
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
                            MessageBox.Show("Player created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to create player", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static public void DeletePlayer(Player player)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    string sql = $"DELETE FROM players WHERE player_id = {player.PlayerId}";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Player deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete player", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static public void EditPlayer(Player playerPrimary, Player playerChanged)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    string sql = $"UPDATE players SET category = '{playerChanged.Category}', age = {playerChanged.Age}, " +
                        $"registration_date = '{playerChanged.RegistrationDate}', password = '{playerChanged.Password}', " +
                        $"login = '{playerChanged.login}', email = '{playerChanged.Email}' WHERE player_id = {playerPrimary.PlayerId}";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Player information updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update player information", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
