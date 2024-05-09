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
    /// Interaction logic for CharacterEdit.xaml
    /// </summary>
    public partial class CharacterEdit : Window
    {
        bool isEdit = false;
        private Character character;
        private Player player;

        public CharacterEdit(Player player)
        {
            InitializeComponent();
            isEdit = false;
            this.player = player;
        }

        public CharacterEdit(Character character)
        {
            InitializeComponent();
            this.character = character;
            isEdit = true;
            Load();
        }

        void Load()
        {
            comboBoxClass.Text = character.CharClass.ToString();
            textBox_Nickname.Text = character.Nickname;
            textBox_level.Text = character.Level.ToString();
            textBox_mana.Text = character.Mana.ToString();
            textBox_stamina.Text = character.Stamina.ToString();
            textBox_coins.Text = character.Coins.ToString();
            textBox_hp.Text = character.Hp.ToString();
            textBox_damage.Text = character.Damage.ToString();
            textBox_critdmg.Text = character.CritDmg.ToString();
            textBox_critrate.Text = character.CritRate.ToString();
            textBox_protection.Text = character.Protection.ToString();
        }

        private void button_cansel_Click(object sender, RoutedEventArgs e)
        {
            Characters CharWindow = new Characters(character.Player);
            this.Close();
            CharWindow.Show();
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxClass.SelectedItem != null && Int32.TryParse(textBox_level.Text, out int level) &&
                Int32.TryParse(textBox_mana.Text, out int mana) && Int32.TryParse(textBox_stamina.Text, out int stamina) &&
                Int32.TryParse(textBox_coins.Text, out int coins) && Int32.TryParse(textBox_hp.Text, out int hp) &&
                Int32.TryParse(textBox_damage.Text, out int damage) && Double.TryParse(textBox_critdmg.Text, out double critdmg) &&
                Double.TryParse(textBox_critrate.Text, out double critrate) && Int32.TryParse(textBox_protection.Text, out int protection))
            {
                if (isEdit)
                {
                    Character.EditCharacter(character, new Character(character.Player, EnumHelper.GetClassFromDescription(comboBoxClass.Text),
                    textBox_Nickname.Text, level, mana, stamina, coins, hp, damage, critdmg, critrate,
                    protection));
                    Characters CharWindow = new Characters(character.Player);
                    this.Close();
                    CharWindow.Show();
                }
                else if (!isEdit)
                {
                    Character.CreateCharacter(new Character(player, EnumHelper.GetClassFromDescription(comboBoxClass.Text),
                    textBox_Nickname.Text, level, mana, stamina, coins, hp, damage, critdmg, critrate,
                    protection));
                    Characters CharWindow = new Characters(player);
                    this.Close();
                    CharWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Some fields have invalid values. Please check and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
