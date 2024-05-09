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
            PlayerEdit edit = new PlayerEdit(true);
            this.Close();
            edit.Show();
        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dataGrid_Players.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                PlayerEdit edit = new PlayerEdit(FindPlayer(selectedRow));
                this.Close();
                edit.Show();
            }
            else
            {
                MessageBox.Show($"Please select a player", "Selection");
            }
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete character?", "Delete character", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                var selectedRow = dataGrid_Players.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    Player.DeletePlayer(FindPlayer(selectedRow));
                    RequestSQL();
                }
                else
                {
                    MessageBox.Show($"Please select a player", "Selection");
                }
            }
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dataGrid_Players.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                Characters CharWindow = new Characters(FindPlayer(selectedRow));
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
                Statistics statistics = new Statistics(FindPlayer(selectedRow));
                this.Close();
                statistics.Show();
            }
            else
            {
                MessageBox.Show($"Please select a player", "Selection");
            }
        }

        private Player FindPlayer(DataRowView selectedRow)
        {
            int playerId = Convert.ToInt32(selectedRow.Row.ItemArray[0]);

            string request = $"SELECT * FROM players WHERE player_id = {playerId}";
            return new Player(request);
        }
    }
}
