using MarathonSkills.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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
    /// Логика взаимодействия для AdministrateVolunteersPage.xaml
    /// </summary>
    public partial class AdministrateVolunteersPage : Page
    {
        int volunteerAmount;

        public AdministrateVolunteersPage()
        {
            InitializeComponent();

            VolunteerList.ItemsSource = DB.entities.Volunteer.ToList();
            volunteerAmount = VolunteerList.Items.Count;
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (SortBox.SelectedItem != null)
            {
                switch (SortBox.SelectedIndex)
                {
                    case 0:
                        VolunteerList.ItemsSource = DB.entities.Volunteer.OrderBy(vol => vol.LastName).ToList();
                        break;
                    case 1:
                        VolunteerList.ItemsSource = DB.entities.Volunteer.OrderBy(vol => vol.FirstName).ToList();
                        break;
                    case 2:
                        VolunteerList.ItemsSource = DB.entities.Volunteer.OrderBy(vol => vol.CountryCode).ToList();
                        break;
                    case 3:
                        VolunteerList.ItemsSource = DB.entities.Volunteer.OrderBy(vol => vol.Gender).ToList();
                        break;
                }
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel файлы со значениями через запятую|*.csv";
            if (ofd.ShowDialog() == true)
            {
                if (File.Exists(ofd.FileName))
                {
                    string[] strs = File.ReadAllLines(ofd.FileName);
                    // проверка формата

                    string str = strs[0];
                    if (str != "VolunteerId,FirstName,LastName,CountryCode,Gender")
                    {
                        MessageBox.Show("Формат файла неправильный. Первая строка должна быть 'VolunteerId,FirstName,LastName,CountryCode,Gender'", "Ошибка");
                        return;
                    }


                    for (int i = 1; i < strs.GetLength(0); i++)
                    {
                        string[] els = strs[i].Split(',');
                        Volunteer volunteer = new Volunteer()
                        {
                            VolunteerId = int.Parse(els[0]),
                            FirstName = els[1],
                            LastName = els[2],
                            CountryCode = els[3],
                            Gender = els[4] == "M" ? "Male" : "Female"
                        };
                        int countNew = 0, countUpdate = 0;
                        if (DB.entities.Volunteer.Any(v=>v.VolunteerId == volunteer.VolunteerId)) // если есть  волонтер c таким же id, то обновляем
                        {
                            DB.entities.Volunteer.AddOrUpdate(volunteer);
                            countUpdate++;
                        } else
                        {
                            DB.entities.Volunteer.Add(volunteer);
                            countNew++;
                        }

                        DB.entities.SaveChanges();
                    }
                    VolunteerList.ItemsSource = DB.entities.Volunteer.ToList();
                }
            }
        }
    }
}
