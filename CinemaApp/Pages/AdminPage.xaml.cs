using CinemaApp.Classes;
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

namespace CinemaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        int UserID;
        int RoleID;
        public AdminPage(int userID, int roleID)
        {
            InitializeComponent();
            UserID = userID;
            RoleID = roleID;
        }

        private void btnMovies_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AdminCinemaPage());
        }

        private void btnSessions_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AdminSessionsPage());
        }

        private void btnGenres_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AdminGenresPage());
        }

        private void btnCountries_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AdminCountryPage());
        }

        private void btnHalls_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AdminHallsPage());
        }

        private void btnTickets_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AdminTicketsPage());
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AdminUsersPage(UserID, RoleID));
        }
    }
}
