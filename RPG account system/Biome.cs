using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using System.Windows;

namespace RPG_account_system
{
    public class Biome : Request
    {
        private int biome_id;
        private string biome_name;
        private double treasure_scale;
        private double coin_scale;
        private double enemy_scale;
        private double exp_scale;

        public int BiomeId { get { return biome_id; } set { biome_id = value; } }
        public string BiomeName { get { return biome_name; } set { biome_name = value; } }
        public double TreasureScale { get { return treasure_scale; } set { treasure_scale = value; } }
        public double CoinScale { get { return coin_scale; } set { coin_scale = value; } }
        public double EnemyScale { get { return enemy_scale; } set { enemy_scale = value; } }
        public double ExpScale { get { return exp_scale; } set { exp_scale = value; } }

        public Biome(string biome_name, double treasure_scale, double coin_scale, double enemy_scale, double exp_scale)
        {
            this.biome_name = biome_name;
            this.treasure_scale = treasure_scale;
            this.coin_scale = coin_scale;
            this.enemy_scale = enemy_scale;
            this.exp_scale = exp_scale;
        }

        public Biome(string request)
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
                                biome_id = reader.GetInt32(0);
                                biome_name = reader.GetString(1);
                                treasure_scale = reader.GetDouble(2);
                                coin_scale = reader.GetDouble(3);
                                enemy_scale = reader.GetDouble(4);
                                exp_scale = reader.GetDouble(5);
                            }
                            else
                            {
                                MessageBox.Show($"Biome not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Biome constructor: {e.Message}");
            }
        }

        static public void CreateBiome(Biome biome)
        {
            string request = $"INSERT INTO public.biome (biome, treasure_scale, coin_scale, enemy_scale, exp_scale) " +
                             $"VALUES ('{biome.BiomeName}', " +
                             $"{biome.TreasureScale.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
                             $"{biome.CoinScale.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
                             $"{biome.EnemyScale.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
                             $"{biome.ExpScale.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
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
                            MessageBox.Show("Biome created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to create biome", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CreateBiome: An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static public void DeleteBiome(Biome biome)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    string sql = $"DELETE FROM biome WHERE biome_id = {biome.BiomeId}";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Biome deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete Biome", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DeleteBiome: An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        static public void EditBiome(Biome biomePrimary, Biome biomeChanged)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    string sql = $"UPDATE biome " +
                 $"SET biome = '{biomeChanged.BiomeName}', " +
                 $"treasure_scale = {biomeChanged.TreasureScale.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
                 $"coin_scale = {biomeChanged.CoinScale.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
                 $"enemy_scale = {biomeChanged.EnemyScale.ToString(System.Globalization.CultureInfo.InvariantCulture)}, " +
                 $"exp_scale = {biomeChanged.ExpScale.ToString(System.Globalization.CultureInfo.InvariantCulture)} " +
                 $"WHERE biome_id = {biomePrimary.BiomeId};";


                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Biome information updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update biome information", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"EditBiome: An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


