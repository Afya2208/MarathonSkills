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
    /// Логика взаимодействия для AdministrateSponsorsPage.xaml
    /// </summary>
    public partial class AdministrateSponsorsPage : Page
    {
        decimal totalSponsorTarget = 0;
        int totalCharities = 0;
        List<Charity> allCharity = new List<Charity>();
        public AdministrateSponsorsPage()
        {
            InitializeComponent();
            Marathon marathon = DB.entities.Marathon.OrderByDescending(m => m.YearHeld).First();
            List<Event> events = DB.entities.Event.Where(eve => eve.MarathonId == marathon.MarathonId).ToList();
            HashSet<Registration> registrations = new HashSet<Registration>();
            foreach (Event e in events)
            {
                List<RegistrationEvent> registrationEvents = DB.entities.RegistrationEvent.Where(f => f.EventId == e.EventId).ToList();
                foreach (RegistrationEvent registrationEvent in registrationEvents)
                {
                    Registration registration = DB.entities.Registration.FirstOrDefault(reg => reg.RegistrationId == registrationEvent.RegistrationId);
                    if (!registrations.Contains(registration))
                    {
                        registrations.Add(registration);
                    }
                }
            }
            HashSet<Charity> charities = DB.entities.Charity.ToHashSet();
            foreach (Registration registration in registrations)
            {
                Charity charity = DB.entities.Charity.FirstOrDefault(c => c.CharityId == registration.CharityId);
                Charity ch;
                if (!charities.Contains(charity))
                {
                    charities.Add(charity);
                }
                charities.TryGetValue(charity, out ch);
                ch.Sum += registration.SponsorshipTarget;
                totalSponsorTarget += registration.SponsorshipTarget;
            }
            charities = charities.Where(c => c.Sum != 0).ToHashSet();
            allCharity = charities.ToList();
            CharitiesList.ItemsSource = charities;
            totalCharities = charities.Count;
            TotalCharityBlock.Text = totalCharities.ToString();
            TotalCharityPayBlock.Text = totalSponsorTarget.ToString();
            SortComboBox.ItemsSource = new List<string>()
            {
                "Наименование",
                "Сумма"
            };
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (SortComboBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали по какому полю сортировать","Ошибка");
                return;
            }
            switch (SortComboBox.SelectedItem as string)
            {
                case "Наименование":
                    CharitiesList.ItemsSource = allCharity.OrderBy(c => c.CharityName).ToList();
                    break;
                case "Сумма":
                    CharitiesList.ItemsSource = allCharity.OrderBy(c => c.Sum).ToList();
                    break;
            }
        }
    }
}
