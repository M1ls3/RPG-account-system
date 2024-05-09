using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace RPG_account_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player player;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Play_Click(object sender, RoutedEventArgs e)
        {
            player = new Player($"SELECT * FROM players WHERE login = '{textBox_Login.Text}'");
            if (player.Password == passwordBox.Password)
            {
                if (player.Category == Category.Admin)
                {
                    Players charactersMenu = new Players();
                    this.Close();
                    charactersMenu.Show();
                }
                else
                    MessageBox.Show($"You don't have permission to do this", "Permission", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show($"Incorrect Login or Password", "Invalid", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void button_Register_Click(object sender, RoutedEventArgs e)
        {
            PlayerEdit edit = new PlayerEdit(false);
            this.Close();
            edit.Show();
        }
    }
}
