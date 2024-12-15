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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string password, login;
            if (LoginBox.Text == "" || PasswordBox.Password == "")
            {
                MessageBox.Show("Вы оставили пустое поле, все поля обязательны к заполнению", "Ошибка");
                return;
            }
            password = PasswordBox.Password;
            login = LoginBox.Text;
            User user = DB.entities.User.FirstOrDefault(u => u.Password == password && u.Email == login);
            if (user == null)
            {
                MessageBox.Show("Неправильный пароль или логин", "Ошибка");
                return;
            }
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Logout.Visibility = Visibility.Visible;
            mainWindow.user = user;
            switch (user.RoleId)
            {
                case "R":
                    NavigationService.Navigate(new RunnerMenuPage());
                    break;
                case "C":
                    NavigationService.Navigate(new CoordinatorPage());
                    break;
                case "A":
                    NavigationService.Navigate(new AdministratorPage());
                    break;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
