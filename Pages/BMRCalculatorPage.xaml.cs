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
    /// Логика взаимодействия для BMRCalculatorPage.xaml
    /// </summary>
    public partial class BMRCalculatorPage : Page
    {
        public BMRCalculatorPage()
        {
            InitializeComponent();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            int age;
            double weight, height;
            if (!double.TryParse(WeightBox.Text, out weight) || !double.TryParse(HeightBox.Text, out height) || !int.TryParse(AgeBox.Text, out age))
            {
                MessageBox.Show("Вы ввели неправильные данные в поля", "Ошибка");
                return;
            }
            double bmr;
            if (FemaleButton.IsChecked == true)
            {
                bmr = 655 + (9.6 * weight) + (1.8 * height) - (4.7 * age);
            } else
            {
                bmr = 66 + (13.7 * weight) + (5 * height) - (6.8 * age);
            }
            BMRBlock.Text = bmr.ToString();
            Act1Block.Text = (bmr * 1.2).ToString();
            Act2Block.Text = (bmr * 1.375).ToString();
            Act3Block.Text = (bmr * 1.55).ToString();
            Act4Block.Text = (bmr * 1.725).ToString();
            Act5Block.Text = (bmr * 1.9).ToString();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }
    }
}
