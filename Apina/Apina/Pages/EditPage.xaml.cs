using System;
using System.Collections.Generic;
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
using RestSharp;

namespace Apina.Pages
{
    /// <summary>
    /// Interakční logika pro EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        public object obj;
        public int ID;
        public DateTime birthdate;
        public string Url = "https://student.sps-prosek.cz/~drechlu14/api/";

        public EditPage()
        {
            InitializeComponent();
        }

        public EditPage(Osoba osoba)
        {
            InitializeComponent();
            obj = osoba;
            ID = osoba.Id;
            Name.Text = osoba.Name;
            SurName.Text = osoba.Surname;
            RodneCislo1.Text = osoba.RC1;
            RodneCislo2.Text = osoba.RC2;
            BirthDate.SelectedDate = osoba.BirthDate;
            birthdate = osoba.BirthDate;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.POST);
            request.AddParameter("Id", ID);
            request.AddParameter("TypOperace", 2);
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Comunication error.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MainPage Page1 = new MainPage();
            this.NavigationService.Navigate(new MainPage());
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.POST);
            request.AddParameter("Id", ID);
            request.AddParameter("TypOperace", 3);
            request.AddParameter("Name", Name.Text);
            request.AddParameter("Surname", SurName.Text);
            request.AddParameter("RC1", RodneCislo1.Text);
            request.AddParameter("RC2", RodneCislo2.Text);
            request.AddParameter("BirthDate", BirthDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"));
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Content, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Comunication error.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage Page1 = new MainPage();
            this.NavigationService.Navigate(new MainPage());
        }
    }
}

