using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для InteractiveMapPage.xaml
    /// </summary>
    public partial class InteractiveMapPage : Page
    {
        List<List<String>> Info = new List<List<string>>();
        public InteractiveMapPage()
        {
            InitializeComponent();
//            Checkpoint 1    Avenida Rudge   Yes Yes No No No
//Checkpoint 2    Theatro Municipal   Yes Yes Yes Yes Yes
//Checkpoint 3    Parque do Ibirapuera Yes Yes Yes No No
//Checkpoint 4    Jardim Luzitania    Yes Yes Yes No  Yes
//Checkpoint 5    Iguatemi Yes Yes Yes Yes No
//Checkpoint 6    Rua Lisboa  Yes Yes Yes No  No
//Checkpoint 7    Cemitério da Consolação Yes Yes Yes Yes Yes
//Checkpoint 8    Cemitério da Consolação Yes Yes Yes Yes Yes

            //Info.Add((new List<String>().AddRange()));
        }
        private void CheckCond(string cond)
        {
            string[] conds = cond.Split(' ');
            CheckpointDescription.Text = "";
            if (conds[0] == "Yes")
            {
                CheckpointDescription.Text += "-Напитки\n";
            }
            if (conds[1] == "Yes")
            {
                CheckpointDescription.Text += "-Энергетические батончики\n";
            }
            if (conds[2] == "Yes")
            {
                CheckpointDescription.Text += "-Туалеты\n";
            }
            if (conds[3] == "Yes")
            {
                CheckpointDescription.Text += "-Информационный стенд\n";
            }
            if (conds[4] == "Yes")
            {
                CheckpointDescription.Text += "-Медицинский пункт\n";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string name = button.Name;
            popup1.IsOpen = true;
            switch(name)
            {
                case "S1":
                    CheckpointName.Text = "Full marathon start (42km)";
                    CheckpointDescription.Text = "";
                    break;
                case "b1":
                    CheckpointName.Text = "Avenida Rudge";
                    CheckCond("Yes Yes No No No");
                    break;
                case "b2":
                    CheckpointName.Text = "Theatro Municipal";
                    CheckCond("Yes Yes Yes Yes Yes");
                    break;
                case "S2":
                    CheckpointName.Text = "Half marathon start (21km)";
                    CheckpointDescription.Text = "";
                    break;
                case "S3":
                    CheckpointName.Text = "Fun run start (5km)";
                    CheckpointDescription.Text = "";
                    break;
                case "b3":
                    CheckpointName.Text = "Parque do Ibirapuera";
                    CheckCond("Yes Yes Yes No No");
                    break;
                case "b4":
                    CheckpointName.Text = "Jardim Luzitania";
                    CheckCond("Yes Yes Yes No Yes");
                    break;
                case "b5":
                    CheckpointName.Text = "Iguatemi";
                    CheckCond("Yes Yes Yes Yes No");
                    break;
                case "b6":
                    CheckpointName.Text = "Rua Lisboa";
                    CheckCond("Yes Yes Yes No No");
                    break;
                case "b7":
                    CheckpointName.Text = "Cemitério da Consolação";
                    CheckCond("Yes Yes Yes Yes Yes");
                    break;
                case "b8":
                    CheckpointName.Text = "Cemitério da Consolação";
                    CheckCond("Yes Yes Yes Yes Yes");
                    break;
            }
        }
    }
}
