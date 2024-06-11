using CinemaApp.Classes;
using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Threading;

namespace CinemaApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для SessionsPage.xaml
    /// </summary>
    public partial class SessionsPage : Page
    {
        Movies _currentMovie = new Movies();
        private DispatcherTimer timer;
        int UserID;
        int RoleID;
        public SessionsPage(Movies selectedMovie, int userID, int roleID)
        {
            InitializeComponent();
            InitializeClock();

            UserID = userID;
            RoleID = roleID;

            if (selectedMovie != null)
                _currentMovie = selectedMovie;

            DataContext = _currentMovie;            

            tbDate1.Text = DateTime.Today.ToString("dd MMMM");
            tbDate2.Text = DateTime.Today.AddDays(1).ToString("dd MMMM");
            tbDate3.Text = DateTime.Today.AddDays(2).ToString("dd MMMM");

            tbPrice.Text = CinemaEntities.GetContext().Sessions.Where(x => x.MovieID == _currentMovie.ID).Min(x => x.Price).ToString();

            SessionsUpdate();
        }

        public void SessionsUpdate()
        {
            var date2 = DateTime.Today.AddDays(1);
            var date3 = DateTime.Today.AddDays(2);

            var time1 = DateTime.Now.TimeOfDay;
            var time2 = date2.TimeOfDay;
            var time3 = date3.TimeOfDay;

            var sessionsList1 = CinemaEntities.GetContext().Sessions.OrderBy(x => x.Time).Where(x => x.MovieID == _currentMovie.ID && x.Date == DateTime.Today && x.Time.CompareTo(time1) > 0).ToList();
            var sessionsList2 = CinemaEntities.GetContext().Sessions.OrderBy(x => x.Time).Where(x => x.MovieID == _currentMovie.ID && x.Date == date2 && x.Time.CompareTo(time2) > 0).ToList();
            var sessionsList3 = CinemaEntities.GetContext().Sessions.OrderBy(x => x.Time).Where(x => x.MovieID == _currentMovie.ID && x.Date == date3 && x.Time.CompareTo(time3) > 0).ToList();

            lvSessions1.ItemsSource = sessionsList1;
            lvSessions2.ItemsSource = sessionsList2;
            lvSessions3.ItemsSource = sessionsList3;
        }

        private void InitializeClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tbClock.Text = DateTime.Now.ToString("HH:mm");
        }

        private void btnTime_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new HallsPage((sender as Button).DataContext as Sessions, UserID, RoleID));           
        }
    }
}
