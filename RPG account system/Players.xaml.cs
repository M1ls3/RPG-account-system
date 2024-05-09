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
    /// Interaction logic for Players.xaml
    /// </summary>
    public partial class Players : Window
    {
        public Players()
        {
            InitializeComponent();
            RequestSQL();
        }

        void RequestSQL()
        {
            try
            {
                using (NpgsqlConnection con = Request.GetConnection())
                {
                    con.Open();

                    string sql = "SELECT * FROM players \nORDER BY player_id";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                dataGrid_Players.ItemsSource = dt.DefaultView;
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
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dataGrid_Players.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                int playerId = Convert.ToInt32(selectedRow.Row.ItemArray[0]);

                string request = $"SELECT * FROM players WHERE player_id = {playerId}";
                Characters CharWindow = new Characters(new Player(request));
                this.Close();
                CharWindow.Show();
            }
            else
            {
                MessageBox.Show($"Please select a player", "Selection");
            }
        }




        private void button_Statistic_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dataGrid_Players.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                int playerId = Convert.ToInt32(selectedRow.Row.ItemArray[0]);

                string request = $"SELECT * FROM players WHERE player_id = {playerId}";
                Statistics statistics = new Statistics(new Player(request));
                this.Close();
                statistics.Show();
            }
            else
                MessageBox.Show($"Please select a player", "Selection");
        }
    }
}
