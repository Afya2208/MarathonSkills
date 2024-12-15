using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.Database
{
    public partial class Charity
    {
        public string FullFileName
        {
            get
            {
                return @"C:\Users\masha\Desktop\charity\" + CharityLogo;
            }
        }
        public decimal Sum { get; set; }
    }
}
