using MarathonSkills.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationRunnerPage.xaml
    /// </summary>
    public partial class RegistrationRunnerPage : Page
    {
        MainWindow mainWindow;
        public RegistrationRunnerPage()
        {
            InitializeComponent();
            CountiesComboBox.ItemsSource = DB.entities.Country.ToList();
            GenderComboBox.ItemsSource= DB.entities.Gender.ToList();
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
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
        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string name, lastname, email, password, repeatPassword;
            DateTime? dateOfBirthday = datePicker1.SelectedDate;
            
            if (GenderComboBox.SelectedItem == null || CountiesComboBox.SelectedValue == null || NameBox.Text == "" || PasswordBox.Text == "" ||
                LastNameBox.Text == "" || RepeatPasswordBox.Text == "" || datePicker1.Text == "" || FileNameBox.Text == "")
            {
                MessageBox.Show("Одно из полей точно пустое, все поля должны быть заполнены", "Ошибка");
                return;
            }
            name = NameBox.Text;
            lastname = LastNameBox.Text;
            password = PasswordBox.Text;
            if (!CheckPassword(password))
            {
                MessageBox.Show("Неправильный формат пароля. В пароле должно быть как минимум 6 символов, 1 заглавная буква, 1 цифра и 1 специальный символ из этого списка: !, @, #, $, %, ^", "Ошибка");
                return;
            }
            repeatPassword = RepeatPasswordBox.Text;
            if (password != repeatPassword)
            {
                MessageBox.Show("Вы неправильно повторили пароль", "Ошибка");
                return;
            }

            const string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            email = EmailBox.Text.Trim().ToLowerInvariant();

            if (!Regex.Match(email, pattern).Success)
            {
                MessageBox.Show("Почта не соответствует формату x@x.x", "Ошибка");
                return;
            }
            if (DB.entities.User.FirstOrDefault(u => u.Email == email) != null)
            {
                MessageBox.Show("Данная почта уже используется", "Ошибка");
                return;
            }
            TimeSpan? diff = DateTime.Today - dateOfBirthday;
            if (dateOfBirthday == null || diff == null)
            {
                MessageBox.Show("Не выбрана дата", "Ошибка");
                return;
            } else if (diff.Value.Days / 365 < 10)
            {
                MessageBox.Show("Возраст бегуна слишком маленький, возраст должен быть от 10 лет", "Ошибка");
                return;
            }
            

            User user = new User{ Email = email, FirstName = name, LastName = lastname, RoleId = "R", Password = password };
            Runner runner = new Runner { Email = user.Email, CountryCode = (CountiesComboBox.SelectedItem as Country).CountryCode, DateOfBirth = dateOfBirthday, 
                Gender = (GenderComboBox.SelectedItem as Gender).Gender1, Photo = File.ReadAllBytes(FileNameBox.Text)};
            DB.entities.User.Add(user);
            DB.entities.Runner.Add(runner);
            DB.entities.SaveChanges();
            mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Logout.Visibility = Visibility.Visible;
            mainWindow.user = user;
            NavigationService.Navigate(new RegistrationOnMarathonPage());
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Картинки|*.jpg;*.jpeg;*.png";
            openFileDialog.ShowDialog();
            string imageFile = openFileDialog.FileName;
            if (File.Exists(imageFile))
            {
                ImageBox.Source = new BitmapImage(new Uri(imageFile));

            }
            FileNameBox.Text = imageFile;
        }
    }
}
