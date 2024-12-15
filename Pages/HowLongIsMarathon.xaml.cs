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
    /// Логика взаимодействия для HowLongIsMarathon.xaml
    /// </summary>
    public partial class HowLongIsMarathon : Page
    {
        public HowLongIsMarathon()
        {
            InitializeComponent();
            DistanceItems.ItemsSource = DB.entities.ItemComparateDistance.ToList();
            SpeedItems.ItemsSource = DB.entities.ItemComparateSpeed.ToList();
        }

        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListView list = sender as ListView;
            if (list.Name == "SpeedItems")
            {
                var item = list.SelectedItem as ItemComparateSpeed;
                ItemNameBlock.Text = item.ItemName;
                ItemImage.Source = new BitmapImage(new Uri("C:\\Users\\masha\\Desktop\\how-long-is-a-marathon-images\\" + item.Image));
                ItemDescriptionBlock.Text = $"Максимальная скорость {item.ItemName} {item.ItemMaxSpeed}km/h. Это займет {(int)(42/item.ItemMaxSpeed)}часов {(42/item.ItemMaxSpeed - (int)(42 / item.ItemMaxSpeed))*60} минут, чтобы завершить 42km марафон";
                
            } 
            else 
            {
                var item = list.SelectedItem as ItemComparateDistance;
                ItemNameBlock.Text = item.ItemName;
                ItemImage.Source = new BitmapImage(new Uri("C:\\Users\\masha\\Desktop\\how-long-is-a-marathon-images\\" + item.Image));
                ItemDescriptionBlock.Text = $"Длина {item.ItemName} {item.ItemLength}m. Это займет {(42000/item.ItemLength)} из них, чтобы покрыть расстояние в 42 км марафона";
                
            }
            
        }
    }
}
