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
    /// Логика взаимодействия для RunnerResultsPage.xaml
    /// </summary>
    public partial class RunnerResultsPage : Page
    {
        string ageCategory, gender;
        Runner runner;
        MainWindow mainWindow;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllResultsPage());
        }

        public RunnerResultsPage(Runner run)
        {
            InitializeComponent();
            this.runner = run;
            int diff = (DateTime.Today - runner.DateOfBirth).Value.Days/365;

            if (diff < 18)
            {
                ageCategory = "до 18";
            }
            else if (diff <= 29)
            {
                ageCategory = "от 18 до 29";
            }
            else if (diff <= 39)
            {
                ageCategory = "от 30 до 39";
            }
            else if (diff <= 55)
            {
                ageCategory = "от 40 до 55";
            }
            else if (diff <= 70)
            {
                ageCategory = "от 56 до 70";
            }
            else 
            {
                ageCategory = "от 70";
            }
            gender = runner.Gender == "Male" ? "мужской" : "женский";
            List<Registration> runnerRegis = DB.entities.Registration.Where(reg => reg.RunnerId == runner.RunnerId && reg.RegistrationStatusId == 4).ToList();
            List<RegistrationEvent> runnerRaces = new List<RegistrationEvent>();
            foreach (Registration r in runnerRegis) 
            {
                runnerRaces.AddRange(DB.entities.RegistrationEvent.Where(a => a.RegistrationId == r.RegistrationId && a.RaceTime != null).ToList());
            }
            ResultView.ItemsSource = runnerRaces;
            if (runnerRaces.Count<1)
            {
                NoRacesBlock.Visibility = Visibility.Visible;
                ResultView.Visibility = Visibility.Collapsed;
            }
        }
    }
}
