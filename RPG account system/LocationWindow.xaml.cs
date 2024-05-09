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
    /// Interaction logic for LocationWindow.xaml
    /// </summary>
    public partial class LocationWindow : Window
    {
        Location location;

        public LocationWindow(Character character)
        {
            InitializeComponent();
            location = new Location(character);
            Load();
        }

        void Load()
        {
            labelNickname.Content = $"{location.Character.Nickname}'s location";
            textBox_x.Text = location.CoordinateX.ToString();
            textBox_y.Text = location.CoordinateY.ToString();
            textBox_z.Text = location.CoordinateZ.ToString();
            textBox_biomeID.Text = location.Biome.BiomeId.ToString();
        }

        private void button_biome_Click(object sender, RoutedEventArgs e)
        {
            BiomeWindow biomeWindow = new BiomeWindow(location);
            this.Close();
            biomeWindow.Show();
        }

        private void button_cansel_Click(object sender, RoutedEventArgs e)
        {
            Characters window = new Characters(location.Character.Player);
            this.Close();
            window.Show();
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(textBox_x.Text, out int x) &&
                Int32.TryParse(textBox_y.Text, out int y) && Int32.TryParse(textBox_z.Text, out int z) &&
                Int32.TryParse(textBox_biomeID.Text, out int biome))
            {
                Location.EditLocation(location, new Location(location.Character, new Biome($"SELECT * FROM biome WHERE biome_id = {biome}"), x, y, z));
                Characters window = new Characters(location.Character.Player);
                this.Close();
                window.Show();
            }
            else
            {
                MessageBox.Show("Some fields have invalid values. Please check and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
