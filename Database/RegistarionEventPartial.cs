using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.Database
{
    public partial class RegistrationEvent
    {
        public int Rank { get; set; }
        
        
        public string RunnerInfo
        {
            get
            {
                Registration registration = DB.entities.Registration.FirstOrDefault(reg => reg.RegistrationId == RegistrationId);
                Runner runner = DB.entities.Runner.FirstOrDefault(r => r.RunnerId == registration.RunnerId);
                User user = DB.entities.User.FirstOrDefault(u => u.Email == runner.Email);
                return $"{user.FirstName} {user.LastName} - {BibNumber} ({runner.CountryCode})";
            }

        }
        public string RunnerName
        {
            get
            {
                Registration registration = DB.entities.Registration.FirstOrDefault(reg => reg.RegistrationId == RegistrationId);
                Runner runner = DB.entities.Runner.FirstOrDefault(r => r.RunnerId == registration.RunnerId);
                User user = DB.entities.User.FirstOrDefault(u => u.Email == runner.Email);
                return $"{user.FirstName} {user.LastName}";
            }

        }
        public string CountryCode
        {
            get
            {
                Registration registration = DB.entities.Registration.FirstOrDefault(reg => reg.RegistrationId == RegistrationId);
                Runner runner = DB.entities.Runner.FirstOrDefault(r => r.RunnerId == registration.RunnerId);
                
                return $"{runner.CountryCode}";
            }

        }
        public string RaceTimeInfo
        {
            get
            {
                if (RaceTime != null)
                {
                    int time = RaceTime.Value;
                    return $"{time/360}h {(time - (time/360*360))/60}m {time%60}s";
                }else
                {
                    return "Нет данных";
                }
            }

        }
        public Gender Gender
        {
            get
            {
                Registration registration = DB.entities.Registration.FirstOrDefault(reg => reg.RegistrationId == RegistrationId);
                Runner runner = DB.entities.Runner.FirstOrDefault(r => r.RunnerId == registration.RunnerId);
                Gender gender = DB.entities.Gender.FirstOrDefault(g => g.Gender1 == runner.Gender);
                return gender;
            }

        }
        public string MarathonInfo
        {
            get
            {
                Event ev = DB.entities.Event.FirstOrDefault(o => o.EventId == EventId);
                Marathon marathon = DB.entities.Marathon.FirstOrDefault(o => o.MarathonId == ev.MarathonId);
                return $"{marathon.YearHeld} {marathon.CityName}";
            }

        }
        public string DistanceInfo
        {
            get
            {
                Event ev = DB.entities.Event.FirstOrDefault(o => o.EventId == EventId);
                EventType evType = DB.entities.EventType.FirstOrDefault(g => g.EventTypeId == ev.EventTypeId);
                return $"{evType.EventTypeName}";
            }

        }
        public string TimeInfo
        {
            get
            {
                if (RaceTime != null)
                {
                    int hours = RaceTime.Value / 60 / 60; // сколько полных часов
                    int minutes = RaceTime.Value % 360; // сколько минут
                    int seconds = RaceTime.Value % 60;
                    
                    return $"{hours}h {minutes}m {seconds}s";
                } else
                {
                    return $"";
                }
                
            }

        }
        public string GeneralRangInfo
        {
            get
            {
                Event ev = DB.entities.Event.FirstOrDefault(o => o.EventId == EventId);
                List<RegistrationEvent> races = DB.entities.RegistrationEvent.Where(v => v.EventId == ev.EventId && v.RaceTime != null).ToList(); // список всех участников
                races = races.OrderBy(b => b.RaceTime).ToList();
                int indexer = 1;
                if (races.Count > 0)
                {
                    int maxRaceTime = races[0].RaceTime.Value;
                    foreach (var r in races)
                    {
                        if (r.RaceTime > maxRaceTime)
                        {
                            maxRaceTime = r.RaceTime.Value;
                            indexer++;
                        }
                        if (r.Equals(this))
                        {
                            return $"#{indexer}";
                        }
                    }
                }

                return $"#{indexer}";
            }


        }
        public string Category
        {
            get
            {
                Registration registration = DB.entities.Registration.FirstOrDefault(reg => reg.RegistrationId == RegistrationId);
                Runner runner = DB.entities.Runner.FirstOrDefault(run => run.RunnerId == registration.RunnerId);
                int Years = (DateTime.Today - runner.DateOfBirth.Value).Days/365;
                if (Years < 18)
                {
                    return "до 18";
                } else if (Years < 30)
                {
                    return "от 18 до 29";
                }
                else if (Years < 40)
                {
                    return "от 30 до 39";
                }
                else if (Years < 56)
                {
                    return "от 40 до 55";
                }
                else if (Years < 70)
                {
                    return "от 56 до 69";
                }
                else if (Years >= 70)
                {
                    return "от 70";
                }
                else
                {
                    return "";
                }
            }


        }
        
    }
}
