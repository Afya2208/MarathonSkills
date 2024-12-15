using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.Database
{
    public partial class Volunteer
    {
        public string CountryName
        {
            get
            {
                Country country = DB.entities.Country.FirstOrDefault(co => co.CountryCode == CountryCode);
                return country.CountryName;
            }
        }
        public string GenderName
        {
            get
            {
                return Gender == "Female" ? "Женский" : "Мужской";
            }
        }
    }
}
