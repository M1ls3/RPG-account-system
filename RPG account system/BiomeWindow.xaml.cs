using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RPG_account_system
{
    /// <summary>
    /// Interaction logic for BiomeWindow.xaml
    /// </summary>
    public partial class BiomeWindow : Window
    {
        Location location;
        public BiomeWindow(Location location)
        {
            InitializeComponent();
            this.location = location;
            RequestSQL();
        }

        void RequestSQL()
        {
            try
            {
                using (NpgsqlConnection con = Request.GetConnection())
                {
                    con.Open();

                    string sql = $"SELECT * FROM biome \nORDER BY biome_id";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                dataGrid_Biomes.ItemsSource = dt.DefaultView;
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

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            LocationWindow locationWindow = new LocationWindow(location.Character);
            this.Close();
            locationWindow.Show();
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            BiomeEdit biomeEdit = new BiomeEdit(location);
            this.Close();
            biomeEdit.Show();
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dataGrid_Biomes.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                int biomeId = Convert.ToInt32(selectedRow.Row.ItemArray[0]);

                string request = $"SELECT * FROM biome WHERE biome_id = {biomeId}";
                BiomeEdit biomeEdit = new BiomeEdit(new Biome(request), location);
                this.Close();
                biomeEdit.Show();
            }
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete biome?", "Delete biome", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                var selectedRow = dataGrid_Biomes.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    int biomeId = Convert.ToInt32(selectedRow.Row.ItemArray[0]);

                    string request = $"SELECT * FROM biome WHERE biome_id = {biomeId}";
                    Biome.DeleteBiome(new Biome(request));
                    RequestSQL();
                }
            }
        }
    }
}
