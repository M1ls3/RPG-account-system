using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        Statistic statistic;
        public Statistics(Player player)
        {
            InitializeComponent();
            statistic = new Statistic(player);
            Load();
        }

        void Load()
        {
            labelStatisticPlayer.Content = $"{statistic.Player.Login}'s statistic";
            textBox_total_losses.Text = statistic.TotalLosses.ToString();
            textBox_total_kills.Text = statistic.TotalKills.ToString();
            textBox_treasure_collected.Text = statistic.TreasureCollected.ToString();
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(textBox_total_losses.Text, out int losses) &&
                Int32.TryParse(textBox_total_kills.Text, out int kills) && Int32.TryParse(textBox_treasure_collected.Text, out int treasure))
            {
                Statistic.EditStatistic(statistic, new Statistic(losses, kills, treasure));
                Players pWindow = new Players();
                this.Close();
                pWindow.Show();
            }
            else
            {
                MessageBox.Show("Some fields have invalid values. Please check and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_cansel_Click(object sender, RoutedEventArgs e)
        {
            Players players = new Players();
            this.Close();
            players.Show();
        }
    }
}
