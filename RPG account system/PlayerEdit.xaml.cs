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
    /// Interaction logic for PlayerEdit.xaml
    /// </summary>
    public partial class PlayerEdit : Window
    {
        bool isEdit = false;
        Player player;
        bool adminAccess;

        public PlayerEdit(bool adminAccess)
        {
            InitializeComponent();
            isEdit = false;
            this.adminAccess = adminAccess;

            if (!adminAccess)
            {
                //comboBoxCategory.Text = Category.Player.ToString();
                comboBoxCategory.SelectedIndex = 0;
                textBox_reg_date.Text = DateTime.Now.ToShortDateString();
                comboBoxCategory.IsReadOnly = true;
                comboBoxCategory.IsEnabled = false;
                textBox_reg_date.IsReadOnly = true;
            }
            else
            {
                comboBoxCategory.SelectedIndex = 0;
                textBox_reg_date.Text = DateTime.Now.ToShortDateString();
            }
        }

        public PlayerEdit(Player player)
        {
            InitializeComponent();
            isEdit = true;
            this.player = player;
            adminAccess = true;
            Load();
        }

        void Load()
        {
            comboBoxCategory.Text = player.Category.ToString();
            textBox_login.Text = player.Login.ToString();
            passwordBox.Password = player.Password.ToString();
            passwordBox_Copy.Password = player.Password.ToString();
            textBox_Age.Text = player.Age.ToString();
            textBox_email.Text = player.Email.ToString();
            textBox_reg_date.Text = player.RegistrationDate.ToShortDateString();
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputValid())
            {
                if (passwordBox.Password == passwordBox_Copy.Password)
                {
                    if (isEdit)
                    {
                        Player.EditPlayer(player, CreateNewPlayerFromInput());
                        Players pWindow = new Players();
                        this.Close();
                        pWindow.Show();
                    }
                    else
                    {
                        Player.CreatePlayer(CreateNewPlayerFromInput());
                        if (adminAccess)
                        {
                            Players pWindow = new Players();
                            this.Close();
                            pWindow.Show();
                        }
                        else
                        {
                            MainWindow mainWindow = new MainWindow();
                            this.Close();
                            mainWindow.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Confirm the Password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_cansel_Click(object sender, RoutedEventArgs e)
        {
            if (adminAccess)
            {
                Players pWindow = new Players();
                this.Close();
                pWindow.Show();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
        }

        private bool IsInputValid()
        {
            if (comboBoxCategory.SelectedItem == null)
            {
                MessageBox.Show("Select a Category.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Int32.TryParse(textBox_Age.Text, out int age) || age <= 0 || age >= 150)
            {
                MessageBox.Show("Enter a valid Age (between 1 and 150).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!DateTime.TryParse(textBox_reg_date.Text, out DateTime date) || date > DateTime.Now)
            {
                MessageBox.Show("Enter a valid Registration Date (not in the future).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!textBox_email.Text.Contains('@'))
            {
                MessageBox.Show("Enter a valid Email Address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private Player CreateNewPlayerFromInput()
        {
            Category category = EnumHelper.GetCategoryFromDescription(comboBoxCategory.Text);
            int age = Int32.Parse(textBox_Age.Text);
            DateTime registrationDate = DateTime.Parse(textBox_reg_date.Text);
            string password = passwordBox.Password;
            string login = textBox_login.Text;
            string email = textBox_email.Text;

            return new Player(category, age, registrationDate, password, login, email);
        }
    }
}
