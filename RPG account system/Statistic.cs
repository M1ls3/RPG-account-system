using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RPG_account_system
{
    public class Statistic : Request
    {
        private int statistic_id;
        private Player player;
        private int total_losses;
        private int total_kills;
        private int treasure_collected;

        public int StatisticId { get { return statistic_id; } set { statistic_id = value; } }
        public Player Player { get { return player; } set { player = value; } }
        public int TotalLosses { get { return total_losses; } set { total_losses = value; } }
        public int TotalKills { get {  return total_kills; } set { total_kills = value; } }
        public int TreasureCollected { get { return treasure_collected; } set { treasure_collected = value; } }

        public Statistic(int total_losses, int total_kills, int treasure_collected)
        {
            this.total_losses = total_losses;
            this.total_kills = total_kills;
            this.treasure_collected = treasure_collected;
        }

        public Statistic(Player player)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();

                    string sql = $"SELECT * FROM player_statistics WHERE player_id = {player.PlayerId}";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                statistic_id = reader.GetInt32(0);
                                this.player = new Player($"SELECT * FROM players WHERE player_id = {reader.GetInt32(1)}");
                                total_losses = reader.GetInt32(2);
                                total_kills = reader.GetInt32(3);
                                treasure_collected = reader.GetInt32(4);
                            }
                            else
                            {
                                MessageBox.Show($"Statistic not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Statistic constructor: {e.Message}");
            }
        }

        static public void EditStatistic(Statistic statisticPrimary, Statistic statisticChanged)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    string sql = $"UPDATE player_statistics " +
             $"SET " +
             $"total_losses = {statisticChanged.TotalLosses}, " +
             $"total_kills = {statisticChanged.TotalKills}, " +
             $"treasure_collected = {statisticChanged.TreasureCollected} " +
             $"WHERE statistics_id = {statisticPrimary.StatisticId} ";


                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Statistic information updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update statistic information", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Statistic: An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
