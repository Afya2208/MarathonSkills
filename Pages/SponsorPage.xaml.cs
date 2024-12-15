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
    /// Логика взаимодействия для SponsorPage.xaml
    /// </summary>  имя, фамилия - BibNumber (CountryCode).
    public partial class SponsorPage : Page
    {
        public SponsorPage()
        {
            InitializeComponent();
            Marathon nearestmarathon = DB.entities.Marathon.OrderByDescending(m => m.YearHeld).ToList().FirstOrDefault(); // последний марафон
            List<Event> events = DB.entities.Event.Where(e => e.MarathonId == nearestmarathon.MarathonId).ToList(); // список забегов
            List<RegistrationEvent> registrationsToEvents = new List<RegistrationEvent>();
            foreach (Event ev in events)
            {
                registrationsToEvents.AddRange(DB.entities.RegistrationEvent.Where(r => r.EventId == ev.EventId).ToList());
            }
            RunnerComboBox.ItemsSource = registrationsToEvents;
        }
        private bool CheckCardNumber(string cardNumber)
        {
            foreach (char c in cardNumber)
            {
                if (!Char.IsNumber(c)) return false;
            }
            return true;
        }
        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            string sponsorName, cardOwner, cardNumber;
            uint CVC, cardMonth, cardYear, sum;
            sponsorName = SponsorNameBox.Text;
            cardOwner = CardOwnerBox.Text;
            cardNumber = CardNumberBox.Text;
            if (sponsorName == "" || cardOwner == "" || cardNumber.Length < 16)
            {
                MessageBox.Show("Вы ввели пустое поле, все поля обязательны к заполнению", "Ошибка");
                return;
            }
            if (!uint.TryParse(CardMonthBox.Text, out cardMonth) || 
                !uint.TryParse(CVCBox.Text, out CVC) || !uint.TryParse(CardYearBox.Text, out cardYear) || !CheckCardNumber(cardNumber))
            {
                MessageBox.Show("Вы ввели неправильные данные карты", "Ошибка");
                return;
            } else if (cardYear < DateTime.Today.Year || cardYear == DateTime.Today.Year && cardMonth < DateTime.Today.Month || cardMonth == 0 || cardMonth > 12 || CVC < 100)
            {
                MessageBox.Show("Вы ввели неправильные данные карты", "Ошибка");
                return;
            }
            if (!uint.TryParse(SumBox.Text, out sum) || sum == 0)
            {
                MessageBox.Show("Вы ввели некорректную сумму", "Ошибка");
                return;
            }
            RegistrationEvent re = (RunnerComboBox.SelectedItem as RegistrationEvent);
            Registration reg = DB.entities.Registration.FirstOrDefault(r => r.RegistrationId == re.RegistrationId);
            DB.entities.Sponsorship.Add(new Sponsorship { Amount = sum, RegistrationId = reg.RegistrationId, SponsorName = sponsorName});
            DB.entities.SaveChanges();
            NavigationService.Navigate(new ThankYouSponsorPage(RunnerComboBox.SelectedItem as RegistrationEvent, SumBox.Text));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            uint sum;
            if (uint.TryParse(SumBox.Text, out sum))
            {
                SumBox.Text = (sum + 10).ToString();
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            uint sum;
            if (uint.TryParse(SumBox.Text, out sum) && sum >= 10)
            {
                SumBox.Text = (sum - 10).ToString();
            }
        }

        private void SumBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uint.TryParse(SumBox.Text, out var result))
            {
                SumBlock.Text = "$"+SumBox.Text;
            } else
            {
                SumBlock.Text = "?";
            }
        }

        private void RunnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // "CharityNameBlock"     (RunnerComboBox.SelectedItem as RegistrationEvent)
            RegistrationEvent registrationEvent = RunnerComboBox.SelectedItem as RegistrationEvent;
            Registration reg = DB.entities.Registration.FirstOrDefault(r => registrationEvent.RegistrationId == r.RegistrationId);
            Charity charity = DB.entities.Charity.FirstOrDefault(c => c.CharityId == reg.CharityId);
            CharityNameBlock.Text = charity.CharityName;
            CharityDescription.Text = charity.CharityDescription;
            CharityName.Text = charity.CharityName;
            CharityLogo.Source = new BitmapImage(new Uri(@"C:\Users\masha\Desktop\charity\" + charity.CharityLogo));
        }

        private void CharityInfoButton_Click(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
        }

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = false;
        }
    }
}
