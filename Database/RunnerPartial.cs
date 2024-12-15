using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.Database
{
    public partial class Runner
    {
        public Gender GenderFull { 
            get
            {
               Gender g = DB.entities.Gender.FirstOrDefault(u => u.Gender1 == Gender);
                return g;
            }
                
        }

        public Country CountryFull
        {
            get
            {
                Country g = DB.entities.Country.FirstOrDefault(u => u.CountryCode == CountryCode);
                return g;
            }

        }

    }
}
