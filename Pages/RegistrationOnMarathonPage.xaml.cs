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
    /// Логика взаимодействия для RegistrationOnMarathonPage.xaml
    /// </summary>
    public partial class RegistrationOnMarathonPage : Page
    {
        MainWindow mainWindow;
        public RegistrationOnMarathonPage()
        {
            InitializeComponent();
            FundComboBox.ItemsSource = DB.entities.Charity.ToList();
        }
        int optionSum = 0;
        int kitSum = 0;

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            decimal sponsorSum;
            if (!decimal.TryParse(SponsorSumBox.Text, out sponsorSum) || sponsorSum <= 0)
            {
                MessageBox.Show("Введите правильную сумму взноса, которая должна быть больше 0", "Ошибка");
                return;
            }
            if (FundComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите фонд, в который внесете взнос", "Ошибка");
                return;
            }
            if (FirstMarathonOption.IsChecked != true && SecondMarathonOption.IsChecked != true && ThirdMarathonOption.IsChecked != true)
            {
                MessageBox.Show("Выберите хотя бы один вид забега", "Ошибка");
                return;
            }
            int resultSum = optionSum + kitSum;
            mainWindow = (MainWindow)Window.GetWindow(this);
            Charity charity = FundComboBox.SelectedItem as Charity;
            Runner runner = DB.entities.Runner.FirstOrDefault(r => r.Email == mainWindow.user.Email);
            string kitopt = "";
            if (FirstKitOption.IsChecked != null && SecondKitOption.IsChecked != null && ThirdKitOption.IsChecked != null)
            {
                if (FirstKitOption.IsChecked == true)
                {
                    kitopt = "A";
                }
                else if (SecondKitOption.IsChecked == true)
                {
                    kitopt = "B";
                }
                else if (ThirdKitOption.IsChecked == true)
                {
                    kitopt = "C";
                }
            } else
            {
                MessageBox.Show("Вы не выбрали вариант комплекта", "Ошибка");
            }
            Registration registration = new Registration()
            {
                CharityId = charity.CharityId,
                RunnerId = runner.RunnerId,
                Cost = resultSum,
                SponsorshipTarget = sponsorSum,
                RegistrationStatusId = 1,
                RegistrationDateTime = DateTime.Now,
                RaceKitOptionId = kitopt
            };
            Marathon nearestmarathon = DB.entities.Marathon.OrderByDescending(m => m.YearHeld).ToList().FirstOrDefault(); // последний марафон
            List<Event> events = DB.entities.Event.Where(eve => eve.MarathonId == nearestmarathon.MarathonId).ToList();
            DB.entities.Registration.Add(registration);
            DB.entities.SaveChanges();
            // сделать регистрации на Event'ы
            if (FirstMarathonOption.IsChecked == true)
            {
                Event ev = events.FirstOrDefault(eve => eve.EventTypeId == "FM");

                short bibNumber = (short)((DB.entities.RegistrationEvent.Where(re => re.EventId == ev.EventId).ToList()).Count + 1);
                RegistrationEvent registrationEvent = new RegistrationEvent()
                {
                    RegistrationId = registration.RegistrationId,
                    RaceTime = null,
                    EventId = ev.EventId,
                    BibNumber = bibNumber
                };
                DB.entities.RegistrationEvent.Add(registrationEvent);
            }
            if (SecondMarathonOption.IsChecked == true)
            {
                Event ev = events.FirstOrDefault(eve => eve.EventTypeId == "HM");

                short bibNumber = (short)((DB.entities.RegistrationEvent.Where(re => re.EventId == ev.EventId).ToList()).Count + 1);
                RegistrationEvent registrationEvent = new RegistrationEvent()
                {
                    RegistrationId = registration.RegistrationId,
                    RaceTime = null,
                    EventId = ev.EventId,
                    BibNumber = bibNumber
                };
                DB.entities.RegistrationEvent.Add(registrationEvent);
            }
            if (ThirdMarathonOption.IsChecked == true)
            {
                Event ev = events.FirstOrDefault(eve => eve.EventTypeId == "FR");

                short bibNumber = (short)((DB.entities.RegistrationEvent.Where(re => re.EventId == ev.EventId).ToList()).Count + 1);
                RegistrationEvent registrationEvent = new RegistrationEvent()
                {
                    RegistrationId = registration.RegistrationId,
                    RaceTime = null,
                    EventId = ev.EventId,
                    BibNumber = bibNumber
                };
                DB.entities.RegistrationEvent.Add(registrationEvent);

            }
            
            DB.entities.SaveChanges();
            NavigationService.Navigate(new ThankYouRunnerPage());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RunnerMenuPage());
        }

        private void FirstMarathonOption_Checked(object sender, RoutedEventArgs e)
        {
            if (FirstMarathonOption.IsChecked != null)
            {
                if (FirstMarathonOption.IsChecked == true)
                {
                    optionSum += 145;
                } else
                {
                    optionSum -= 145;
                }
            }
            RegistrationSumBlock.Text = "$" + (optionSum + kitSum).ToString();
        }

        private void SecondMarathonOption_Checked(object sender, RoutedEventArgs e)
        {
            if (SecondMarathonOption.IsChecked != null)
            {
                if (SecondMarathonOption.IsChecked == true)
                {
                    optionSum += 75;
                } else
                {
                    optionSum -= 75;
                }
            }
            RegistrationSumBlock.Text = "$" + (optionSum + kitSum).ToString();
        }

        private void ThirdMarathonOption_Checked(object sender, RoutedEventArgs e)
        {
            if (ThirdMarathonOption.IsChecked != null)
            {
                if (ThirdMarathonOption.IsChecked == true)
                {
                    optionSum += 20;
                } else
                {
                    optionSum -= 20;
                }
            }
            RegistrationSumBlock.Text = "$"+(optionSum + kitSum).ToString();
        }

        private void KitOption_Checked(object sender, RoutedEventArgs e)
        {
            if (FirstKitOption.IsChecked != null)
            {
                if (FirstKitOption.IsChecked == true)
                {
                    kitSum = 0;
                }
            }
            if (SecondKitOption.IsChecked != null)
            {
                if (SecondKitOption.IsChecked == true)
                {
                    kitSum = 20;
                }
            }
            if (ThirdKitOption.IsChecked != null)
            {
                if (ThirdKitOption.IsChecked == true)
                {
                    kitSum = 45;
                }
            }
            RegistrationSumBlock.Text = "$" + (optionSum + kitSum).ToString();
        }

        private void CharityInfoButton_Click(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
        }

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = false;
        }

        private void FundComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Charity charity = FundComboBox.SelectedItem as Charity;
            CharityDescription.Text = charity.CharityDescription;
            CharityName.Text = charity.CharityName;
            CharityLogo.Source = new BitmapImage(new Uri(@"C:\Users\masha\Desktop\charity\" + charity.CharityLogo));
        }
    }
}
