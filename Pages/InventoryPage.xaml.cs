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
    /// Логика взаимодействия для InventoryPage.xaml
    /// </summary>
    public partial class InventoryPage : Page
    {
        public InventoryPage()
        {
            InitializeComponent();
            
        }

        private void ArrivalButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ItemArrivalPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Marathon nearestmarathon = DB.entities.Marathon.OrderByDescending(m => m.YearHeld).ToList().FirstOrDefault(); // последний марафон
            List<Event> events = DB.entities.Event.Where(ev => ev.MarathonId == nearestmarathon.MarathonId).ToList(); // список забегов
            List<RegistrationEvent> registrationsToEvents = new List<RegistrationEvent>();
            foreach (Event ev in events)
            {
                registrationsToEvents.AddRange(DB.entities.RegistrationEvent.Where(r => r.EventId == ev.EventId && r.RaceTime == null).ToList());
            }
            HashSet<Registration> registrations = new HashSet<Registration>();
            int numA = 0, numB = 0, numC = 0;
            foreach (RegistrationEvent ev in registrationsToEvents)
            {
                Registration registration = DB.entities.Registration.FirstOrDefault(a => a.RegistrationId == ev.RegistrationId);
                if (!registrations.Contains(registration))
                {
                    registrations.Add(registration);
                    switch (registration.RaceKitOptionId)
                    {
                        case "A":
                            numA++;
                            break;
                        case "B":
                            numB++;
                            break;
                        case "C":
                            numC++;
                            break;
                    }
                }
            }
            NumberOfRegistrationsBlock.Text = registrations.Count.ToString();
            List<Storage> storages = DB.entities.Storage.ToList(); // все остатки
            // первый элемент
            ItemForTable first = new ItemForTable()
            {
                Name = "Выбрало данный вариант",
                NumA = numA,
                NumB = numB,
                NumC = numC
            };
            List<ItemForTable> itemsForTable = new List<ItemForTable>();
            itemsForTable.Add(first);
            foreach (Storage storage in storages)
            {
                Item item = DB.entities.Item.FirstOrDefault(i => i.ItemId == storage.ItemId);
                ItemsInKit kitA = DB.entities.ItemsInKit.FirstOrDefault(k => k.RaceKitOptionId == "A" && k.ItemId == item.ItemId);
                ItemsInKit kitB = DB.entities.ItemsInKit.FirstOrDefault(k => k.RaceKitOptionId == "B" && k.ItemId == item.ItemId);
                ItemsInKit kitC = DB.entities.ItemsInKit.FirstOrDefault(k => k.RaceKitOptionId == "C" && k.ItemId == item.ItemId);

                itemsForTable.Add(new ItemForTable()
                {
                    Name = item.ItemName,
                    NumA = numA * (kitA == null ? 0 : kitA.ItemAmount),
                    NumB = numB * (kitB == null ? 0 : kitB.ItemAmount),
                    NumC = numC * (kitC == null ? 0 : kitC.ItemAmount),
                    Remains = storage.ItemAmount
                });

            }
            ItemsView.ItemsSource = itemsForTable;
        }
    }
}
