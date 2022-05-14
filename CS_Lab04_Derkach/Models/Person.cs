using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CS_Lab04_Derkach.Models.InputExceptions;

namespace CS_Lab04_Derkach.Models
{
    [Serializable]
    public class Persons
    {
        public List<Person> PersonsList { get; set; } = new List<Person>();
    }

    [Serializable]
    public class Person
    {
        private string _lastname;
        private string _surname;
        private string? _email;
        private DateTime? _birthday;
        private bool? _isBirthday;
        private bool? _isAdult;
        private string? _sunSign;
        private string? _chineseSign;

        public string Lastname { get => _lastname; set => _lastname = value; }
        public string Surname { get => _surname; set => _surname = value; }
        public string? Email { get => _email; set => _email = value; }
        public DateTime? Birthday { get => _birthday; set => _birthday = value; }
        public bool? IsAdult { get => _isAdult; set => _isAdult = value; }
        public bool? IsBirthday { get => _isBirthday; set => _isBirthday = value; }
        public string? SunSign { get => _sunSign; set => _sunSign = value; }
        public string? ChineseSign { get => _chineseSign; set => _chineseSign = value; }
        public bool countIsAdult { get => (Birthday.Value.AddYears(18).CompareTo(DateTime.Now) <= 0); }
        public string countSunSign {
            get
            {
                switch (Birthday.Value.Month)
                {
                    case 1: return (Birthday.Value.Day <= 20) ? "Capricorn" : "Aquarius"; break;
                    case 2: return (Birthday.Value.Day <= 19) ? "Aquarius" : "Pisces"; break;
                    case 3: return (Birthday.Value.Day <= 20) ? "Pisces" : "Aries"; break;
                    case 4: return (Birthday.Value.Day <= 20) ? "Aries" : "Taurus"; break;
                    case 5: return (Birthday.Value.Day <= 21) ? "Taurus" : "Gemini"; break;
                    case 6: return (Birthday.Value.Day <= 21) ? "Gemini" : "Cancer"; break;
                    case 7: return (Birthday.Value.Day <= 22) ? "Cancer" : "Leo"; break;
                    case 8: return (Birthday.Value.Day <= 23) ? "Leo" : "Virgo"; break;
                    case 9: return (Birthday.Value.Day <= 23) ? "Virgo" : "Libra"; break;
                    case 10: return (Birthday.Value.Day <= 23) ? "Libra" : "Scorpio"; break;
                    case 11: return (Birthday.Value.Day <= 22) ? "Scorpio" : "Sagittarius"; break;
                    case 12: return (Birthday.Value.Day <= 23) ? "Sagittarius" : "Capricorn"; break;
                    default: return "How did you even get this case?";
                }
            } }
        public string countChineseSign
        {
            get
            {
                switch (Birthday.Value.Year % 12)
                {
                    case 0: return "Monkey"; break;
                    case 1: return "Rooster"; break;
                    case 2: return "Dog"; break;
                    case 3: return "Pig"; break;
                    case 4: return "Rat"; break;
                    case 5: return "Bull"; break;
                    case 6: return "Tiger"; break;
                    case 7: return "Rabbit"; break;
                    case 8: return "Dragon"; break;
                    case 9: return "Snake"; break;
                    case 10: return "Horse"; break;
                    case 11: return "Goat"; break;
                    default: return "Another unreachable return";
                }
            } }

        public bool countIsBirthday { get => (DateTimeOffset.Now.Day == Birthday.Value.Day) && (DateTimeOffset.Now.Month == Birthday.Value.Month); }
        public Person(string lastname, string surname, string email, DateTime birthday)
        {
            Lastname = lastname;
            Surname = surname;
            Email = email;
            Birthday = birthday;
            refresh();
        }
        public void validate()
        {
            if (!(new Regex("[a-zA-Z]+").Match(Lastname).Length == Lastname.Length)) throw new InvalidLastnameException();
            if (!(new Regex("[A-Z][a-z]+").Match(Lastname).Length == Lastname.Length)) throw new CapitalLastnameException();
            if (!(new Regex("[a-zA-Z]+").Match(Surname).Length == Surname.Length)) throw new InvalidSurnameException();
            if (!(new Regex("[A-Z][a-z]+").Match(Surname).Length == Surname.Length)) throw new CapitalSurnameException();
            if (!(new Regex("[a-zA-Z][0-9a-zA-Z.]+[@][a-zA-Z][a-zA-Z.]*[.][a-zA-Z]+").Match(Email).Length == Email.Length) || Email.Contains("..")) throw new InvalidEmailException();
            if (Birthday.Value.Year.CompareTo(DateTimeOffset.Now.Year - 135) < 0) throw new ImpossibleAgeException();
            if (Birthday.Value.CompareTo(DateTimeOffset.Now.DateTime) > 0) throw new FutureBirthdayException();
        }
        public void refresh()
        {
            _sunSign = countSunSign;
            _chineseSign= countChineseSign;
            _isAdult = countIsAdult;
            _isBirthday = countIsBirthday;
        }
        public Person() { }
        public Person(string lastname, string surname, string email)
        {
            Lastname = lastname;
            Surname = surname;
            Email = email;
        }
        public Person(string lastname, string surname, DateTime birthday)
        {
            Lastname = lastname;
            Surname = surname;
            Birthday = birthday;
            refresh();
        }
    }
}
