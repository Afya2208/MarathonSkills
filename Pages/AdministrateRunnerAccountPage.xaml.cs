using MarathonSkills.Database;
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
    /// Логика взаимодействия для AdministrateRunnerAccountPage.xaml
    /// </summary>
    public partial class AdministrateRunnerAccountPage : Page
    {
        Runner runner;
        Registration registration;
        public AdministrateRunnerAccountPage(int regId)
        {
            InitializeComponent();
            registration = DB.entities.Registration.FirstOrDefault(reg => reg.RegistrationId == regId);
            runner = DB.entities.Runner.FirstOrDefault(r => r.RunnerId == registration.RunnerId);
            DataContext = registration;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            switch(registration.RegistrationStatusId)
            {
                case 1:
                    status1Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    status2Image.Source = new BitmapImage(new Uri("Resources\\cross-icon.png"));
                    status3Image.Source = new BitmapImage(new Uri("Resources\\cross-icon.png"));
                    status4Image.Source = new BitmapImage(new Uri("Resources\\cross-icon.png"));
                    break;
                case 2:
                    status1Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    status2Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    status3Image.Source = new BitmapImage(new Uri("Resources\\cross-icon.png"));
                    status4Image.Source = new BitmapImage(new Uri("Resources\\cross-icon.png"));
                    break;
                case 3:
                    status1Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    status2Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    status3Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    status4Image.Source = new BitmapImage(new Uri("Resources\\cross-icon.png"));
                    break;
                case 4:
                    status1Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    status2Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    status3Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    status4Image.Source = new BitmapImage(new Uri("Resources\\tick-icon.png"));
                    break;
            }
        }

        private void RedactButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditAccountPage(runner.User));
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
