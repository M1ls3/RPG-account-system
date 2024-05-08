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
        public Characters(Player player)
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

        }

        private void button_Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
