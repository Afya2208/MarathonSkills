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
    /// Логика взаимодействия для ItemArrivalPage.xaml
    /// </summary>
    public partial class ItemArrivalPage : Page
    {
        List<ArrivalItem> arrivalItems;
        public ItemArrivalPage()
        {
            InitializeComponent();
            List<Item> items = DB.entities.Item.ToList();
            arrivalItems = new List<ArrivalItem>();
            foreach (Item item in items)
            {
                arrivalItems.Add(new ArrivalItem()
                {
                    ItemId = item.ItemId,
                    ItemAmount = 0,
                    DateOfArrival = DateTime.Now,

                });
            }
            ArrivalList.ItemsSource = arrivalItems;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox != null)
            {
                int check;
                if (!int.TryParse(textBox.Text, out check))
                {
                    SaveButton.IsEnabled = false;
                    CheckFailed.Visibility = Visibility.Visible;
                    CheckFailed.Text = "Все числовые поля должны быть заполнены целыми числами";
                }
                else
                {
                    SaveButton.IsEnabled = true;
                    CheckFailed.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in arrivalItems)
            {
                Storage storage = DB.entities.Storage.FirstOrDefault(s => s.ItemId == item.ItemId);
                int checkAmount = storage.ItemAmount + item.ItemAmount;
                if (checkAmount == storage.ItemAmount) // если количество поступления = 0
                {
                    continue;
                }
                if (checkAmount > -1)
                {
                    DB.entities.ArrivalItem.Add(item);
                    storage.ItemAmount = checkAmount;
                    DB.entities.SaveChanges();
                }

                else
                {
                    Item item1 = DB.entities.Item.FirstOrDefault(s => s.ItemId == storage.ItemId);
                    MessageBox.Show($"Вы пытаетесь списать больше предметов ({item1.ItemName}), чем их есть ({storage.ItemAmount})", "Ошибка");
                    return;
                }
                
            }
            NavigationService.GoBack();
        }
    }
}
