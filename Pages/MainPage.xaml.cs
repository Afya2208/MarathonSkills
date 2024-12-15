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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MoreInfoButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetailedMenuPage());
        }

        private void SponsorButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SponsorPage());
        }

        private void RunnerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CheckRunnerPage());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

    }
}
