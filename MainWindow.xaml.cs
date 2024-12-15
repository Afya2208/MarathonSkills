using MarathonSkills.Database;
using MarathonSkills.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarathonSkills
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    /*
     * Этот шрифт должен использоваться для всех заголовков.
Open Sans Semibold
    Этот шрифт должен использоваться в качестве основного шрифта.
Open Sans Light
    Этот альтернативный шрифт может использоваться, если у Вас нет доступа к Open Sans font.
Arial

    Бразильский
зеленый
R: 0
G: 144
B: 62
Бразильский
желтый
R: 253
G: 193
B: 0
Бразильский
синий
R: 36
G: 29
B: 112

    Белый
R: 255
G: 255
B: 255
Светло серый
R: 235
G: 235
B: 235
Серый
R: 180
G: 180
B: 180
Темно серый
R: 80
G: 80
B: 80
Черный
R: 0
G: 0
B: 0
     */
    public partial class MainWindow : Window
    {
        public User user;
      
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            MainFrame.Navigate(new MainPage());
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime theMarathon = new DateTime(2024, 11, 25, 9, 0, 0);
            TimeSpan diff = theMarathon - now;
            TimeUntilBlock.Text = $"{diff.Days} дня {diff.Hours} часов {diff.Minutes + (diff.Seconds > 0? 1 : 0)} минут до старта марафона!";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
            if (MainFrame.Content as RegistrationOnMarathonPage != null)
            {
                MainFrame.Navigate(new RunnerMenuPage());
            }
            if (MainFrame.Content as ThankYouRunnerPage != null)
            {
                MainFrame.Navigate(new RunnerMenuPage());
            }
            if (MainFrame.Content as ThankYouSponsorPage != null)
            {
                MainFrame.Navigate(new MainPage());
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //Page currentPage = MainFrame.Content as Page;
            //Height = currentPage. + 10;
            //Width = currentPage.Width + 10;
            //this.SizeToContent = SizeToContent.WidthAndHeight;
            //this.SizeToContent = SizeToContent.Manual;
            if (MainFrame.CanGoBack)
            {
                BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                BackButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage());
            Logout.Visibility = Visibility.Collapsed;
            user = null;
        }
    }
}
