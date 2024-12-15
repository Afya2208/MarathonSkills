using MarathonSkills.Database;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarathonSkills.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        User user;
        public EditUserPage(string email)
        {
            InitializeComponent();
            user = DB.entities.User.FirstOrDefault(x => x.Email == email);
            DataContext = user;
            RoleComboBox.ItemsSource = DB.entities.Role.ToList();
        }
        private bool CheckPassword(string password)
        {
            bool hasOneDigit = false, hasMinSixSymbols = false, hasSpecialSymbol = false, hasOneBigLetter = false, hasSpaces = false;
            foreach (char symb in password)
            {
                if (symb == ' ')
                {
                    hasSpaces = true;
                    break;
                }
                if (!hasOneDigit && Char.IsDigit(symb))
                {
                    hasOneDigit = true;
                }
                if (!hasSpecialSymbol && (symb == '^' || symb == '$' || symb == '@' || symb == '%' || symb == '#' || symb == '!'))
                {
                    hasSpecialSymbol = true;
                }
                if (!hasOneBigLetter && (Char.IsLetter(symb) && (Char.IsUpper(symb))))
                {
                    hasOneBigLetter = true;
                }
            }
            if (password.Length > 5) hasMinSixSymbols = true;

            if (hasSpaces || !hasOneDigit || !hasOneBigLetter || !hasMinSixSymbols || !hasSpecialSymbol)
            {
                return false;
            }
            return true;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNameBox.Text == "" || LastNameBox.Text == "" || RoleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Вы оставили пустое поле для обязательных значений: имя, фамилия и роль","Ошибка");
                return;
            }
            string password;
            if (PasswordBox.Text != "")
            {
                password = PasswordBox.Text;
                if (!CheckPassword(password))
                {
                    MessageBox.Show("Неправильный формат пароля. В пароле должно быть как минимум 6 символов, 1 заглавная буква, 1 цифра и 1 специальный символ из этого списка: !, @, #, $, %, ^", "Ошибка");
                    return;
                }
                if (RepeatPasswordBox.Text != password)
                {
                    MessageBox.Show("Вы не правильно повторили пароль", "Ошибка");
                    return;
                }
                user.Password = password;
            }
            try
            {
                DB.entities.SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
