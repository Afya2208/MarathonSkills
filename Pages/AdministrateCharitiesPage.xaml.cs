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
    /// Логика взаимодействия для AdministrateCharitiesPage.xaml
    /// </summary>
    public partial class AdministrateCharitiesPage : Page
    {
        public AdministrateCharitiesPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Charity> charities = DB.entities.Charity.ToList();
            CharityList.ItemsSource = charities;
            AddButton.Tag = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = (int)button.Tag;
            NavigationService.Navigate(new AddEditCharityPage(id));
        }
    }
}
