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
    /// Логика взаимодействия для AllResultsPage.xaml
    /// </summary>
    public partial class AllResultsPage : Page
    {
        Marathon marathon = DB.entities.Marathon.OrderByDescending(m => m.YearHeld).First();
        int totalRunners = 0;
        int totalFinishedRunners = 0;
        double avgTime = 0;
        public AllResultsPage()
        {
            InitializeComponent();
            // загрузка возможных вариантов выбора:
            MarathonComboBox.ItemsSource = DB.entities.Marathon.ToList();
            List<string> genders = new List<string>
            {
                "Все",
                "Женский",
                "Мужской"
            };
            GenderComboBox.ItemsSource = genders;
            GenderComboBox.SelectedIndex = 0;
            List<string> cat = new List<string>
            {
                "до 18",
                "от 18 до 29",
                "от 30 до 39",
                "от 40 до 55",
                "от 56 до 69",
                "от 70"
            };
            CategoryComboBox.ItemsSource = cat;
            ReadRaces();
            HowMuchRunners(RacesList.ItemsSource as List<RegistrationEvent>);
            RacesList.ItemsSource = RaceRanks(RacesList.ItemsSource as List<RegistrationEvent>);
        }
        List<RegistrationEvent> RaceRanks(List<RegistrationEvent> races)
        {
            List<RegistrationEvent> registrationEvents = races.ToList();

            registrationEvents.OrderBy(race => race.RaceTime); //сортировка по времени, далее отсчет мест
            int rank = 0; // первое место, ведь первая проверка maxRaceTime добавит +1 к месту
            int maxRaceTime = -1; // время
            foreach (RegistrationEvent registration in registrationEvents)
            {
                if (registration.RaceTime != null && registration.RaceTime.Value != 0)
                {
                    if (registration.RaceTime.Value > maxRaceTime)
                    {
                        maxRaceTime = registration.RaceTime.Value;
                        rank++;
                    }
                    registration.Rank = rank;
                }
            }
            return registrationEvents;
        }
        void ReadRaces()
        {
            List<Marathon> marathons = DB.entities.Marathon.ToList();
            List<Event> events = DB.entities.Event.Where(eve => eve.MarathonId == marathon.MarathonId).ToList();
            List<RegistrationEvent> registrations = new List<RegistrationEvent>();
            foreach (Event e in events)
            {
                List<RegistrationEvent> registrationEvents = DB.entities.RegistrationEvent.Where(f => f.EventId == e.EventId).ToList();
                registrations.AddRange(registrationEvents);
            }
            RacesList.ItemsSource = registrations;
        }
        void HowMuchRunners(List<RegistrationEvent> races)
        {
            totalRunners = races.Count;
            foreach (var item in races)
            {
                if (item.RaceTime != null && item.RaceTime.Value != 0)
                {
                    avgTime += item.RaceTime.Value;
                    totalFinishedRunners++;
                }
            }
            avgTime /= totalFinishedRunners * 1.0;
            TotalRunnersBlock.Text = totalRunners.ToString();
            TotalFinishedRunnersBlock.Text = totalFinishedRunners.ToString();
            TotalTimeBlock.Text = avgTime.ToString();

        }
        void FilterRaces()
        {
            // список бегунов по выбранным категориям
            List<RegistrationEvent> regs = new List<RegistrationEvent>();
            // выбранные опции
            Marathon selectedMarathon = MarathonComboBox.SelectedItem as Marathon;
            Event @event = DistanceComboBox.SelectedItem as Event;
            string selectedGender = GenderComboBox.SelectedItem as string;
            // набор всех бегунов по выбранному марафону
                regs.AddRange(DB.entities.RegistrationEvent.Where(r => r.EventId == @event.EventId));
            
            // фильтрация по полу
            if (selectedGender != "Все") 
            {
                Gender gender = DB.entities.Gender.FirstOrDefault(g=>g.Gender1 == (selectedGender == "Мужской" ? "Male" : "Female"));
                regs = regs.FindAll(reg => reg.Gender == gender);
            }
            // фильтрация по возрасту
            switch (CategoryComboBox.SelectedItem as string)
            {
                case "до 18":
                    regs = regs.FindAll(reg => reg.Category == "до 18");
                    break;
                case "от 18 до 29":
                    regs = regs.FindAll(reg => reg.Category == "от 18 до 29");
                    break;
                case "от 30 до 39":
                    regs = regs.FindAll(reg => reg.Category == "от 30 до 39");
                    break;
                case "от 40 до 55":
                    regs = regs.FindAll(reg => reg.Category == "от 40 до 55");
                    break;
                case "от 56 до 69":
                    regs = regs.FindAll(reg => reg.Category == "от 56 до 69");
                    break;
                case "от 70":
                    regs = regs.FindAll(reg => reg.Category == "от 70");
                    break;
            }
            RacesList.ItemsSource = regs;
            
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (GenderComboBox.SelectedItem == null || MarathonComboBox.SelectedItem == null || CategoryComboBox.SelectedItem == null || DistanceComboBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали все нужные поля","ошибка");
                return;
            }
            FilterRaces();
            RacesList.ItemsSource = RaceRanks(RacesList.ItemsSource as List<RegistrationEvent>);
            HowMuchRunners(RacesList.ItemsSource as List<RegistrationEvent>);
        }

        private void MarathonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Marathon selectedMarathon = MarathonComboBox.SelectedItem as Marathon;
            List<Event> events = DB.entities.Event.Where(dis => dis.MarathonId == selectedMarathon.MarathonId).ToList();
            DistanceComboBox.ItemsSource = events;
        }
    }
}
