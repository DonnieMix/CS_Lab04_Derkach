using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CS_Lab04_Derkach.Views;
using CS_Lab04_Derkach.Models;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Controls;
using CS_Lab04_Derkach.Models.InputExceptions;

namespace CS_Lab04_Derkach.ViewModels
{
    public class UsersViewModel
    {
        private readonly UsersView _view;
        private Persons _persons;

        public Persons Persons { get => _persons; set { _persons = value; } }
        public UsersView View { get => _view; }
        public UsersViewModel(UsersView view)
        {
            _view = view;
            if (File.Exists("Persons.xml"))
            {
                _persons = read();
                if(_persons.PersonsList.Count < 50)
                {
                    Random rnd = new Random();
                    for(int i = _persons.PersonsList.Count; i <= 50; i++)
                    {
                        Person toAdd = new Person($"John{i}", $"Doe{i}", $"john.doe{i}@email.com", DateTimeOffset.Now.DateTime.AddYears(-rnd.Next(50)).AddMonths(rnd.Next(-6,6)).AddDays(rnd.Next(-15, 15)));
                        toAdd.refresh();
                        _persons.PersonsList.Add(toAdd);
                    }
                    write(Persons);
                }
            }
            else
            {
                _persons = new Persons();
                Random rnd = new Random();
                for (int i = 1; i <= 50; i++)
                {
                    Person toAdd = new Person($"John{i}", $"Doe{i}", $"john.doe{i}@email.com", DateTimeOffset.Now.DateTime.AddYears(-rnd.Next(50)).AddMonths(rnd.Next(-6, 6)).AddDays(rnd.Next(-15, 15)));
                    toAdd.refresh();
                    _persons.PersonsList.Add(toAdd);
                }
                write(Persons);
            }
            _view.DGUsers.ItemsSource = _persons.PersonsList;
        }
        public static void write(Persons objectToWrite)
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(Persons));
                writer = new StreamWriter("Persons.xml", false);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        public static Persons read()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(Persons));
                reader = new StreamReader("Persons.xml");
                return (Persons)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public void BAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Person person = new Person(View.TbLastname.Text, View.TbSurname.Text, View.TbEmail.Text, View.DpBirthday.SelectedDate.Value);
                person.validate();
                person.refresh();
                Persons.PersonsList.Add(person);
                write(Persons);
                View.DGUsers.Items.Refresh();
            }
            catch (ImpossibleAgeException)
            {
                MessageBox.Show("Age of person is too big!", "ErrorMessage",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (FutureBirthdayException)
            {
                MessageBox.Show("Person hasn't even born yet!", "ErrorMessage",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (InvalidEmailException)
            {
                MessageBox.Show("Given the wrong email address!", "ErrorMessage",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (InvalidLastnameException)
            {
                MessageBox.Show("Invalid lastname!", "ErrorMessage",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (InvalidSurnameException)
            {
                MessageBox.Show("Invalid surname!", "ErrorMessage",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (CapitalLastnameException)
            {
                MessageBox.Show("Lastname should start with capital letter,\nfollowing with lowercase letters", "ErrorMessage",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (CapitalSurnameException)
            {
                MessageBox.Show("Surname should start with capital letter,\nfollowing with lowercase letters", "ErrorMessage",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        public void BEdit_Click(object sender, RoutedEventArgs e)
        {
            if (View.DGUsers.SelectedItems.Count >= 1)
            {
                try
                {
                    Person toChange = Persons.PersonsList.ElementAt(View.DGUsers.SelectedIndex);
                    toChange.Lastname = View.TbLastname.Text;
                    toChange.Surname = View.TbSurname.Text;
                    toChange.Email = View.TbEmail.Text;
                    toChange.Birthday = View.DpBirthday.SelectedDate.Value;
                    toChange.validate();
                    toChange.refresh();
                    View.DGUsers.Items.Refresh();
                    write(Persons);
                }
                catch (ImpossibleAgeException)
                {
                    MessageBox.Show("Age of person is too big!", "ErrorMessage",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                catch (FutureBirthdayException)
                {
                    MessageBox.Show("Person hasn't even born yet!", "ErrorMessage",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                catch (InvalidEmailException)
                {
                    MessageBox.Show("Given the wrong email address!", "ErrorMessage",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                catch (InvalidLastnameException)
                {
                    MessageBox.Show("Invalid lastname!", "ErrorMessage",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                catch (InvalidSurnameException)
                {
                    MessageBox.Show("Invalid surname!", "ErrorMessage",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                catch (CapitalLastnameException)
                {
                    MessageBox.Show("Lastname should start with capital letter,\nfollowing with lowercase letters", "ErrorMessage",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                catch (CapitalSurnameException)
                {
                    MessageBox.Show("Surname should start with capital letter,\nfollowing with lowercase letters", "ErrorMessage",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        public void BRemove_Click(object sender, RoutedEventArgs e)
        {
            if (View.DGUsers.SelectedItems.Count >= 1)
            {
                foreach (Person person in View.DGUsers.SelectedItems) Persons.PersonsList.Remove(person);
                View.DGUsers.Items.Refresh();
                write(Persons);
            }
        }
        public void BRefresh_Click(object sender, RoutedEventArgs e)
        {
            Persons = read();
            View.DGUsers.ItemsSource = Persons.PersonsList;
            View.DGUsers.Items.Refresh();
        }
        public void DGUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (View.DGUsers.SelectedItems.Count >= 1 && View.DGUsers.SelectedIndex < Persons.PersonsList.Count)
            {
                View.TbLastname.Text = Persons.PersonsList.ElementAt(View.DGUsers.SelectedIndex).Lastname;
                View.TbSurname.Text = Persons.PersonsList.ElementAt(View.DGUsers.SelectedIndex).Surname;
                View.TbEmail.Text = Persons.PersonsList.ElementAt(View.DGUsers.SelectedIndex).Email;
                View.DpBirthday.SelectedDate = Persons.PersonsList.ElementAt(View.DGUsers.SelectedIndex).Birthday;
            }
        }
    }
}
