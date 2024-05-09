using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RPG_account_system
{
    /// <summary>
    /// Interaction logic for Characters.xaml
    /// </summary>
    public partial class Characters : Window
    {
        Player player;
        public Characters(Player player)
        {
            InitializeComponent();
            this.player = player;
            RequestSQL();
        }

        void RequestSQL()
        {
            try
            {
                using (NpgsqlConnection con = Request.GetConnection())
                {
                    con.Open();

                    string sql = $"SELECT * FROM character WHERE player_id = {player.PlayerId} \nORDER BY character_id";
                    using (var cmd = new NpgsqlCommand(sql, con))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                dataGrid_Characters.ItemsSource = dt.DefaultView;
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
            Players PlWindow = new Players();
            this.Close();
            PlWindow.Show();
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            CharacterEdit characterEdit = new CharacterEdit(player);
            this.Close();
            characterEdit.Show();
        }

            private void button_Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dataGrid_Characters.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                int characterId = Convert.ToInt32(selectedRow.Row.ItemArray[0]);

                string request = $"SELECT * FROM character WHERE character_id = {characterId}";
                CharacterEdit characterEdit = new CharacterEdit(new Character(request));
                this.Close();
                characterEdit.Show();
            }
            else
                MessageBox.Show($"Incorrect character_id", "Selection");
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete character?", "Delete character", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                var selectedRow = dataGrid_Characters.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    int characterId = Convert.ToInt32(selectedRow.Row.ItemArray[0]);

                    string request = $"SELECT * FROM character WHERE character_id = {characterId}";
                    Character.DeleteCharacter(new Character(request));
                    RequestSQL();
                }
            }
        }

        private void button_location_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dataGrid_Characters.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                int characterId = Convert.ToInt32(selectedRow.Row.ItemArray[0]);

                string request = $"SELECT * FROM character WHERE character_id = {characterId}";
                LocationWindow locationWindow = new LocationWindow(new Character(request));
                this.Close();
                locationWindow.Show();
            }
            else
                MessageBox.Show($"Incorrect character_id", "Selection");
        }
    }
}
