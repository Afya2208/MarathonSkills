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
    /// Логика взаимодействия для DetailedMenuPage.xaml
    /// </summary>
    public partial class DetailedMenuPage : Page
    {
        public DetailedMenuPage()
        {
            InitializeComponent();

        }

        private void AboutMarathonSkillsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AboutMarathonPage());
        }

        private void PreviousResultsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllResultsPage());
        }

        private void BMICalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BMICalulatorPage());
        }

        private void HowLongMarathonButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HowLongIsMarathon());
        }

        private void ListOfCharityButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CharitiesPage());
        }

        private void BMRCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BMRCalculatorPage());
        }
    }
}
