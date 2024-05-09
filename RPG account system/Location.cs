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
    public class Location : Request
    {
        private int location_id;
        private Character character;
        private Biome biome;
        private int coordinate_x;
        private int coordinate_y;
        private int coordinate_z;

        public int LocationID { get { return location_id; } set { location_id = value; } }
        public Character Character { get { return character; } set { character = value; } }
        public Biome Biome { get { return biome; } set {  biome = value; } }
        public int CoordinateX { get { return coordinate_x; } set { coordinate_x = value; } }
        public int CoordinateY { get { return coordinate_y; } set { coordinate_y = value; } }
        public int CoordinateZ { get { return coordinate_z; } set { coordinate_z = value; } }

        public Location(Character character)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();

                    string sql = $"SELECT * FROM location WHERE character_id = {character.CharacterId}";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                location_id = reader.GetInt32(0);
                                this.character = character;
                                biome = new Biome($"SELECT * FROM biome WHERE biome_id = {reader.GetInt32(2)}");
                                coordinate_x = reader.GetInt32(3);
                                coordinate_y = reader.GetInt32(4);
                                coordinate_z = reader.GetInt32(5);
                            }
                            else
                            {
                                MessageBox.Show($"Location not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Location constructor: {e.Message}");
            }
        }

        public Location(Character character, Biome biome, int coordinate_x, int coordinate_y, int coordinate_z)
        {
            this.character = character;
            this.biome = biome;
            this.coordinate_x = coordinate_x;
            this.coordinate_y = coordinate_y;
            this.coordinate_z = coordinate_z;
        }

        static public void EditLocation(Location locationPrimary, Location locationChanged)
        {
            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    con.Open();
                    string sql = $"UPDATE public.location " +
             $"SET " +
             $"biome_id = '{locationChanged.Biome.BiomeId}', " +
             $"coordinate_x = {locationChanged.CoordinateX}, " +
             $"coordinate_y = {locationChanged.CoordinateY}, " +
             $"coordinate_z = {locationChanged.CoordinateZ} " +
             $"WHERE character_id = {locationPrimary.Character.CharacterId} ";


                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Location information updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update location information", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Location: An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}