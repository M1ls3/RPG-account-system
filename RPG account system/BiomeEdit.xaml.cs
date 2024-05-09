using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for BiomeEdit.xaml
    /// </summary>
    public partial class BiomeEdit : Window
    {
        bool isEdit = false;
        Biome biome;
        Location location;

        public BiomeEdit(Location location)
        {
            InitializeComponent();
            this.location = location;
            isEdit = false;
        }

        public BiomeEdit(Biome biome, Location location)
        {
            InitializeComponent();
            this.biome = biome;
            this.location = location;
            isEdit = true;
            Load();
        }

        void Load()
        {
            textBox_biome_name.Text = biome.BiomeName;
            textBox_treasule_scale.Text = biome.TreasureScale.ToString();
            textBox_coin_scale.Text = biome.CoinScale.ToString();
            textBox_enemy_scale.Text = biome.EnemyScale.ToString();
            textBox_exp_scale.Text = biome.ExpScale.ToString();
        }

        private void button_cansel_Click(object sender, RoutedEventArgs e)
        {
            BiomeWindow biomeWindow = new BiomeWindow(location);
            this.Close();
            biomeWindow.Show();
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {

            if (Double.TryParse(textBox_treasule_scale.Text, out double treasure) && Double.TryParse(textBox_coin_scale.Text, out double coin) &&
                Double.TryParse(textBox_enemy_scale.Text, out double enemy) && Double.TryParse(textBox_exp_scale.Text, out double exp))
            {
                if (isEdit)
                {
                    Biome.EditBiome(biome, new Biome(textBox_biome_name.Text, treasure, coin, enemy, exp));
                }
                else if (!isEdit)
                {
                    Biome.CreateBiome(new Biome(textBox_biome_name.Text, treasure, coin, enemy, exp));
                }
                BiomeWindow biomeWindow = new BiomeWindow(location);
                this.Close();
                biomeWindow.Show();
            }
            else
            {
                MessageBox.Show("Some fields have invalid values. Please check and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
