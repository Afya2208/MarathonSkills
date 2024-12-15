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
    /// Логика взаимодействия для AdministrateUsersPage.xaml
    /// </summary>
    public partial class AdministrateUsersPage : Page
    {
        List<User> users;
        public AdministrateUsersPage()
        {
            InitializeComponent();
            users = DB.entities.User.ToList();
            UserList.ItemsSource = users;
            List<string> sorts = new List<string>()
            {
                "Имени",
                "Фамилии",
                "Email",
                "Роли"
            };
            SortComboBox.ItemsSource = sorts;
            RolesComboBox.ItemsSource = DB.entities.Role.ToList();
            if (users.Count == 0)
            {
                NoUsersBlock.Visibility = Visibility.Visible;
                UserList.Visibility = Visibility.Collapsed;

            }
            else 
            {
                NoUsersBlock.Visibility = Visibility.Collapsed;
                UserList.Visibility = Visibility.Visible;
            }
            TotalUsersBlock.Text = users.Count.ToString();
        }
        
        void Search(string text)
        {
            text = text.Trim();
            users = users.FindAll(u => u.FirstName.Contains(text)
            || u.LastName.Contains(text) || u.Email.Contains(text) ||
            u.FirstName.Contains(text.Split(' ').First()) && u.LastName.Contains(text.Split(' ').Last()) 
            || u.LastName.Contains(text.Split(' ').First()) && u.FirstName.Contains(text.Split(' ').Last()));

        }
        void Filter()
        {
            Role selectedRole = RolesComboBox.SelectedItem as Role;
            users = users.FindAll(u => u.RoleId == selectedRole.RoleId);
        }
        void Sort()
        {
            switch (SortComboBox.SelectedItem as string)
            {
                case "Имени":
                    users.OrderBy(u => u.FirstName);
                    break;
                case "Фамилии":
                    users.OrderBy(u => u.LastName);
                    break;
                case "Email":
                    users.OrderBy(u => u.Email);
                    break;
                case "Роли":
                    users.OrderBy(u => u.RoleId);
                    break;
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (RolesComboBox.SelectedItem == null || SortComboBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали сортировку и фильтрацию","Ошибка");
                return;
            }
            users = DB.entities.User.ToList();
            Filter();
            Sort();
            if (SearchBox.Text != null && SearchBox.Text != "") Search(SearchBox.Text);
            UserList.ItemsSource = users;
            TotalUsersBlock.Text = users.Count.ToString();
            if (users.Count == 0)
            {
                NoUsersBlock.Visibility = Visibility.Visible;
                UserList.Visibility = Visibility.Collapsed;

            }
            else
            {
                NoUsersBlock.Visibility = Visibility.Collapsed;
                UserList.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditUserPage(((FrameworkElement)sender).Tag as string));
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            users = DB.entities.User.ToList();
            UserList.ItemsSource = users;
            
        }
    }
}
