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
    /// Логика взаимодействия для RunnerMenuPage.xaml
    /// </summary>
    public partial class RunnerMenuPage : Page
    {
        public RunnerMenuPage()
        {
            InitializeComponent();
        }

        private void ResultsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            Runner runner = DB.entities.Runner.FirstOrDefault(r => r.Email == mainWindow.user.Email);
            NavigationService.Navigate(new RunnerResultsPage(runner));
        }

        private void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
        }
        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = false;
        }
        private void RegOnMarathon_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationOnMarathonPage());
        }

        private void EditAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            NavigationService.Navigate(new EditAccountPage(mainWindow.user));
        }

        private void SposorButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RunnersSponsor());
        }
    }
}
