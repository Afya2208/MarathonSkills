using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.Database
{
    public partial class Registration
    {

        public string StatusInfo
        {
            get
            {
                RegistrationStatus status = DB.entities.RegistrationStatus.FirstOrDefault(reg => reg.RegistrationStatusId == RegistrationStatusId);
                return $"{status.RegistrationStatus1}";
            }
        }
        public string FirstName
        {
            get
            {
                Runner runner = DB.entities.Runner.FirstOrDefault(run => run.RunnerId == RunnerId);
                User user = DB.entities.User.FirstOrDefault(u => u.Email == runner.Email);
                return user.FirstName;
            }
        }

        public string LastName
        {
            get
            {
                Runner runner = DB.entities.Runner.FirstOrDefault(run => run.RunnerId == RunnerId);
                User user = DB.entities.User.FirstOrDefault(u => u.Email == runner.Email);
                return user.LastName;
            }
        }
        public string Gender
        {
            get
            {
                Runner runner = DB.entities.Runner.FirstOrDefault(run => run.RunnerId == RunnerId);
                
                return runner.Gender;
            }
        }
        public string DateOfBirthday
        {
            get
            {
                Runner runner = DB.entities.Runner.FirstOrDefault(run => run.RunnerId == RunnerId);

                return runner.DateOfBirth.Value.ToString("ddth MMMM yyyy");
            }
        }
        public string CharityInfo
        {
            get
            {
                Charity charity = DB.entities.Charity.FirstOrDefault(run => run.CharityId == CharityId);

                return charity.CharityName;
            }
        }
        public byte[] Photo
        {
            get
            {
                Runner runner = DB.entities.Runner.FirstOrDefault(run => run.RunnerId == RunnerId);
                return runner.Photo;
            }
        }

        public string CountryInfo
        {
            get
            {
                Runner runner = DB.entities.Runner.FirstOrDefault(run => run.RunnerId == RunnerId);
                Country country = DB.entities.Country.FirstOrDefault(run => run.CountryCode == runner.CountryCode);

                return country.CountryName;
            }
        }
        public string DistanceInfo
        {
            get
            {
                List<RegistrationEvent> registrationEvents = DB.entities.RegistrationEvent.Where(r => r.RegistrationId == RegistrationId).ToList();
                List<Event> events = new List<Event>();
                foreach (RegistrationEvent registrationEvent in registrationEvents)
                {
                    Event @event = DB.entities.Event.FirstOrDefault();
                    if (!events.Contains(@event) && events!= null) events.Add(@event);
                }
                string res = "";
                for (int i = 0; i < events.Count - 1; i++)
                {
                    res += events[i].EventName + ",\n";
                }
                return res += events[events.Count-1].EventName;
            }
        }
        public string Email
        {
            get
            {
                Runner runner = DB.entities.Runner.FirstOrDefault(run => run.RunnerId == RunnerId);
                
                return runner.Email;
            }
        }

    }
}
