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
    /// Логика взаимодействия для AdministrateRunnersPage.xaml
    /// </summary>
    public partial class AdministrateRunnersPage : Page
    {
        int totalRunners = 0;
        public AdministrateRunnersPage()
        {
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatusComboBox.SelectedItem == null || DistanceComboBox.SelectedItem == null || SortComboBox.SelectedItem == null)
            {
                MessageBox.Show("Все выборы сортировки и фильтрации обязательны", "Ошибка");
                return;
            }
            SortAndFilterRunnersList();
        }
        void ReadRunnersList()
        {
            Marathon marathon = DB.entities.Marathon.ToList().OrderByDescending(mar => mar.YearHeld).First();
            List<Event> events = DB.entities.Event.Where(eve => eve.MarathonId == marathon.MarathonId).ToList();
            HashSet<Registration> registrations = new HashSet<Registration>();
            foreach (Event e in events) 
            {
                List<RegistrationEvent> registrationEvents = DB.entities.RegistrationEvent.Where(f=> f.EventId == e.EventId).ToList();
                foreach (RegistrationEvent registrationEvent in registrationEvents)
                {
                    Registration registration = DB.entities.Registration.FirstOrDefault(reg => reg.RegistrationId == registrationEvent.RegistrationId);
                    if (!registrations.Contains(registration))
                    {
                        registrations.Add(registration);
                    }
                }
            }
            RunnersList.ItemsSource = registrations;
            
            totalRunners = registrations.Count;
            if (totalRunners == 0)
            {
                NoRunnersBlock.Visibility = Visibility.Visible;
            } else
            {
                NoRunnersBlock.Visibility = Visibility.Collapsed;
            }
        }
        void SortAndFilterRunnersList()
        {
           
            Event evvent = DistanceComboBox.SelectedItem as Event;
            RegistrationStatus status = StatusComboBox.SelectedItem as RegistrationStatus;
            String field = SortComboBox.SelectedItem as String;

           
            List<Registration> registrations = new List<Registration>();
            List<RegistrationEvent> registrationEvents = DB.entities.RegistrationEvent.Where(f => f.EventId == evvent.EventId).ToList();
            foreach (RegistrationEvent registrationEvent in registrationEvents)
            {
                Registration registration = DB.entities.Registration.FirstOrDefault(reg => reg.RegistrationId == registrationEvent.RegistrationId 
                && reg.RegistrationStatusId == status.RegistrationStatusId);
                if (!registrations.Contains(registration) && registration != null)
                {
                    registrations.Add(registration);
                }
            }
            switch (field)
            {
                case "Имя":
                    registrations.OrderBy(reg => reg.FirstName);
                    break;
                case "Фамилия":
                    registrations.OrderBy(reg => reg.LastName);
                    break;
                case "Email":
                    registrations.OrderBy(reg => reg.Email);
                    break;
                case "Статус":
                    registrations.OrderBy(reg => reg.StatusInfo);
                    break;
            }
            RunnersList.ItemsSource = registrations;
            totalRunners = registrations.Count;
            
            if (totalRunners == 0)
            {
                NoRunnersBlock.Visibility = Visibility.Visible;
            }
            else
            {
                NoRunnersBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void CVSButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EmailListButton_Click(object sender, RoutedEventArgs e)
        {
            EmailPopup.IsOpen = true;
            foreach (var item in RunnersList.Items)
            {
                Registration registration = item as Registration;
                if (registration != null) 
                {
                    EmailsBox.Text += $"\"{registration.LastName} {registration.FirstName} {registration.RegistrationStatusId}\" <{registration.Email}>;\n";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // переход на страницу с переданным Tag
            Button button = sender as Button;
            int regId = (int)button.Tag;
            NavigationService.Navigate(new AdministrateRunnerAccountPage(regId));

        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            StatusComboBox.ItemsSource = DB.entities.RegistrationStatus.ToList();
            Marathon marathon = DB.entities.Marathon.ToList().OrderByDescending(mar => mar.YearHeld).First();
            List<Event> events = DB.entities.Event.Where(eve => eve.MarathonId == marathon.MarathonId).ToList();
            DistanceComboBox.ItemsSource = events;
            List<string> fields = new List<string>();
            fields.Add("Имя");
            fields.Add("Фамилия");
            fields.Add("Email");
            fields.Add("Статус");
            ReadRunnersList();
            SortComboBox.ItemsSource = fields;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            EmailPopup.IsOpen= false;
        }
    }
}
