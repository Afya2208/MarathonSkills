using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MarathonSkills.Database
{
    internal class ComparateItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageFileName { 
            get 
            {
                string name = Name.ToLower();
                name = name.Replace(' ', '-');
                return "C:\\Users\\masha\\Desktop\\how-long-is-a-marathon-images\\" + name + ".jpg";
            }
        }
    }
}
