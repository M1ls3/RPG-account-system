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
        public Statistics(Player player)
        {
            InitializeComponent();
            RequestSQL(player);
        }

        void RequestSQL(Player player)
        {
            try
            {
                using (NpgsqlConnection con = Request.GetConnection())
                {
                    con.Open();

                    string sql = $"SELECT * FROM player_statistics WHERE player_id = {player.PlayerId} \nORDER BY statistics_id";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                dataGrid_Statistics.ItemsSource = dt.DefaultView;
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
            Players players = new Players();
            this.Close();
            players.Show();
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
