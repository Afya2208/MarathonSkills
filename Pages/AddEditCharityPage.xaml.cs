using MarathonSkills.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddEditCharityPage.xaml
    /// </summary>
    
    public partial class AddEditCharityPage : Page
    {
        Charity _charity;
        string fileName;
        public AddEditCharityPage(int charityId)
        {
            InitializeComponent();
            if (charityId == 0)
            {
                _charity = new Charity();
                
            } else
            {
                _charity = DB.entities.Charity.FirstOrDefault(c => c.CharityId == charityId);
            }
            DataContext = _charity;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
            {
                MessageBox.Show("Вы ввели пустое имя", "Ошибка");
            }
            FileInfo info = new FileInfo(FileBox.Text);

            if (_charity.CharityId == 0)
            {
                _charity.CharityLogo = info.Name;
            } else
            {
                if (FileBox.Text != "")
                {
                    
                    _charity.CharityLogo = info.Name;
                }
            }
            _charity.CharityDescription = DescBox.Text;
            _charity.CharityName = NameBox.Text;
            if (_charity.CharityId == 0) DB.entities.Charity.Add( _charity );
            DB.entities.SaveChanges();
            NavigationService.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pictures|*.png;*.jpg;*.jpeg;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    ImageBox.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    fileName = openFileDialog.FileName;
                    FileBox.Text = openFileDialog.FileName;
                }
            }
            if (openFileDialog.FileName == "")
            {
                fileName = null;
            }
        }
    }
}
