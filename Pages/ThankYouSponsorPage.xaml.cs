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
    /// Логика взаимодействия для ThankYouSponsorPage.xaml
    /// </summary>
    public partial class ThankYouSponsorPage : Page
    {
        public ThankYouSponsorPage(RegistrationEvent registrationEvent, string sum)
        {
            InitializeComponent();
            //
            /*
            < TextBlock x: Name = "RunnerBlock" ></ TextBlock >
        < TextBlock x: Name = "CharityBlock" ></ TextBlock >
        < TextBlock x: Name = "SumBlock" ></ TextBlock >
            */
            Registration reg = DB.entities.Registration.FirstOrDefault(r => r.RegistrationId == registrationEvent.RegistrationId);
            Charity charity = DB.entities.Charity.FirstOrDefault(c => c.CharityId == reg.CharityId);
            Runner runner = DB.entities.Runner.FirstOrDefault(r => r.RunnerId == reg.RunnerId);
            User user = DB.entities.User.FirstOrDefault(u => u.Email == runner.Email);
            Country country = DB.entities.Country.FirstOrDefault(c => c.CountryCode == runner.CountryCode);
            RunnerBlock.Text = $"{user.FirstName} {user.LastName}({registrationEvent.BibNumber}) из {country.CountryName}";
            SumBlock.Text = "$"+sum;
            CharityBlock.Text = charity.CharityName;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
