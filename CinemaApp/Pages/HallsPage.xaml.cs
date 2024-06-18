using CinemaApp.Classes;
using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для HallsPage.xaml
    /// </summary>
    public partial class HallsPage : Page
    {
        Sessions _currentSession = new Sessions();
        int UserID;
        int RoleID;
        public HallsPage(Sessions selectedSession, int userID, int roleID)
        {
            InitializeComponent();

            UserID = userID;
            RoleID = roleID;

            if (selectedSession != null)
                _currentSession = selectedSession;

            DataContext = _currentSession;
          
            tbHall.Text = CinemaEntities.GetContext().RowsAndSeats.FirstOrDefault(x => x.HallID == _currentSession.HallID).HallName;

            SeatsUpdate();
        }

        public void SeatsUpdate()
        {
            var seatsList1 = CinemaEntities.GetContext().RowsAndSeats.Where(x => x.HallID == _currentSession.HallID && x.Row == 1 && x.SessionID == _currentSession.ID).ToList();
            var seatsList2 = CinemaEntities.GetContext().RowsAndSeats.Where(x => x.HallID == _currentSession.HallID && x.Row == 2 && x.SessionID == _currentSession.ID).ToList();

            lvSeats1.ItemsSource = seatsList1;
            lvSeats2.ItemsSource= seatsList2;
        }

        private void btnSeat_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new TicketPage((sender as Button).DataContext as RowsAndSeats, _currentSession.MovieName, _currentSession.Date, _currentSession.Time, _currentSession.Price, _currentSession.MovieID, UserID, RoleID));
        }
    }
}
