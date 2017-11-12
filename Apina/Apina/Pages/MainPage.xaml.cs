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
using System.Text.RegularExpressions;

namespace Apina.Pages
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public string Url = "https://student.sps-prosek.cz/~drechlu14/api/";

        public MainPage()
        {
            InitializeComponent();
            listview.ItemsSource = GetData();
        }

        public void listViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Content;
            EditPage EditPage = new EditPage();
            this.NavigationService.Navigate(new EditPage(obj as Osoba));
        }

        private List<Osoba> GetData(string SearchSurname = null)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.POST);
            request.AddParameter("TypOperace", 0);
            if (SearchSurname != null || SearchSurname != "")
            {
                request.AddParameter("Surname", SearchSurname);
            }
            var response = client.Execute<List<Osoba>>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("Comunication error.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else
            {
                return response.Data;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(SearchText.Text, @"^[a-zA-Z]+$") == false)
            {
                MessageBox.Show("Hledaný výraz je neplatný.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (SearchText.Text != "")
            {
                listview.ItemsSource = GetData(SearchText.Text);
            }
            else
            {
                listview.ItemsSource = GetData();
            }
        }

        int x = 0;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient(Url);
            var request = new RestRequest(Method.POST);
            request.AddParameter("TypOperace", 1);
            request.AddParameter("Name", Name.Text);
            request.AddParameter("Surname", SurName.Text);
            request.AddParameter("RC1", RodneCislo1.Text);
            request.AddParameter("RC2", RodneCislo2.Text);
            request.AddParameter("BirthDate", BirthDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd"));
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                listview.ItemsSource = GetData();
                MessageBox.Show(response.Content, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Comunication error.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

