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
    /// Логика взаимодействия для BMICalulatorPage.xaml
    /// </summary>
    public partial class BMICalulatorPage : Page
    {
        public BMICalulatorPage()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            double weight, height;
            if (!double.TryParse(WeightBox.Text, out weight) || !double.TryParse(HeightBox.Text, out height))
            {
                MessageBox.Show("Вы ввели неправильные данные в поля","Ошибка");
                return;
            }
            double bmi = weight / (height * height / 10000);
            BMIBlock.Text = bmi.ToString();
            double triangleLeft = bmi * 7 - 10;
            if (bmi > 36)
            {
                triangleLeft = 242; 

            }
            //Canvas.SetLeft(TrianglePanel, triangleLeft);
            if (bmi < 18.5)
            {
                StatusBlock.Text = "Недостаточный вес";
                BMIImage.Source = new BitmapImage(new Uri("\\Resources\\bmi-underweight-icon.png", UriKind.Relative));
            }
            else if (bmi < 24.9)
            {
                StatusBlock.Text = "Здоровый вес";
                BMIImage.Source = new BitmapImage(new Uri("\\Resources\\bmi-healthy-icon.png", UriKind.Relative));
            }
            else if (bmi < 29.9)
            {
                StatusBlock.Text = "Лишний вес";
                BMIImage.Source = new BitmapImage(new Uri("\\Resources\\bmi-overweight-icon.png", UriKind.Relative));
            }
            else if (bmi > 30)
            {
                StatusBlock.Text = "Ожирение";
                BMIImage.Source = new BitmapImage(new Uri("\\Resources\\bmi-obese-icon.png", UriKind.Relative));
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
