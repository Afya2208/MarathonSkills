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
    /// Логика взаимодействия для RunnersSponsor.xaml
    /// </summary>
    public partial class RunnersSponsor : Page
    {
        public RunnersSponsor()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            User user = mainWindow.user;
            Runner runner = DB.entities.Runner.FirstOrDefault(r => r.Email == user.Email);
            Marathon marathon = DB.entities.Marathon.OrderByDescending(m => m.YearHeld).ToList().First();

            // проверка на null, может регистрации нет
            Registration registration = DB.entities.Registration.FirstOrDefault(reg => reg.RunnerId == runner.RunnerId &&
            reg.RegistrationStatusId != 4 && reg.RegistrationDateTime.Year == marathon.YearHeld);
            if (registration != null)
            {
                Sponsors.ItemsSource = DB.entities.Sponsorship.Where(sp => sp.RegistrationId == registration.RegistrationId).ToList();
                Charity charity = DB.entities.Charity.FirstOrDefault(c => c.CharityId == registration.CharityId);
                NameBlock.Text = charity.CharityName;
                DescBlock.Text = charity.CharityDescription;
                ImageBlock.Source = new BitmapImage(new Uri(charity.FullFileName));
                decimal absSum = 0;
                if (Sponsors.Items.Count == 0)
                {
                    NoSponsorsBlock.Visibility = Visibility.Visible;
                    sponsorsPanel.Visibility = Visibility.Collapsed;
                }
                for (int i = 0; i < Sponsors.Items.Count; i++)
                {
                    absSum += (Sponsors.Items[i] as Sponsorship).Amount;

                }
                ResultBlock.Text = $"Итого:    ${absSum}";
            } else
            {
                message.Text = "У Вас нет регистрации на текущий марафон";
                panel.Visibility = Visibility.Collapsed;
            }
           
        }
    }
}
