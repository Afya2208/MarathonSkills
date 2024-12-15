using MarathonSkills.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для RedactRunnerPage.xaml
    /// </summary>
    public partial class RedactRunnerPage : Page
    {
        User _user;
        Runner _runner;
        Registration _registration;
        public RedactRunnerPage(Registration registration)
        {
            InitializeComponent();
            StatusComboBox.ItemsSource = DB.entities.RegistrationStatus.ToList();
            CountiesComboBox.ItemsSource = DB.entities.Country.ToList();
            GenderComboBox.ItemsSource = DB.entities.Gender.ToList();
            StatusComboBox.SelectedItem = registration.RegistrationStatus;
            _runner = registration.Runner;
            _user = _runner.User;
            NameBox.Text = _user.FirstName;
            _registration = registration;
            LastNameBox.Text = _user.LastName;
            DataContext = _runner;
            datePicker1.SelectedDate = _runner.DateOfBirth;
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name, lastname, password, repeatPassword;
            DateTime? dateOfBirthday = datePicker1.SelectedDate;

            if (GenderComboBox.SelectedItem == null || CountiesComboBox.SelectedValue == null || NameBox.Text == "" ||
                LastNameBox.Text == "" || datePicker1.Text == "" || FileNameBox.Text == "")
            {
                MessageBox.Show("Одно из полей точно пустое, все поля должны быть заполнены", "Ошибка");
                return;
            }
            name = NameBox.Text;
            lastname = LastNameBox.Text;
            password = PasswordBox.Text;
            if (PasswordBox.Text != "")
            {
                if (!CheckPassword(password))
                {
                    MessageBox.Show("Неправильный формат пароля. В пароле должно быть как минимум 6 символов, 1 заглавная буква, 1 цифра и 1 специальный символ из этого списка: !, @, #, $, %, ^", "Ошибка");
                    return;
                }
                else
                {
                    repeatPassword = RepeatPasswordBox.Text;
                    if (password != repeatPassword)
                    {
                        MessageBox.Show("Вы неправильно повторили пароль", "Ошибка");
                        return;
                    }
                }
            }


            TimeSpan? diff = DateTime.Today - dateOfBirthday;
            if (dateOfBirthday == null || diff == null)
            {
                MessageBox.Show("Не выбрана дата", "Ошибка");
                return;
            }
            else if (diff.Value.Days / 365 < 10)
            {
                MessageBox.Show("Возраст бегуна слишком маленький, возраст должен быть от 10 лет", "Ошибка");
                return;
            }


            if (password != "")
            {
                _user.Password = password;
            }
            _user.FirstName = name;
            _user.LastName = lastname;
            _runner.DateOfBirth = dateOfBirthday;
            _runner.Gender = (GenderComboBox.SelectedItem as Gender).Gender1;
            _runner.Photo = File.ReadAllBytes(FileNameBox.Text);
            _runner.CountryCode = (CountiesComboBox.SelectedItem as Country).CountryCode;
           _registration.RegistrationStatusId = (StatusComboBox.SelectedItem as RegistrationStatus).RegistrationStatusId;
            DB.entities.SaveChanges();
            NavigationService.GoBack();
        }
    }
}
